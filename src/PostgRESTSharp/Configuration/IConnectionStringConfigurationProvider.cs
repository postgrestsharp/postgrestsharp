using System;

namespace PostgrestSharp.Configuration
{
	public interface IConnectionStringConfigurationProvider
	{
		string ConnectionString { get; }
	}
}

