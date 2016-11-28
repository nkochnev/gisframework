using System;
using System.Runtime.Serialization;

namespace GisFramework.Exceptions
{
	/// <summary>
	/// Исключение, которое бросаем, когда ГИС ЖКХ сообщает об ошибке
	/// </summary>
	[Serializable]
	public class IntegrationServerErrorException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public IntegrationServerErrorException()
		{
		}

		public IntegrationServerErrorException(string errorCode, string errorDescription, string stackTrace) : base($"{errorCode}: {errorDescription}. {stackTrace}")
		{
			
		}

		public IntegrationServerErrorException(string message) : base(message)
		{
		}

		public IntegrationServerErrorException(string message, Exception inner) : base(message, inner)
		{
		}

		protected IntegrationServerErrorException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}