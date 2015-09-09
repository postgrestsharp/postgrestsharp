using NUnit.Specifications;
using PostgRESTSharp.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Specs.ConventionResolverSpecs
{
    public class when_initialising_the_resolver_with_conventions : ContextSpecification
    {
        static IConventionResolver conventionResolver;
        static IList<IConvention> conventions;

        Establish context = () =>
        {
            // setup some conventions
            conventions = new List<IConvention>();
            conventions.Add(new DefaultViewInclusionConvention());
            conventions.Add(new TableExclusionViewInclusionConvention("halo", "public", "applications"));
        };

        Because of = () =>
        {
            conventionResolver = new ConventionResolver(conventions);
        };

        It should = () =>
        {

        };
    }
}
