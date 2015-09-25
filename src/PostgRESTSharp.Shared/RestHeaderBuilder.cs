using System;
using System.Collections.Generic;
using System.Net.Http;

namespace PostgRESTSharp.Shared
{
    public class RestHeaderBuilder : IRestHeaderBuilder
    {
        public void BuildHeader(HttpClient client, string version, string userId)
        {

            version = VerifyVersion(version);
            
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json; version=" + version);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Basic " + CreateBasicAuth(userId));
            
        }

        private string VerifyVersion(string version)
        {
            if (version.Length == 0)
            {
                version = "1";
            }
            return version;
        }

        private string CreateBasicAuth(string userId)
        {
            byte[] values = System.Text.Encoding.UTF8.GetBytes(userId + ":");
            return Convert.ToBase64String(values);
        }

    }

}