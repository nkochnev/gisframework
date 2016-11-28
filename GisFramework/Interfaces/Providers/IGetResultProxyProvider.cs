namespace GisFramework.Interfaces.Providers
{
	/// <summary>
	/// Выполнение запроса на получение результата обработки сообщения
	/// </summary>
	/// <typeparam name="TGetStateResultProxy"></typeparam>
	/// <typeparam name="TResultProxy"></typeparam>
	public interface IGetResultProxyProvider<in TGetStateResultProxy, TResultProxy>
	{
		bool TryGetResult(TGetStateResultProxy getStateResult, out TResultProxy resultProxy);
	}
}