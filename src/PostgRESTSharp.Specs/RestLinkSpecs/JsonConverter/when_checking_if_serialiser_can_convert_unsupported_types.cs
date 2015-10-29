using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;

namespace PostgRESTSharp.Specs.RestLinkSpecs.JsonConverter
{
    public class when_checking_if_serialiser_can_convert_unsupported_types : WithFakes
    {
        Establish that = () =>
        {
            converter = new RestLinkJsonConverter();
            typesToCheck = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch (Exception)
                    {
                        //HACK: skip assemblies that throw when loading (e.g. Machine.Specifications)
                        return Enumerable.Empty<Type>();
                    }
                })
                .Where(a => !typeof(IRestLink).IsAssignableFrom(a));
        };

        private Because of = () =>
        {
            result = typesToCheck.Any(a =>
            {
                var convertResult = converter.CanConvert(a);
                return convertResult;
            });
        };

        private It should_not_be_able_to_convert_any_unsupported_types = () =>
        {
            result.ShouldBeFalse();
        };

        private static RestLinkJsonConverter converter;
        private static bool result;
        private static IEnumerable<Type> typesToCheck;
    }
}