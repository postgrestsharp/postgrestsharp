using System;

namespace PostgRESTSharp.Configuration
{
	public interface IConnectionStringConfigurationProvider
	{
		string ConnectionString { get; set;  }
	}
}

