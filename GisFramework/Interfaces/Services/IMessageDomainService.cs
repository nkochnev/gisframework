using System.Collections.Generic;
using GisFramework.Data;
using GisFramework.Domains;

namespace GisFramework.Interfaces.Services
{
	/// <summary>
	/// Сервис доменного сообщения: получение и обработка
	/// </summary>
	/// <typeparam name="TMessageDomain"></typeparam>
	public interface IMessageDomainService<TMessageDomain>
		where TMessageDomain : MessageDomain
	{
		/// <summary>
		/// Сохранение созданных доменных сообщений в БД
		/// </summary>
		/// <param name="messageDomains"></param>
		void InsertMessageDomains(List<TMessageDomain> messageDomains);

		/// <summary>
		/// Получить сообщения для отправки
		/// </summary>
		/// <returns></returns>
		List<TMessageDomain> GetMessageDomainsForSend(CoreInitData coreInitData);

		/// <summary>
		/// Получить оправленные сообщения для получения результата
		/// </summary>
		/// <returns></returns>
		List<TMessageDomain> GetMessageDomainsForGetResults(CoreInitData coreInitData);

		/// <summary>
		/// Сохранение изменений в сообщении
		/// </summary>
		/// <param name="messageDomain"></param>
		void Update(TMessageDomain messageDomain);

		/// <summary>
		/// Получение доменного сообщения по идентификатору
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		TMessageDomain GetMessageDomainById(int id);
	}
}