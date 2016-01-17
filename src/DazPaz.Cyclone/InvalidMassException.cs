using System;

namespace DazPaz.Cyclone
{
	public class InvalidMassException : Exception
	{
		public InvalidMassException() : base()
		{
		}

		public InvalidMassException(string message) : base(message)
		{
		}

		public InvalidMassException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
