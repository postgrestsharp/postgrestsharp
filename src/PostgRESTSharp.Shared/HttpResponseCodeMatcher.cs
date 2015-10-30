using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Shared
{
    public class HttpStatusCodeMatcher : IHttpStatusCodeMatcher
    {
        public Nancy.HttpStatusCode ToNancy(System.Net.HttpStatusCode systemNetHttpStatusCode)
        {
            return (Nancy.HttpStatusCode)systemNetHttpStatusCode;
        }

        public System.Net.HttpStatusCode ToSystemNet(Nancy.HttpStatusCode nancyHttpStatusCode)
        {
            return (System.Net.HttpStatusCode)nancyHttpStatusCode;
        }

        public bool AreEqual(System.Net.HttpStatusCode systemNetHttpStatusCode, Nancy.HttpStatusCode nancyHttpStatusCode)
        {
            return (int)systemNetHttpStatusCode == (int)nancyHttpStatusCode;
        }

        public bool AreEqual(Nancy.HttpStatusCode nancyHttpStatusCode, System.Net.HttpStatusCode systemNetHttpStatusCode)
        {
            return AreEqual(systemNetHttpStatusCode, nancyHttpStatusCode);
        }

        public bool IsInGroup(int httpStatusCode, int group)
        {
            return httpStatusCode >= group && httpStatusCode <= (group + 99);
        }

        public bool IsInformational(int httpStatusCode)
        {
            return IsInGroup(httpStatusCode, 100);
        }

        public bool IsInformational(Nancy.HttpStatusCode httpStatusCode)
        {
            return IsInformational((int)httpStatusCode);
        }

        public bool IsInformational(System.Net.HttpStatusCode httpStatusCode)
        {
            return IsInformational((int)httpStatusCode);
        }

        public bool IsSuccess(int httpStatusCode)
        {
            return IsInGroup(httpStatusCode, 200);
        }

        public bool IsSuccess(Nancy.HttpStatusCode httpStatusCode)
        {
            return IsSuccess((int)httpStatusCode);
        }

        public bool IsSuccess(System.Net.HttpStatusCode httpStatusCode)
        {
            return IsSuccess((int)httpStatusCode);
        }

        public bool IsRedirection(int httpStatusCode)
        {
            return IsInGroup(httpStatusCode, 300);
        }

        public bool IsRedirection(Nancy.HttpStatusCode httpStatusCode)
        {
            return IsRedirection((int)httpStatusCode);
        }

        public bool IsRedirection(System.Net.HttpStatusCode httpStatusCode)
        {
            return IsRedirection((int)httpStatusCode);
        }

        public bool IsClientError(int httpStatusCode)
        {
            return IsInGroup(httpStatusCode, 400);
        }

        public bool IsClientError(Nancy.HttpStatusCode httpStatusCode)
        {
            return IsClientError((int)httpStatusCode);
        }

        public bool IsClientError(System.Net.HttpStatusCode httpStatusCode)
        {
            return IsClientError((int)httpStatusCode);
        }

        public bool IsServerError(int httpStatusCode)
        {
            return IsInGroup(httpStatusCode, 500);
        }

        public bool IsServerError(Nancy.HttpStatusCode httpStatusCode)
        {
            return IsServerError((int)httpStatusCode);
        }

        public bool IsServerError(System.Net.HttpStatusCode httpStatusCode)
        {
            return IsServerError((int)httpStatusCode);
        }
    }
}
