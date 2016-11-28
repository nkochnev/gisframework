using System;
using GisFramework.Data;

namespace GisFramework.Domains
{
	/// <summary>
	/// Базовая доменная сущность сообщения
	/// Расширяя её, указывается какие именно параметры указываются при постановке задания на выполнение действия
	/// </summary>
	public class MessageDomain
	{
		/// <summary>
		/// Состояние выполнения сообщения
		/// </summary>
		public MessageStatus Status { get; set; }
		
		/// <summary>
		/// Идентификатор запроса в информационной системе
		/// </summary>
		public Guid MessageGuid { get; set; }
		
		/// <summary>
		/// Идентификатор запроса ГИС ЖКХ, задается после постановки задания
		/// </summary>
		public Guid? ResponseGuid { get; set; }
		
		/// <summary>
		/// Идентифиткатор организации, чьи данные отправляются в ГИС ЖКХ
		/// Нужно только для ИС, которые отправляют данные других компаний
		/// </summary>
		public int UkId { get; set; }

		/// <summary>
		/// Идентификатор поставщика информации ГИС ЖКХ, чьи данные отправляются
		/// </summary>
		public Guid OrgPPAGUID { get; set; }

		/// <summary>
		/// Сообщение об ошибке
		/// </summary>
		public string ErrorText { get; set; }

		/// <summary>
		/// Дата создания сообщения
		/// </summary>
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Дата отправки сообщения
		/// </summary>
		public DateTime? SendedDate { get; set; }

		/// <summary>
		/// Дата получения результата обработкиы
		/// </summary>
		public DateTime? GetResultDate { get; set; }
	}
}