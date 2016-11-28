using System.Collections.Generic;
using GisFramework.Data;

namespace GisFramework.Interfaces.Services
{
	/// <summary>
	/// Сервис получения исходных данных
	/// </summary>
	/// <typeparam name="TSourceDomain"></typeparam>
	public interface ISourceService<TSourceDomain>
	{
		List<TSourceDomain> GetSourceDomains(CoreInitData coreInitData);
	}
}