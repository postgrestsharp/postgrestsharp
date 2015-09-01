using System.Data;
using PostgrestSharp.Configuration;
using PostgrestSharp.Data;

namespace PostgrestSharp.Pgsql
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