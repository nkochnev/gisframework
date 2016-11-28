using System;
using System.Collections.Generic;
using GisFramework.Data;
using GisFramework.Domains;

namespace GisFramework.Interfaces.Converters
{
	/// <summary>
	/// Преобразование исходных данных в доменные сообщения
	/// </summary>
	/// <typeparam name="TMessageDomain"></typeparam>
	/// <typeparam name="TSourceDomain"></typeparam>
	public interface IMessageDomainConverter<TMessageDomain, TSourceDomain>
		where TMessageDomain : MessageDomain
	{
		List<TMessageDomain> ToMessageDomain(List<TSourceDomain> sourceDomains, CoreInitData coreInitData, Guid senderGuid);
	}
}