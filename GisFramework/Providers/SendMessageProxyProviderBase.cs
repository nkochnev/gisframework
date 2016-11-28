using System;
using GisFramework.Exceptions;
using GisFramework.Interfaces;
using GisFramework.Interfaces.Providers;

namespace GisFramework.Providers
{
	/// <summary>
	/// Базовый провайдер для выполнения запросов к ГИС ЖКХ
	/// </summary>
	/// <typeparam name="TMessageProxy"></typeparam>
	/// <typeparam name="TAckProxy"></typeparam>
	public abstract class SendMessageProxyProviderBase<TMessageProxy, TAckProxy> : ISendMessageProxyProvider<TMessageProxy, TAckProxy>
		where TAckProxy : IAckRequestAck
	{
		/// <summary>
		/// Метод вызова сервиса ГИС ЖКХ
		/// </summary>
		/// <param name="messageProxy"></param>
		/// <returns></returns>
		protected abstract TAckProxy Send(TMessageProxy messageProxy);

		/// <summary>
		/// Выполняет запрос к ГИС ЖКХ, обрабатывая ошибки
		/// </summary>
		/// <param name="messageProxy"></param>
		/// <returns></returns>
		public TAckProxy SendMessage(TMessageProxy messageProxy)
		{
			try
			{
				return Send(messageProxy);
			}
			catch (TimeoutException timeoutException)
			{
				throw new IntegrationServerErrorException("TimeoutException", timeoutException);
			}
		}
	}
}