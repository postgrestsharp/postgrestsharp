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
    //watch out for the glass box
    public class MimeMapping : IMapping
    {
        public void Configure()
        {
            Mapper.CreateMap<RESTResponseDefinition, MimeType>()
            .ForMember(
                model => model.Schema,
                expression => expression.MapFrom(src => src.ResponseSchema)
            )
            .ForMember(
                model => model.Example,
                expression => expression.MapFrom(src => src.ResponseExample)
            );
        }
    }
}
