using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Shared
{
    public class PostgRestHttpRequestFactory : IHttpFactory
    {
        public IHttp Create()
        {
            return new PostgRestHttpRequest();
        }
    }
}
