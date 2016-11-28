using System;
using GisFramework.Interfaces.Services;

namespace GisFramework.Interfaces.Factories
{
	public interface IServiceFactory
	{
		ICoreService GetCoreService(Type type);
		IBusinessService GetBusinessService(Type type);
	}
}