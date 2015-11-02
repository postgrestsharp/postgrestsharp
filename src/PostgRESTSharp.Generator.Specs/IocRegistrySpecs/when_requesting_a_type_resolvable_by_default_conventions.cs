using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

//using StructureMap;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Generator.Specs.IocRegistrySpecs.Mock;
using StructureMap;

namespace PostgRESTSharp.Generator.Specs.IocRegistrySpecs
{
    public class when_requesting_a_type_resolvable_by_default_conventions : WithFakes
    {
        Establish that = () =>
        {
            container = new Container(new IocRegistry());
        };


        private Because of = () =>
        {
            instance = container.TryGetInstance<ITestDefaultConventions>();
        };

        private It should_have_resolved_an_instance = () =>
        {
            instance.ShouldNotBeNull();
        };

        private static Container container;
        private static ITestDefaultConventions instance;
        private static IocRegistry registry;
    }
}
