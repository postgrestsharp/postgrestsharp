using AutoMapper;
using PostgRESTSharp.Commands.GenerateRAML.Transitions;
using PostgRESTSharp.REST;
using Raml.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRAML.Maps
{
    public class MethodMapping : IMapping
    {
        public void Configure()
        {
            Mapper.CreateMap<RESTMethod, Method>()
            .ForMember(
                model => model.Verb,
                expression => expression.MapFrom(src => src.Verb.ToString().ToLower())
            )
            .ForMember(
                model => model.Description,
                expression => expression.MapFrom(src => string.Format("{0} for a {1}.", src.Verb.ToString().ToLower(), src.VerbDetail))
            )
            .ForMember(
                model=>model.Description,
                expression => expression.MapFrom(src=>src.Description)
            )
            .ForMember(
                model=> model.Responses,
                expression => expression.MapFrom(src => src.ResponseDefinitions)
            );

            Mapper.CreateMap<RAMLNestedResource, Resource>()
            .ForMember(
                model => model.RelativeUri,
                expression => expression.MapFrom(x => string.Format("/{{{0}}}", x.URIParameter.Name))
            )
            .ForMember(
                model => model.Methods,
                expression => expression.MapFrom(x => new List<RESTMethod>() { x.Method })
            )
            .ForMember(
                model => model.UriParameters,
                expression => expression.MapFrom(x => x.URIParameter)
            );
        }
    }
}
