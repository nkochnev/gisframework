using System;
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
	/// Базовый сервис для второго этапа взаимодействия - отправки сообщений
	/// </summary>
	/// <typeparam name="TMessageDomain">Тип доменного сообщения</typeparam>
	/// <typeparam name="TMessageProxy">Тип прокси объекта сообщения</typeparam>
	/// <typeparam name="TAckProxy">Тип прокси объекта ответа</typeparam>
	public class SendMessageCoreService<TMessageDomain, TMessageProxy, TAckProxy> : ICoreService
		where TMessageDomain : MessageDomain
		where TAckProxy : IAckRequestAck
	{
		private readonly IMessageDomainService<TMessageDomain> _messageDomainService;
		private readonly IMessageProxyConverter<TMessageDomain, TMessageProxy> _messageProxyConverter;
		private readonly ISendMessageProxyProvider<TMessageProxy, TAckProxy> _sendMessageProxyProvider;
		private readonly ISendMessageHandler<TMessageDomain, TAckProxy> _sendMessageHandler;
		private readonly IGisLogger _logger;

		private DateTime _startDateTime;

		public SendMessageCoreService(IMessageDomainService<TMessageDomain> messageDomainService,
			IMessageProxyConverter<TMessageDomain, TMessageProxy> messageProxyConverter,
			ISendMessageProxyProvider<TMessageProxy, TAckProxy> sendMessageProxyProvider,
			ISendMessageHandler<TMessageDomain, TAckProxy> sendMessageHandler, IGisLogger logger)
		{
			_messageDomainService = messageDomainService;
			_messageProxyConverter = messageProxyConverter;
			_sendMessageProxyProvider = sendMessageProxyProvider;
			_sendMessageHandler = sendMessageHandler;
			_logger = logger;
		}

		public void Do(CoreInitData coreInitData)
		{
			_startDateTime = DateTime.Now;
			try
			{
				//получаем доменные сообщения для отправки
				//не обязательно могут быть только новые
				//также поднимаются не отправленные с первого раза
				var messages = _messageDomainService.GetMessageDomainsForSend(coreInitData);
				foreach (var messageDomain in messages)
				{
					try
					{
						//по каждому из доменных сообщений создаем прокси сообщение
						var proxyMessageRequests = _messageProxyConverter.ToMessageProxy(messageDomain);
						//отправляем прокси сообщение
						var proxyAck = _sendMessageProxyProvider.SendMessage(proxyMessageRequests);
						//обрабатываем успешный результат
						_sendMessageHandler.SendSuccess(messageDomain, proxyAck);
					}
					catch (Exception exception)
					{
						//обрабатываем исключения
						_sendMessageHandler.SendFail(messageDomain, exception);
					}
				}

				var duration = DateTime.Now.Subtract(_startDateTime);
				_logger.Info(this.GetType(), $"По {messages.Count} доменным сообщениям УК {coreInitData.UkId} отправлено " +
							  $"{messages.Count(x => x.Status == MessageStatus.Sent)} сообщений, " +
							  $"{messages.Count(x => x.Status == MessageStatus.SendError)} упали с ошибкой, " +
							  $"{messages.Count(x => x.Status == MessageStatus.SendErrorTryAgain)} будут отправлены повторно, за {duration}");
			}
			catch (Exception ex)
			{
				_logger.Error(this.GetType(), $"Произошло исключение при обработке {coreInitData}", ex);
			}
		}
	}
}