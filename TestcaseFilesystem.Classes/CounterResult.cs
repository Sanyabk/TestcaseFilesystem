using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestcaseFilesystem.Classes
{
	public class CounterResult
	{
		public double Small { get; set; }
		public double Medium { get; set; }
		public double Large { get; set; }

		public CounterResult()
		{
			Small = 0; Medium = 0; Large = 0;
		}

		public static CounterResult operator +(CounterResult a, CounterResult b)
		{
			return new CounterResult()
			{
				Small = a.Small + b.Small,
				Medium = a.Medium + b.Medium,
				Large = a.Large + b.Large,
			};
		}
	}
}