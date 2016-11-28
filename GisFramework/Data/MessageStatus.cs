namespace GisFramework.Data
{
	public enum MessageStatus
	{
		/// <summary>
		/// Новое сообщение
		/// </summary>
		New = 0,

		/// <summary>
		/// Ошибка во время отправки
		/// </summary>
		SendError = 1,

		/// <summary>
		/// Отправлен
		/// </summary>
		Sent = 2,

		/// <summary>
		/// Запрос получен сервером
		/// </summary>
		Received = 3,

		/// <summary>
		/// Обрабатывается сервером
		/// </summary>
		InProcess = 4,

		/// <summary>
		/// Ошибка получения ответа
		/// </summary>
		ResponseTakingError = 5,

		/// <summary>
		/// Получен ответ с ошибками
		/// </summary>
		ResponseTakenWithErrors = 6,

		/// <summary>
		/// Сообщение отправленно и подтверженно
		/// </summary>
		Done = 7,

		/// <summary>
		/// Ошибка во время отправки, но можно попробовать ещё
		/// </summary>
		SendErrorTryAgain = 8,

		/// <summary>
		/// Ошибка получения ответа, но можно попробовать ещё раз
		/// </summary>
		ResponseTakingErrorTryAgain = 9,

		/// <summary>
		/// В течение таймаута не можем получить результат обработки сообщения
		/// </summary>
		NoResultByTimeout = 10
	}
}