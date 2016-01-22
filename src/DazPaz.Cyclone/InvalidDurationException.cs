using System;

namespace DazPaz.Cyclone
{
	public class InvalidDurationException : Exception
	{
		public InvalidDurationException()
		{
		}

		public InvalidDurationException(string message) : base(message)
		{
		}

		public InvalidDurationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
