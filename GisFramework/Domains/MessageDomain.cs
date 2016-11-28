using System;
using GisFramework.Data;

namespace GisFramework.Domains
{
	public class MessageDomain
	{
		public MessageStatus Status { get; set; }
		public Guid MessageGuid { get; set; }
		public Guid? ResponseGuid { get; set; }
		public int UkId { get; set; }
		public Guid SenderGuid { get; set; }
		public string ErrorText { get; set; }
		public DateTime Created { get; set; }
		public DateTime? Sended { get; set; }
		public DateTime? GetResult { get; set; }
	}
}