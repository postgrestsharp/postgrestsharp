using System;

namespace PostgRESTSharp.Configuration
{
	public class SimpleConnectionStringConfigurationProvider : IConnectionStringConfigurationProvider
	{
		public string ConnectionString
		{
			get;
			set;
		}
	}
}

