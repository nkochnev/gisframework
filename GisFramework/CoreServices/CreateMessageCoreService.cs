using System;
using System.Diagnostics;
using GisFramework.Data;
using GisFramework.Domains;
using GisFramework.Interfaces.Converters;
using GisFramework.Interfaces.Services;

namespace GisFramework.CoreServices
{
	/// <summary>
	/// Базовый сервис для первого этапа взаимодействия - создания сообщений
	/// </summary>
	/// <typeparam name="TMessageDomain">Тип доменного сообщения</typeparam>
	/// <typeparam name="TSourceDomain">Тип объекта, который возвращается из информационной системы</typeparam>
	public class CreateMessageCoreService<TMessageDomain, TSourceDomain>
		where TMessageDomain : MessageDomain
	{
		private readonly ISourceService<TSourceDomain> _sourceService;
		private readonly IMessageDomainConverter<TMessageDomain, TSourceDomain> _messageDomainConverter;
		private readonly IMessageDomainService<TMessageDomain> _messageDomainService;
		private readonly IOrgPPAGUIDService _orgPPAGUIDService;
		private readonly IGisLogger _logger;
		
		public CreateMessageCoreService(ISourceService<TSourceDomain> sourceService,
			IMessageDomainConverter<TMessageDomain, TSourceDomain> messageDomainConverter,
			IMessageDomainService<TMessageDomain> messageDomainService,
			IOrgPPAGUIDService orgPPAGUIDService, IGisLogger logger)
		{
			_sourceService = sourceService;
			_messageDomainConverter = messageDomainConverter;
			_messageDomainService = messageDomainService;
			_orgPPAGUIDService = orgPPAGUIDService;
			_logger = logger;
		}

		public void CreateMessages(CoreInitData coreInitData)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			try
			{
				//получаем данные из информационной системы, по которым нужно осуществить взаимодействие
				var sourceDomains = _sourceService.GetSourceDomains(coreInitData);
				//получаем senderId по УК
				var orgPPAGUID = _orgPPAGUIDService.GetOrgPPAGUID(coreInitData.UkId);
				//по исходным данным создаем доменные сообщения 
				var messages = _messageDomainConverter.ToMessageDomain(sourceDomains, coreInitData, orgPPAGUID);
				//сохраняем сообщения в базу данных
				_messageDomainService.InsertMessageDomains(messages);

				stopWatch.Stop();
				_logger.Info(this.GetType(), $"Создано {messages.Count} доменных сообщений по УК {coreInitData.UkId} за {stopWatch.Elapsed}");
			}
			catch (Exception ex)
			{
				_logger.Error(this.GetType(), $"Произошло исключение при обработке {coreInitData}", ex);
			}
		}
	}
}