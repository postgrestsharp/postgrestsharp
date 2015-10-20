using System;
using RestSharp;

namespace PostgRESTSharp.Shared
{
    public class RestRequestFactory : IRestRequestFactory
    {
        private readonly Func<IRestRequest> constructionAction;

        public RestRequestFactory(Func<IRestRequest> constructionAction)
        {
            this.constructionAction = constructionAction;
        }

        public IRestRequest Create()
        {
            return constructionAction();
        }
    }
}
