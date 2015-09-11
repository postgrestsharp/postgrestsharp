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
    public class ResponseMapping : IMapping
    {
        public void Configure()
        {
            Mapper.CreateMap<RESTResponseDefinition, Response>()
            .ForMember(
                model => model.Code,
                expression => expression.MapFrom(src => (int)src.StatusCode)
            )
            .ForMember(
                model=>model.Description,
                expression => expression.MapFrom(src=>src.ResponseSchema)
            )
            .ForMember(
                model => model.Body,
                expression => expression.MapFrom(src => new Dictionary<string,RESTResponseDefinition>(){
                    {"application/javascript",src}
                })
            )
            .ForMember(
                model=>model.Headers,
                expression=>expression.Ignore()
            );
        }
    }
}
