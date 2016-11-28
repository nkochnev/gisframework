using System;

namespace GisFramework.Interfaces.Services
{
	/// <summary>
	/// Сервис, который возвращает OrgPPAGUID по идентификатору поставщика информации
	/// Предполагается, что у нас уже есть справочник соответствий: ИД поставщика информации - OrgPPAGUID
	/// </summary>
	public interface IOrgPPAGUIDService
	{
		Guid GetOrgPPAGUID(int ukId);
	}
}