﻿namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestTransformer.Mock
{
    public interface IMockTransformer
    {
    }

    [Order(1)]
    public class TransformerWithOrderOf1 : IMockTransformer
    {
    }

    [Order(2)]
    public class TransformerWithOrderOf2 : IMockTransformer
    {
    }

    [Order(3)]
    public class TransformerWithOrderOf3 : IMockTransformer
    {
    }

    [Order(4)]
    public class TransformerWithOrderOf4 : IMockTransformer
    {
    }

    [Order(5)]
    public class TransformerWithOrderOf5 : IMockTransformer
    {
    }
}
