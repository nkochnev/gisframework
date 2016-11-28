namespace GisFramework.Interfaces.Providers
{
	/// <summary>
	/// Класс выполнения запросов на выполнение действия в ГИС ЖКХ
	/// </summary>
	/// <typeparam name="TMessageProxy">Тип отправляемого WCF прокси объекта</typeparam>
	/// <typeparam name="TAckProxy">Тип Ack для сервиса ГИС ЖКХ</typeparam>
	public interface ISendMessageProxyProvider<in TMessageProxy, out TAckProxy> 
		where TAckProxy : IAckRequestAck
	{
		TAckProxy SendMessage(TMessageProxy messageProxy);
	}
}