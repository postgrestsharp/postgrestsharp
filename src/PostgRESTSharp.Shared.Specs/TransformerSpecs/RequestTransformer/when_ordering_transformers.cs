using System.Collections.Generic;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestTransformer.Mock;

namespace PostgRESTSharp.Shared.Specs.TransformerSpecs.RequestTransformer
{
    public class when_ordering_transformers : WithFakes
    {
        Establish that = () =>
        {
            requestHeaderTransformers = new List<object>
            {
                An<IRequestHeaderTransformer>(),    //orderedTransformers[5] -> 0
                An<TransformerWithOrderOf5>(),      //orderedTransformers[4] -> 1
                An<TransformerWithOrderOf1>(),      //orderedTransformers[0] -> 2
                An<IRequestHeaderTransformer>(),    //orderedTransformers[6] -> 3
                An<TransformerWithOrderOf2>(),      //orderedTransformers[1] -> 4
                An<TransformerWithOrderOf4>(),      //orderedTransformers[3] -> 5
                An<IRequestHeaderTransformer>(),    //orderedTransformers[7] -> 6
                An<TransformerWithOrderOf3>(),      //orderedTransformers[2] -> 7
                An<IRequestHeaderTransformer>(),    //orderedTransformers[8] -> 8
            };
        };

        private Because of = () =>
        {
            orderedTransformers = requestHeaderTransformers.OrderByOrderAttribute().ToList();
        };

        private It should_have_ordered_the_transformers_according_to_the_order_attribute = () =>
        {
            orderedTransformers[0].ShouldEqual(requestHeaderTransformers[2]);
            orderedTransformers[1].ShouldEqual(requestHeaderTransformers[4]);
            orderedTransformers[2].ShouldEqual(requestHeaderTransformers[7]);
            orderedTransformers[3].ShouldEqual(requestHeaderTransformers[5]);
            orderedTransformers[4].ShouldEqual(requestHeaderTransformers[1]);

            //types without order attribute, but results should be stable
            orderedTransformers[5].ShouldEqual(requestHeaderTransformers[0]);
            orderedTransformers[6].ShouldEqual(requestHeaderTransformers[3]);
            orderedTransformers[7].ShouldEqual(requestHeaderTransformers[6]);
            orderedTransformers[8].ShouldEqual(requestHeaderTransformers[8]);
        };

        private static List<object> requestHeaderTransformers;
        private static List<object> orderedTransformers;
    }
}
