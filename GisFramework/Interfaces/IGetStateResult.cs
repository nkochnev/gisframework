namespace GisFramework.Interfaces
{
	/// <summary>
	/// Этот интерфейс нужно повесить на GetStateResult каждого сервиса ГИС ЖКХ, с котором производится взаимодействие
	/// Т.к. все классы wcf прокси объетов создаются как partial, это можно без проблем
	/// </summary>
	public interface IGetStateResult
	{
		object[] Items { get; set; }
	}
}