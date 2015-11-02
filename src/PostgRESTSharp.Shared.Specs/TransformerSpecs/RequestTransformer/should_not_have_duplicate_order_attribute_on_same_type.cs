using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using NUnit.Framework;
using PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestTransformer.Mock;

namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestTransformer
{
    public class should_not_have_duplicate_order_attribute_on_same_type : WithFakes
    {
        Establish that = () =>
        {
            interfacesToCheck = new[]
            {
                typeof(IQueryStringTransformer),
                typeof(IRequestHeaderTransformer),
                typeof(IMockTransformer), //HACK: allow mock transformers for purposes of this test
            };
        };

        private Because of = () =>
        {
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
                .Where(a => a.GetCustomAttributes(typeof(OrderAttribute), false).Any())
                .ToList();
        };

        private It should_not_have_order_attributes_on_types_not_specified = () =>
        {
            foreach (var item in typesToCheck)
            {
                var isAssignable = interfacesToCheck.Any(a => a.IsAssignableFrom(item));
                if (isAssignable)
                {
                    continue;
                }
                ThrowNotAssignable(item);
            }
        };

        private It should_have_only_one_type_with_each_found_order_value = () =>
        {
            var types = new List<KeyValuePair<Type, Type>>();
            foreach (var interfaceType in interfacesToCheck)
            {
                foreach (var concreteType in typesToCheck)
                {
                    if (!interfaceType.IsAssignableFrom(concreteType))
                    {
                        continue;
                    }
                    types.Add(new KeyValuePair<Type, Type>(interfaceType, concreteType));
                }
            }
            var typesGroupedByInterface = types.GroupBy(a => a.Key);
            foreach (var interfaceGroup in typesGroupedByInterface)
            {
                var orders = interfaceGroup
                    .Select(a => a.Value)
                    .Where(a => a.GetCustomAttributes(typeof(OrderAttribute), false).Any())
                    .GroupBy(a => a.GetCustomAttributes(typeof(OrderAttribute), false).Cast<OrderAttribute>().Single().Order);

                foreach (var item in orders)
                {
                    if (item.Count() <= 1)
                    {
                        continue;
                    }
                    ThrowDuplicateOrderAttributeOnType(interfaceGroup, item);
                }
            }
        };

        private static void ThrowDuplicateOrderAttributeOnType(IGrouping<Type, KeyValuePair<Type, Type>> interfaceGroup, IGrouping<int, Type> item)
        {
            var message = string.Format("There are multiple implementors of {0} that have the same {1} value of {2}.",
                interfaceGroup.Key.Name,
                typeof(OrderAttribute).Name,
                item.Key);
            throw new AssertionException(message);
        }

        private static void ThrowNotAssignable(Type item)
        {
            var message = string.Format("{0} is not assignable to any of the interfaces on which {1} should be used",
                item.Name,
                typeof(OrderAttribute).Name
                );
            throw new AssertionException(message);
        }

        private static IEnumerable<Type> typesToCheck;
        private static Type[] interfacesToCheck;
    }
}