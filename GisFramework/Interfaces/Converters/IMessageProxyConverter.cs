using GisFramework.Domains;

namespace GisFramework.Interfaces.Converters
{
	/// <summary>
	/// Преобразование доменного сообщения в WCF прокси объект сообщения
	/// </summary>
	/// <typeparam name="TMessageDomain"></typeparam>
	/// <typeparam name="TMessageProxy">Тип WCF прокси объекта</typeparam>
	public interface IMessageProxyConverter<in TMessageDomain, out TMessageProxy>
		where TMessageDomain : MessageDomain
	{
		TMessageProxy ToMessageProxy(TMessageDomain messageDomain);
	}
}