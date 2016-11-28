using System;
using System.Collections.Generic;
using GisFramework.Data;
using GisFramework.Domains;

namespace GisFramework.Interfaces.Converters
{
	/// <summary>
	/// Конвертер объектов, вернувшихся из информационной системы в доменные сообщения
	/// </summary>
	/// <typeparam name="TMessageDomain"></typeparam>
	/// <typeparam name="TSourceDomain"></typeparam>
	public interface IMessageDomainConverter<TMessageDomain, TSourceDomain>
		where TMessageDomain : MessageDomain
	{
		/// <summary>
		/// При отправке данных в ГИС ЖКХ для каждых N TSourceDomain создается одно доменное сообщение
		/// При получении данных из ГИС ЖКХ часто для каждого TSourceDomain создается TMessageDomain
		/// </summary>
		/// <param name="sourceDomains"></param>
		/// <param name="coreInitData"></param>
		/// <param name="senderGuid"></param>
		/// <returns></returns>
		List<TMessageDomain> ToMessageDomain(List<TSourceDomain> sourceDomains, CoreInitData coreInitData, Guid senderGuid);
	}
}