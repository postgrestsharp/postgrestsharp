using System;
using System.Data;

namespace PostgRESTSharp.Data
{
	public interface IDbConnectionProvider
	{
		IDbConnection GetConnection();
	}
}

