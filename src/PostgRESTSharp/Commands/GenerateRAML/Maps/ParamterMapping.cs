using AutoMapper;
using PostgRESTSharp.REST;
using Raml.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRAML.Maps
{
    public class ParamterMapping : IMapping
    {
        public void Configure()
        {
            Mapper.CreateMap<RESTParameter, IDictionary<string, Parameter>>()
            .ConvertUsing(x => GetDictionaryFromParamter(x));
        }

        //had to be done
        private IDictionary<string, Parameter> GetDictionaryFromParamter(RESTParameter param)
        {
            IDictionary<string, Parameter> dictionary = new Dictionary<string, Parameter>();
            string type = param.Type;
            switch (param.Type)
            {
                case "int":
                    type = "integer";
                    break;
                default:
                    break;
            }
            dictionary.Add(param.Name, new Parameter() { DisplayName = param.Name, Required = param.IsRequired, Type = type});
            
            return dictionary;
        }
    }
}
