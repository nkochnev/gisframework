using System;

namespace GisFramework.Interfaces.Factories
{
	/// <summary>
	/// Фабрика по созданию RequestHeader
	/// </summary>
	/// <typeparam name="TRequestHeader"></typeparam>
	public interface IRequestHeaderFactory<out TRequestHeader>
	{
		TRequestHeader CreateRequestHeader(Guid senderGuid, Guid messageGuid);
	}
}