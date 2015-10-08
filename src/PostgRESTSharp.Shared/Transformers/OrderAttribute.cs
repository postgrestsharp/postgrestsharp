using System;

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
}