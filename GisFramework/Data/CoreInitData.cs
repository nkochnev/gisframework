namespace GisFramework.Data
{
	/// <summary>
	/// Объект, котороый содержит данные для начала работы CoreService 
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

		/// <summary>
		/// Идентификатор поставщика информации, по которому начинается взаимодействие
		/// </summary>
		public int UkId { get; set; }

		public override string ToString()
		{
			return $"UkId={UkId}";
		}
	}
}