# Базовые классы для асинхронного взаимодействия с ГИС ЖКХ


**Порядок реализации сервисов**

1. CreateMessageCoreService\<TMessageDomain, TSourceDomain\> - создание сообщений
  * ISourceService\<TSourceDomain\> - подъем данных из информационной системы
  * IOrgPPAGUIDService - получение OrgPPAGUID поставщика информации
  * IMessageDomainConverter\<TMessageDomain, TSourceDomain\> - преобразование объекта информационной системы к доменному сообщению с возможной разбивкой на пачки

2. SendMessageCoreService\<TMessageDomain, TMessageProxy, TAckProxy\> - отправка сообщений
  * IMessageProxyConverter\<TMessageDomain, TMessageProxy\> - преобразование доменных сообщений к wcf прокси объекта
  * ISendMessageProxyProvider\<TMessageProxy, TAckProxy\> - отправка сообщения
  * ISendMessageHandler\<TMessageDomain, TAckProxy\> - обработка отправки
  
3. GetResultsCoreService\<TMessageDomain, TGetStateResultProxy, TResultProxy, TResult\> - получение результата 
  * IGetStateProxyConverter\<TGetStateResultProxy, TMessageDomain\> - преобразование доменного сообщения к wcf прокси объекту запроса состояния
  * IGetResultProxyProvider\<TGetStateResultProxy, TResultProxy\> - отправка сообщения о проверке состояния
  * IResultConverter\<TResultProxy, TResult\> - преобразование результата прокси объекта ГИС ЖКХ к бизнес сущности, сохраняется только необходимое
  * ISaveResultService\<TResult, TMessageDomain\> - сохранение результата обработки 
  * IGetResultMessageHandler\<TMessageDomain, TResult\> - обработка получения результата
