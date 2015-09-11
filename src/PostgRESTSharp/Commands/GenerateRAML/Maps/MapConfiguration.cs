using AutoMapper;
using Raml.Parser.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRAML.Maps
{
    public class MapConfiguration : IMapConfiguration
    {
        IEnumerable<IMapping> maps;

        public MapConfiguration(IEnumerable<IMapping> maps)
        {
            this.maps = maps;
        }

        public void ConfigureAllMappings()
        {
            foreach (var map in maps)
            {
                map.Configure();
            }
        }

        public U Transform<T, U>(T model)
        {
            return Mapper.Map<T, U>(model);
        }
    }

    public interface IMapConfiguration
    {
        void ConfigureAllMappings();

        U Transform<T,U>(T model);
    }
    public interface IMapping
    {
        void Configure();
    }
}
