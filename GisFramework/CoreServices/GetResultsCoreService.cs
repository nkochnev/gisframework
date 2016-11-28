using System;
using System.Diagnostics;
using System.Linq;
using GisFramework.Data;
using GisFramework.Domains;
using GisFramework.Interfaces;
using GisFramework.Interfaces.Converters;
using GisFramework.Interfaces.Handlers;
using GisFramework.Interfaces.Providers;
using GisFramework.Interfaces.Services;

namespace GisFramework.CoreServices
{
	/// <summary>
	/// Базовый сервис для третьего этапа взаимодействия - получения результата обработки
	/// </summary>
	/// <typeparam name="TMessageDomain">Тип доменного сообщения</typeparam>
	/// <typeparam name="TGetStateResultProxy">Тип прокси объекта запроса результата обработки</typeparam>
	/// <typeparam name="TResultProxy">Тип прокси объекта результата обработки сообщения</typeparam>
	/// <typeparam name="TResult">Тип объекта результата обработки сообщения</typeparam>
	public class GetResultsCoreService<TMessageDomain, TGetStateResultProxy, TResultProxy, TResult> : ICoreService
		where TMessageDomain : MessageDomain
		where TResultProxy : IGetStateResult
	{
		private readonly IMessageDomainService<TMessageDomain> _messageDomainService;
		private readonly IGetResultProxyProvider<TGetStateResultProxy, TResultProxy> _getResultProxyProvider;
		private readonly IGetStateProxyConverter<TGetStateResultProxy, TMessageDomain> _getStateProxyConverter;
		private readonly IResultConverter<TResultProxy, TResult> _resultConverter;
		private readonly ISaveResultService<TResult, TMessageDomain> _saveResultService;
		private readonly IGetResultMessageHandler<TMessageDomain, TResult> _getResultMessageHandler;
		private readonly IGisLogger _logger;
		
		/// <summary>
		/// Количество дней, через которые считается, что запрос не выполнится никогда
		/// </summary>
		private const int GET_RESULT_TIMEOUT_IN_DAYS = 3;

		public GetResultsCoreService(IMessageDomainService<TMessageDomain> messageDomainService,
			IGetResultProxyProvider<TGetStateResultProxy, TResultProxy> getResultProxyProvider,
			IGetStateProxyConverter<TGetStateResultProxy, TMessageDomain> getStateProxyConverter,
			IResultConverter<TResultProxy, TResult> resultConverter,
			ISaveResultService<TResult, TMessageDomain> saveResultService,
			IGetResultMessageHandler<TMessageDomain, TResult> getResultMessageHandler, IGisLogger logger)
		{
			_messageDomainService = messageDomainService;
			_getResultProxyProvider = getResultProxyProvider;
			_getStateProxyConverter = getStateProxyConverter;
			_resultConverter = resultConverter;
			_saveResultService = saveResultService;
			_getResultMessageHandler = getResultMessageHandler;
			_logger = logger;
		}

		public void Do(CoreInitData coreInitData)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			try
			{
				//получаем доменнные сообщения для проверки результата обработки
				var messages = _messageDomainService.GetMessageDomainsForGetResults(coreInitData);
				foreach (var messageDomain in messages)
				{
					try
					{
						//по доменному сообщению получаем getState для проверки результатов обработки сообщения
						var getStateProxy = _getStateProxyConverter.ToGetStateResultProxy(messageDomain);
						TResultProxy resultProxy;
						//проверяем результат обработки. 
						//если возвращается false, значит сообщение ещё не обработано
						//если true, значит можно получать результат обработки
						if (_getResultProxyProvider.TryGetResult(getStateProxy, out resultProxy))
						{
							//полученный ответ преобразовываем из прокси сущности в нашу бизнес-сущность результата обработки
							var result = _resultConverter.ToResult(resultProxy);
							//сохраняем результат обработки сообщения
							_saveResultService.SaveResult(result, messageDomain);
							//проставляем статусы обработки сообщения в доменном сообщении
							_getResultMessageHandler.Success(messageDomain, result);
						}
						else
						{
							if (messageDomain.SendedDate.HasValue 
								&& DateTime.Now.Subtract(messageDomain.SendedDate.Value).Days > GET_RESULT_TIMEOUT_IN_DAYS)
							{
								//в течение таймаута не можем получить результат обработки сообщения, помечаем
								_getResultMessageHandler.NoResultByTimeout(messageDomain);
							}
							else
							{
								//помечаем, что сообщение ещё не обработалось
								_getResultMessageHandler.NotReady(messageDomain);
							}
						}
					}
					catch (Exception exception)
					{
						//обрабатываем исключения во время работы
						_getResultMessageHandler.Fail(messageDomain, exception);
					}
				}
				stopWatch.Stop();
				_logger.Info(this.GetType(), $"По {messages.Count} доменным сообщениям УК {coreInitData.UkId} получено " +
							  $"{messages.Count(x => x.Status == MessageStatus.Done)} успешных ответов, " +
							  $"{messages.Count(x => x.Status == MessageStatus.InProcess)} в обработке, " +
							  $"{messages.Count(x => x.Status == MessageStatus.ResponseTakingError)} упали с ошибкой, " +
							  $"{messages.Count(x => x.Status == MessageStatus.ResponseTakingErrorTryAgain)} будут отправлены повторно, за {stopWatch.Elapsed}");
			}
			catch (Exception ex)
			{
				_logger.Error(this.GetType(),$"Произошло исключение при обработке {coreInitData}", ex);
			}
		}
	}
}