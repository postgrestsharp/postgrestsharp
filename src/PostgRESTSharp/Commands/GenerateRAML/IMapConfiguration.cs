using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRAML
{
    public interface IMapConfiguration
    {
        void ConfigureAllMappings();

        U Transform<T, U>(T model);
    }
}
