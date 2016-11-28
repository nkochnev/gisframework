namespace GisFramework.Interfaces.Converters
{
	/// <summary>
	/// Преобразование результата обработаки в бизнес-сущность результата обработки
	/// </summary>
	/// <typeparam name="TResultProxy"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	public interface IResultConverter<in TResultProxy, out TResult>
		where TResultProxy : IGetStateResult
	{
		TResult ToResult(TResultProxy resultProxy);
	}
}