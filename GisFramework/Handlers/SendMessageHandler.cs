using System;
using GisFramework.Data;
using GisFramework.Domains;
using GisFramework.Exceptions;
using GisFramework.Interfaces;
using GisFramework.Interfaces.Handlers;
using GisFramework.Interfaces.Services;

namespace GisFramework.Handlers
{
	/// <summary>
	/// Обработчик доменного сообщения при отправке сообщения
	/// </summary>
	/// <typeparam name="TMessageDomain">Тип доменного сообщения</typeparam>
	/// <typeparam name="TAckProxy">Тип прокси объекта ответа на отправку сообщения</typeparam>
	public class SendMessageHandler<TMessageDomain, TAckProxy> : ISendMessageHandler<TMessageDomain, TAckProxy>
		where TMessageDomain : MessageDomain
		where TAckProxy : IAckRequestAck
	{
		private readonly IGisLogger _logger;
		private readonly IMessageDomainService<TMessageDomain> _messageDomainService;

		public SendMessageHandler(IGisLogger logger,
			IMessageDomainService<TMessageDomain> messageDomainService)
		{
			_logger = logger;
			_messageDomainService = messageDomainService;
		}

		/// <summary>
		/// Отправка сообщения прошла успешно.
		/// </summary>
		/// <param name="messageDomain">Тип доменного сообщения</param>
		/// <param name="ackProxy"></param>
		public void SendSuccess(TMessageDomain messageDomain, TAckProxy ackProxy)
		{
			messageDomain.Status = MessageStatus.Sent;
			messageDomain.Sended = DateTime.Now;
			messageDomain.ResponseGuid = new Guid(ackProxy.MessageGUID);
			_messageDomainService.Update(messageDomain);
		}

		/// <summary>
		/// При отправке сообщения произошло исключение
		/// </summary>
		/// <param name="messageDomain">Тип доменного сообщения</param>
		/// <param name="exception">Исключение</param>
		public void SendFail(TMessageDomain messageDomain, Exception exception)
		{
			//По умолчанию все сообщения нужно попробовать отправить ещё раз
			var status = MessageStatus.SendErrorTryAgain;
			//только обработанные ошибки не подлежат повторной отправке
			if (exception is IntegrationServerErrorException)
			{
				status = MessageStatus.SendError;
			}
			messageDomain.Status = status;
			messageDomain.ErrorText = exception.Message;
			_messageDomainService.Update(messageDomain);
			_logger.Error(exception);
		}
	}
}