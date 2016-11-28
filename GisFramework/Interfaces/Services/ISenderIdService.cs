using System;

namespace GisFramework.Interfaces.Services
{
	public interface ISenderIdService
	{
		Guid GetSenderId(int ukId);
	}
}