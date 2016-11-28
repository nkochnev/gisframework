namespace GisFramework.Interfaces.Providers
{
	/// <summary>
	/// Выполнение отправки сообщения
	/// </summary>
	/// <typeparam name="TMessageProxy"></typeparam>
	/// <typeparam name="TAckProxy"></typeparam>
	public interface ISendMessageProxyProvider<in TMessageProxy, out TAckProxy> 
		where TAckProxy : IAckRequestAck
	{
		TAckProxy SendMessage(TMessageProxy messageProxy);
	}
}