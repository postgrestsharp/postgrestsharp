using System;

namespace PostgRESTSharp.Shared
{
    public class LocationHeaderParser : ILocationHeaderParser
    {
        public T ParseLocationHeader<T>(string primaryKeyColumnName, Uri location)
        {
            return default(T);
        }
    }
}
