using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PostgRESTSharp.Shared
{
    /// <summary>
    /// Provides a mechanism to override value-setting on certain protected headers in RestSharp Http wrapper (and its underlying HttpWebRequest)
    /// </summary>
    public class PostgRestHttpRequest : Http
    {
        private static string[] restrictedHeaderActionsToOverride = new[] { "Range" };

        private static object restrictedHeaderActionFieldInfoLock = new object();
        private static FieldInfo restrictedHeaderActionFieldInfo = null;

        private static object setValueMethodInfoLock = new object();
        private static MethodInfo setValueMethodInfo = null;

        public PostgRestHttpRequest()
        {
            //standard double checked locking
            if (restrictedHeaderActionFieldInfo == null)
            {
                lock (restrictedHeaderActionFieldInfoLock)
                {
                    if (restrictedHeaderActionFieldInfo == null)
                    {
                        var fieldInfo = this
                            .GetType()
                            .BaseType
                            .GetField("restrictedHeaderActions", BindingFlags.NonPublic | BindingFlags.Instance);

                        if (fieldInfo == null)
                        {
                            Throw();
                        }

                        restrictedHeaderActionFieldInfo = fieldInfo;
                    }
                }
            }

            var restrictedHeaderActions = restrictedHeaderActionFieldInfo.GetValue(this) as IDictionary<string, Action<HttpWebRequest, string>>;
            if (restrictedHeaderActions == null)
            {
                Throw();
            }

            foreach(var header in restrictedHeaderActionsToOverride)
            {
                restrictedHeaderActions.Remove(header);
                restrictedHeaderActions.Add(header, (request, value) =>
                {
                    AddHeaderWithoutValidation(request, header, value);
                });
            }
        }

        private static void Throw()
        {
            throw new InvalidOperationException("Cannot retrieve restricted headers collection for overriding");
        }

        private static void AddHeaderWithoutValidation(HttpWebRequest request, string name, string value)
        {
            //standard double checked locking
            if (setValueMethodInfo == null)
            {
                lock (setValueMethodInfoLock)
                {
                    if (setValueMethodInfo == null)
                    {
                        var type = request.Headers.GetType();
                        var method = type.GetMethod("AddWithoutValidate", BindingFlags.NonPublic | BindingFlags.Instance);
                        setValueMethodInfo = method;
                    }
                }
            }

            setValueMethodInfo.Invoke(request.Headers, new[] { name, value });
        }
    }
}
