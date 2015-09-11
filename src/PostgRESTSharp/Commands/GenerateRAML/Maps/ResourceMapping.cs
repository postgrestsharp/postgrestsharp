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
    public class ResourceMapping : IMapping
    {
        public void Configure()
        {
            Mapper.CreateMap<IRESTResource, Resource>()
            .ForMember(
                model => model.DisplayName,
                expression => expression.MapFrom(src => src.DisplayName)
            )
            .ForMember(
                model => model.RelativeUri,
                expression => expression.MapFrom(src => string.Format("/{0}", src.Uri))
            )
            .ForMember(
                model => model.Methods,
                expression => expression.MapFrom(src => src.Methods.Where(x => x.VerbDetail == RESTVerbDetailEnum.Collection))
            )
            .ForMember(
                model => model.Resources,
                expression => expression.MapFrom(src => src.Methods.Where(x => x.VerbDetail == RESTVerbDetailEnum.Item).Expand())
            );
        }
    }
}
