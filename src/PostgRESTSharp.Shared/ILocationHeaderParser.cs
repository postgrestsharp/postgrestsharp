using System;

namespace PostgRESTSharp.Shared
{
	public interface ILocationHeaderParser
	{
		T ParseLocationHeader<T>(string primaryKeyColumnName, Uri location);
	}
}

