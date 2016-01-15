using System;

namespace DazPaz.Cyclone
{
	public class MagnitudeZeroException : Exception
	{
		public MagnitudeZeroException()
			: base()
		{
		}

		public MagnitudeZeroException(string message)
			: base(message)
		{
		}

		public MagnitudeZeroException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}