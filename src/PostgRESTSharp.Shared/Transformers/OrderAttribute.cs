using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Shared
{
    public class OrderAttribute : Attribute
    {
        public int Order { get; private set; }

        public OrderAttribute(int order)
        {
            this.Order = order;
        }
    }

    public static class OrderAttributeExtensions
    {
        public static IOrderedEnumerable<T> OrderByOrderAttribute<T>(this IEnumerable<T> queryStringTransformers)
        {
            return queryStringTransformers.OrderBy(a =>
            {
                var orderAttribute = a
                    .GetType()
                    .GetCustomAttributes(typeof(OrderAttribute), true)
                    .Cast<OrderAttribute>()
                    .SingleOrDefault();

                return orderAttribute == null ? int.MaxValue : orderAttribute.Order;
            });
        }
    }
}
