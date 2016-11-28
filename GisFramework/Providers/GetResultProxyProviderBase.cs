using System;
using GisFramework.Exceptions;
using GisFramework.Interfaces.Providers;

namespace GisFramework.Providers
{
	/// <summary>
	/// Базовый провайдер для выполнения запросов на получение результата обработки из ГИС ЖКХ
	/// </summary>
	/// <typeparam name="TGetStateResultProxy"></typeparam>
	/// <typeparam name="TResultProxy"></typeparam>
	public abstract class GetResultProxyProviderBase<TGetStateResultProxy, TResultProxy> : IGetResultProxyProvider<TGetStateResultProxy, TResultProxy>
	{
		/// <summary>
		/// Метод вызова сервисов ГИС ЖКХ
		/// </summary>
		/// <param name="getStateResult"></param>
		/// <param name="resultProxy"></param>
		/// <returns></returns>
		protected abstract bool GetStateResult(TGetStateResultProxy getStateResult, out TResultProxy resultProxy);

		/// <summary>
		/// Выполняет запрос к ГИС ЖКХ, обрабатывая ошибки
		/// </summary>
		/// <param name="getStateResult"></param>
		/// <param name="resultProxy"></param>
		/// <returns></returns>
		public bool TryGetResult(TGetStateResultProxy getStateResult, out TResultProxy resultProxy)
		{
			try
			{
				return this.GetStateResult(getStateResult, out resultProxy);
			}
			catch (TimeoutException timeoutException)
			{
				throw new IntegrationServerErrorException("TimeoutException", timeoutException);
			}
		}
	}
}