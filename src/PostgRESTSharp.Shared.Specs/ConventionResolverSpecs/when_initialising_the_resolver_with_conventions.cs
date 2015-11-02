using System.Collections.Generic;
using NUnit.Specifications;
using PostgRESTSharp.Conventions;

namespace PostgRESTSharp.Shared.Specs.ConventionResolverSpecs
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
            conventionResolver = new ConventionResolver();
        };

        Because of = () =>
        {
            conventionResolver.Initialise(conventions);
        };

        It should = () =>
        {

        };
    }
}
