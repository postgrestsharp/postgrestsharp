using System;
using System.Net;
using Machine.Fakes;
using Machine.Specifications;
using PostgRESTSharp.Shared;
using RestSharp;

namespace PostgRESTSharp.Specs.PostgRestHttpRequestSpecs
{
    public class when_overriding_a_range_header : WithFakes
    {
        Establish that = () =>
        {
            request = new PostgRestHttpRequest();

            //we're testing the side effect of request.Get(), so set the timeout to 1 millisecond
            request.Timeout = 1;
            request.Url = new Uri("http://localhosty:1234/");

            header = new HttpHeader();
            header.Name = "Range";
            header.Value = "0-1";
            request.Headers.Add(header);

            customMethodWasCalled = false;

            rangeHeaderAction = request.restrictedHeaderActions["Range"];

            //set a wrapper action to check if it is called when we perform a call on the request
            request.restrictedHeaderActions["Range"] = (webRequest, value) =>
            {
                rangeHeaderAction(webRequest, value);
                customMethodWasCalled = true;
            };
        };

        private Because of = () =>
        {
            request.Get();
        };

        private It should_have_overridden_the_header_add_action = () =>
        {
            customMethodWasCalled.ShouldBeTrue();
        };

        private static PostgRestHttpRequest request;
        private static HttpHeader header;
        private static Action<HttpWebRequest, string> rangeHeaderAction;
        private static bool customMethodWasCalled;
    }
}