using System;

namespace GisFramework.Interfaces.Services
{
	public interface IGisLogger
	{
		void Info(Type type, string message);
		void Error(Type type, string message, Exception exception);
		void Error(Exception exception);
	}
}