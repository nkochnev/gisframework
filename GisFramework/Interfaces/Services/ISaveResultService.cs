using GisFramework.Domains;

namespace GisFramework.Interfaces.Services
{
	/// <summary>
	/// Сервис сохранения результата обработки сообщения
	/// Например, тут сохраняется привязки объектов ГИС ЖКХ и ИС или сохраняется полученный объект из ГИС ЖКХ
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	/// <typeparam name="TMessageDomain"></typeparam>
	public interface ISaveResultService<in TResult, in TMessageDomain> 
		where TMessageDomain : MessageDomain
	{
		void SaveResult(TResult result, TMessageDomain messageDomain);
	}
}