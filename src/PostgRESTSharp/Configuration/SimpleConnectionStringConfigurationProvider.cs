using System;

namespace PostgrestSharp.Configuration
{
	public class SimpleConnectionStringConfigurationProvider : IConnectionStringConfigurationProvider
	{
		public SimpleConnectionStringConfigurationProvider (string connectionString)
		{
			this.ConnectionString = connectionString;
		}

		public string ConnectionString
		{
			get;
			protected set;
		}
	}
}

