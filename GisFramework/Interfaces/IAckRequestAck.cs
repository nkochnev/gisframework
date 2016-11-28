using System;

namespace GisFramework.Interfaces
{
	/// <summary>
	/// Этот интерфейс нужно повесить на AckRequestAck каждого сервиса ГИС ЖКХ, с котором производится взаимодействие
	/// Т.к. все классы wcf прокси объетов создаются как partial, это можно без проблем
	/// </summary>
	public interface IAckRequestAck
	{
		string MessageGUID { get; set; }
	}
}