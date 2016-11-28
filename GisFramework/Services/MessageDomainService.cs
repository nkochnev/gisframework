using System.Collections.Generic;
using System.Linq;
using GisFramework.Data;
using GisFramework.Domains;
using GisFramework.Interfaces;
using GisFramework.Interfaces.Services;

namespace GisFramework.Services
{
	public class MessageDomainService<TMessageDomain> : IMessageDomainService<TMessageDomain> where TMessageDomain : MessageDomain
	{
		private readonly IRepository<TMessageDomain> _messageRepository;

		public MessageDomainService(IRepository<TMessageDomain> messageRepository)
		{
			_messageRepository = messageRepository;
		}

		public void InsertMessageDomains(List<TMessageDomain> messageDomains)
		{
			_messageRepository.InsertRange(messageDomains);
		}

		public List<TMessageDomain> GetMessageDomainsForSend(CoreInitData coreInitData)
		{
			var notSendedStatuses = new List<MessageStatus>()
			{
				MessageStatus.New,
				MessageStatus.SendErrorTryAgain
			};
			return _messageRepository.Table.Where(x => x.UkId == coreInitData.UkId && notSendedStatuses.Contains(x.Status) && !x.ResponseGuid.HasValue).ToList();
		}

		public List<TMessageDomain> GetMessageDomainsForGetResults(CoreInitData coreInitData)
		{
			var inProcessStatuses = new List<MessageStatus>()
			{
				MessageStatus.Sent,
				MessageStatus.InProcess,
				MessageStatus.Received,
				MessageStatus.ResponseTakingErrorTryAgain
			};
			return _messageRepository.Table.Where(x => x.UkId == coreInitData.UkId && (inProcessStatuses.Contains(x.Status) || (x.Status == MessageStatus.New && x.ResponseGuid.HasValue))).ToList();
		}

		public void Update(TMessageDomain messageDomain)
		{
			_messageRepository.Update(messageDomain);
		}

		public TMessageDomain GetMessageDomainById(int id)
		{
			return _messageRepository.GetById(id);
		}
	}
}