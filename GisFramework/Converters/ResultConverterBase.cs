using GisFramework.Data;
using GisFramework.Exceptions;
using GisFramework.Interfaces;
using GisFramework.Interfaces.Converters;

namespace GisFramework.Converters
{
	public abstract class ResultConverterBase<TResultProxy, TResult> : IResultConverter<TResultProxy, TResult>
		where TResultProxy : IGetStateResult
	{
		protected abstract TResult ConvertToResult(TResultProxy resultProxy);

		public TResult ToResult(TResultProxy resultProxy)
		{
			ValidateResult(resultProxy.Items);
			return ConvertToResult(resultProxy);
		}

		/// <summary>
		/// Проверка ответа на корректность. 
		/// Если найдена ошибка, бросаем исключение для завершения обработки этого запроса
		/// </summary>
		/// <param name="items"></param>
		private void ValidateResult(object[] items)
		{
			if (items == null || items.Length == 0)
			{
				throw new IntegrationServerErrorException($"Ответ не содержит результатов импорта или ошибок");
			}
			if (items.Length == 1 && items[0] is ErrorMessageType)
			{
				var err = (ErrorMessageType)items[0];
				throw new IntegrationServerErrorException(err.ErrorCode, err.Description, err.StackTrace);
			}
		}
	}
}