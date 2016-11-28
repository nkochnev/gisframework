using System;
using GisFramework.Domains;

namespace GisFramework.Interfaces.Handlers
{
	/// <summary>
	/// Обработчик доменной сущности при получении результата обработки
	/// </summary>
	/// <typeparam name="TMessageDomain"></typeparam>
	/// <typeparam name="TResultProxy"></typeparam>
	public interface IGetResultMessageHandler<in TMessageDomain, in TResultProxy> 
		where TMessageDomain : MessageDomain
	{
		/// <summary>
		/// Сообщение помечается как успешно выполненное
		/// </summary>
		/// <param name="messageDomain"></param>
		/// <param name="result"></param>
		void Success(TMessageDomain messageDomain, TResultProxy result);

		/// <summary>
		/// Сообщение помечается как неготовое, результат в ГИС ЖКХ ещё не готов
		/// </summary>
		/// <param name="messageDomain"></param>
		void NotReady(TMessageDomain messageDomain);

		/// <summary>
		/// По сообщению долго не могут получить результат, помечаем как упавшее по таймауту
		/// </summary>
		/// <param name="messageDomain"></param>
		void NoResultByTimeout(TMessageDomain messageDomain);

		/// <summary>
		/// При обработке сообщения произошла ошибка
		/// </summary>
		/// <param name="messageDomain"></param>
		/// <param name="exception"></param>
		void Fail(TMessageDomain messageDomain, Exception exception);
	}
}