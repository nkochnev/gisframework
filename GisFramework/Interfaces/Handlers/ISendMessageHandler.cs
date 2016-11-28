using System;
using GisFramework.Domains;

namespace GisFramework.Interfaces.Handlers
{
	/// <summary>
	/// Обработчик доменной сущности при отправке сообщения
	/// </summary>
	/// <typeparam name="TMessageDomain"></typeparam>
	/// <typeparam name="TAckProxy"></typeparam>
	public interface ISendMessageHandler<in TMessageDomain, in TAckProxy>
		where TMessageDomain : MessageDomain
		where TAckProxy : IAckRequestAck
	{
		void SendSuccess(TMessageDomain messageDomain, TAckProxy ackProxy);
		void SendFail(TMessageDomain messageDomain, Exception exception);
	}
}