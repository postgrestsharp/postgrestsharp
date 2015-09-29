using PostgRESTSharp.REST;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes.Templates
{
    public partial class NancyRESTRoute
    {
        public NancyRESTRoute(RESTResource resource, string fileNamespace, string modelNamespace, string lowerErrorHandlingMode)
        {
            this.Resource = resource;
            this.Namespace = fileNamespace;
            this.ModelNamespace = modelNamespace;
            this.ClassName = resource.ModelName + "Module";
            this.GETModelName = resource.ModelName + "GETModel";
            this.POSTModelName = resource.ModelName + "POSTModel";
            this.POSTResponseModelName = resource.ModelName + "POSTResponseModel";
            this.ErrorHandlingMode = lowerErrorHandlingMode;
        }

        public string ErrorHandlingMode { get; set; }

        public string Namespace { get; protected set; }

        public string ModelNamespace { get; protected set; }

        public string ClassName { get; protected set; }

        public string GETModelName { get; protected set; }

        public string POSTModelName { get; protected set; }

        public string POSTResponseModelName { get; protected set; }

        public RESTResource Resource { get; protected set; }

        public string GetVerbString(RESTVerbEnum verb)
        {
            switch (verb)
            {
                case RESTVerbEnum.GET:
                    return "Get";

                case RESTVerbEnum.POST:
                    return "Post";

                case RESTVerbEnum.PUT:
                    return "Put";

                case RESTVerbEnum.PATCH:
                    return "Patch";

                case RESTVerbEnum.DELETE:
                    return "Delete";

                case RESTVerbEnum.OPTIONS:
                    return "Options";

                default:
                    return "";
            }
        }

        public string GetParameters(IEnumerable<RESTParameter> parameters)
        {
            return "/" + string.Join("/", parameters.Select(x =>
                     string.Format("{{{0}:{1}}}", x.Name, x.Type)
            ));
        }
    }
}