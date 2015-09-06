using System;

namespace PostgRESTSharp.Shared
{
	public class LocationHeaderParser : ILocationHeaderParser
	{
		public LocationHeaderParser ()
		{
		}

		public T ParseLocationHeader<T>(string primaryKeyColumnName, Uri location)
		{
			primaryKeyColumnName = primaryKeyColumnName.ToLower ();
			return default(T);
		}
	}
}

