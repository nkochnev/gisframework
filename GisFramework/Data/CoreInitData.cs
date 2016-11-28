namespace GisFramework.Data
{
	/// <summary>
	/// Объект, котороый содержит данные для начала работы CoreService
	/// Надо бы придумать другое название
	/// </summary>
	public class CoreInitData
	{
		public CoreInitData()
		{
			
		}

		public CoreInitData(int ukId)
		{
			UkId = ukId;
		}

		public int UkId { get; set; }

		public override string ToString()
		{
			return $"UkId={UkId}";
		}
	}
}