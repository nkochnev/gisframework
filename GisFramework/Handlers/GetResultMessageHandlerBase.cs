using System;
using GisFramework.Data;
using GisFramework.Domains;
using GisFramework.Exceptions;
using GisFramework.Interfaces.Handlers;
using GisFramework.Interfaces.Services;

namespace GisFramework.Handlers
{
	/// <summary>
	/// Базовый обработчик доменного сообщения при получении результата обработки сообщения
	/// </summary>
	/// <typeparam name="TMessageDomain">Тип доменного сообщения</typeparam>
	/// <typeparam name="TResult">Тип сущности результата обработки сообщения</typeparam>
	public abstract class GetResultMessageHandlerBase<TMessageDomain, TResult> : IGetResultMessageHandler<TMessageDomain, TResult>
		where TMessageDomain : MessageDomain
	{
		private readonly IGisLogger _logger;
		private readonly IMessageDomainService<TMessageDomain> _messageDomainService;

		protected GetResultMessageHandlerBase(IGisLogger logger, IMessageDomainService<TMessageDomain> messageDomainService)
		{
			_logger = logger;
			_messageDomainService = messageDomainService;
		}

		/// <summary>
		/// Обработка успешного получения результата обработки сообщения
		/// </summary>
		/// <param name="messageDomain"></param>
		/// <param name="result"></param>
		public abstract void Success(TMessageDomain messageDomain, TResult result);

		/// <summary>
		/// Сообщение ещё не обработано в ГИС ЖКХ
		/// </summary>
		/// <param name="messageDomain">Тип доменного сообщения</param>
		public void NotReady(TMessageDomain messageDomain)
		{
			messageDomain.Status = MessageStatus.InProcess;
			_messageDomainService.Update(messageDomain);
		}
		
		/// <summary>
		/// В течение таймаута не можем получить результат обработки сообщения
		/// </summary>
		/// <param name="messageDomain"></param>
		public void NoResultByTimeout(TMessageDomain messageDomain)
		{
			messageDomain.Status = MessageStatus.NoResultByTimeout;
			_messageDomainService.Update(messageDomain);
		}

		/// <summary>
		/// При получении результата обработки произошло исключение
		/// </summary>
		/// <param name="messageDomain">Тип доменного сообщения</param>
		/// <param name="exception">Исключение</param>
		public void Fail(TMessageDomain messageDomain, Exception exception)
		{
			//По умолчанию все сообщения нужно попробовать отправить ещё раз
			var status = MessageStatus.ResponseTakingErrorTryAgain;
			//только обработанные ошибки не подлежат повторной отправке
			if (exception is IntegrationServerErrorException)
			{
				status = MessageStatus.ResponseTakingError;
			}
			messageDomain.Status = status;
			messageDomain.ErrorText = exception.Message;
			_messageDomainService.Update(messageDomain);
			_logger.Error(exception);
		}
	}
}