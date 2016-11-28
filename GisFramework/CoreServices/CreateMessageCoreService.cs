using System;
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
	/// <typeparam name="TSourceDomain">Тип исходных данных</typeparam>
	public class CreateMessageCoreService<TMessageDomain, TSourceDomain> : ICoreService
		where TMessageDomain : MessageDomain
	{
		private readonly ISourceService<TSourceDomain> _sourceService;
		private readonly IMessageDomainConverter<TMessageDomain, TSourceDomain> _messageDomainConverter;
		private readonly IMessageDomainService<TMessageDomain> _messageDomainService;
		private readonly ISenderIdService _senderIdService;
		private readonly IGisLogger _logger;

		private DateTime _startDateTime;

		public CreateMessageCoreService(ISourceService<TSourceDomain> sourceService,
			IMessageDomainConverter<TMessageDomain, TSourceDomain> messageDomainConverter,
			IMessageDomainService<TMessageDomain> messageDomainService,
			ISenderIdService senderIdService, IGisLogger logger)
		{
			_sourceService = sourceService;
			_messageDomainConverter = messageDomainConverter;
			_messageDomainService = messageDomainService;
			_senderIdService = senderIdService;
			_logger = logger;
		}

		public void Do(CoreInitData coreInitData)
		{
			_startDateTime = DateTime.Now;
			try
			{
				//получаем исходные данные для взаимодействия
				//может быть массив объектов для отправки, а может быть исходные данные для получения информации
				var sourceDomains = _sourceService.GetSourceDomains(coreInitData);
				//получаем senderId по УК
				var senderId = _senderIdService.GetSenderId(coreInitData.UkId);
				//по исходным данным создаем сообщения для отправки
				//передается сразу коллекция исходных объектов, т.к. внутри ToMessageDomain происходит разбитие объектов на сообщения
				var messages = _messageDomainConverter.ToMessageDomain(sourceDomains, coreInitData, senderId);
				//сохраняем сообщения в базу данных
				_messageDomainService.InsertMessageDomains(messages);

				var duration = DateTime.Now.Subtract(_startDateTime);
				_logger.Info(this.GetType(), $"Создано {messages.Count} доменных сообщений по УК {coreInitData.UkId} за {duration}");
			}
			catch (Exception ex)
			{
				_logger.Error(this.GetType(), $"Произошло исключение при обработке {coreInitData}", ex);
			}
		}
	}
}