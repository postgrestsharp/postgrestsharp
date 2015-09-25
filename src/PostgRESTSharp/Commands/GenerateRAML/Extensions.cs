using PostgRESTSharp.Commands.GenerateRAML.Transitions;
using PostgRESTSharp.REST;
using Raml.Parser;
using Raml.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static RamlDocument LoadRamlDocument(string fileName)
        {

            if (File.Exists(fileName))
            {
                RamlParser ramlParser = new RamlParser();
                try
                {
                    var ramlDocument = ramlParser.LoadAsync(fileName).Result;
                    return ramlDocument;
                }
                catch (Exception exc)
                {
                    Console.WriteLine("An error has occured while loading raml document");
                    Console.WriteLine(string.Format("filename  : {0}", fileName));
                    Console.WriteLine(exc.Message);
                    if (exc.InnerException!=null)
                        Console.WriteLine(exc.InnerException.Message);
                }
                
            }
            return null;
        }

        public static IEnumerable<string> FindFiles(string includedRamlDirectory,string filter)
        {
            if (Directory.Exists(includedRamlDirectory))
            {
                IEnumerable<string> fileNames = Directory.GetFiles(includedRamlDirectory,filter);
                return fileNames.Select(x => Path.Combine(includedRamlDirectory, x));
            }
            return null;
        }

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
