using GisFramework.Domains;

namespace GisFramework.Interfaces.Converters
{
	/// <summary>
	/// Преобразование доменной сущности в запрос на получение результата обработки
	/// </summary>
	/// <typeparam name="TGetStateResultProxy"></typeparam>
	/// <typeparam name="TMessageDomain"></typeparam>
	public interface IGetStateProxyConverter<out TGetStateResultProxy, in TMessageDomain>
		where TMessageDomain : MessageDomain
	{
		TGetStateResultProxy ToGetStateResultProxy(TMessageDomain messageDomain);
	}
}