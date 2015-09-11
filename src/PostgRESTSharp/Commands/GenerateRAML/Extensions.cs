using PostgRESTSharp.Commands.GenerateRAML.Transitions;
using PostgRESTSharp.REST;
using Raml.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    public static class Extensions
    {
        public static RamlDocument New(string baseURI,string title,string version)
        {
            RamlDocument document = new RamlDocument();
            document.BaseUri = baseURI;
            document.Title = title;
            document.Version = version;
            return document;
        }

        //please find another way :(
        public static IEnumerable<RAMLNestedResource> Expand(this IEnumerable<RESTMethod> methods)
        {
            IList<RAMLNestedResource> nestedResources = new List<RAMLNestedResource>();
            foreach (var method in methods)
            {
                foreach (var uriParameter in method.URIParameters)
                {
                    var resource = new RAMLNestedResource(method.Verb, method.VerbDetail, method,uriParameter);
                    nestedResources.Add(resource);
                }
            }
            return nestedResources;
        }
    }
}
