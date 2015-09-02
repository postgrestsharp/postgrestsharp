using System.Data;
using PostgRESTSharp.Configuration;
using PostgRESTSharp.Data;

namespace PostgRESTSharp.Pgsql
{
    public class PgSqlDbConnectionProvider : IDbConnectionProvider
    {
		private IConnectionStringConfigurationProvider connectionStringConfigurationProvider;

		public PgSqlDbConnectionProvider(IConnectionStringConfigurationProvider connectionStringConfigurationProvider)
        {
			this.connectionStringConfigurationProvider = connectionStringConfigurationProvider;
        }

        public IDbConnection GetConnection()
        {
            return new Npgsql.NpgsqlConnection(this.connectionStringConfigurationProvider.ConnectionString);
        }
    }
}