using System;
using System.Data;

namespace PostgrestSharp.Data
{
	public interface IDbConnectionProvider
	{
		IDbConnection GetConnection();
	}
}

