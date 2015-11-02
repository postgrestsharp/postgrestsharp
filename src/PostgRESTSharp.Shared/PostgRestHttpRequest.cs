using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        private static readonly string[] restrictedHeaderActionsToOverride = { "Range" };

        private static readonly object restrictedHeaderActionFieldInfoLock = new object();
        private static FieldInfo restrictedHeaderActionFieldInfo;

        private static readonly object setValueMethodInfoLock = new object();
        private static MethodInfo setValueMethodInfo;

        private readonly object restrictedHeaderActionsLock = new object();
        public IDictionary<string, Action<HttpWebRequest, string>> restrictedHeaderActions;

        public PostgRestHttpRequest()
        {
            SetRestrictedHeaderActionFieldInfo();

            var restrictedHeaderActions = restrictedHeaderActionFieldInfo.GetValue(this) as IDictionary<string, Action<HttpWebRequest, string>>;
            if (restrictedHeaderActions == null)
            {
                Throw();
            }

            SetRestrictedHeaderActions(restrictedHeaderActions);

            SetNewMethodOnOverriddenHeaderActions();
        }

        private void SetNewMethodOnOverriddenHeaderActions()
        {
            foreach (var header in restrictedHeaderActionsToOverride)
            {
                this.restrictedHeaderActions.Remove(header);
                this.restrictedHeaderActions.Add(header,
                    (request, value) =>
                    {
                        AddHeaderWithoutValidation(request, header, value);
                    });
            }
        }

        private void SetRestrictedHeaderActions(IDictionary<string, Action<HttpWebRequest, string>> restrictedHeaderActions)
        {
            if (this.restrictedHeaderActions != null)
            {
                return;
            }
            lock (restrictedHeaderActionsLock)
            {
                if (this.restrictedHeaderActions != null)
                {
                    return;
                }
                this.restrictedHeaderActions = restrictedHeaderActions;
            }
        }
        
        private void SetRestrictedHeaderActionFieldInfo()
        {
            //standard double checked locking
            if (restrictedHeaderActionFieldInfo != null)
            {
                return;
            }
            lock (restrictedHeaderActionFieldInfoLock)
            {
                if (restrictedHeaderActionFieldInfo != null)
                {
                    return;
                }
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

        private static void Throw()
        {
            throw new InvalidOperationException("Cannot retrieve restricted headers collection for overriding");
        }

        private static void AddHeaderWithoutValidation(HttpWebRequest request, string name, string value)
        {
            SetValueMethodInfo(request);

            setValueMethodInfo.Invoke(request.Headers, new[] { name, value });
        }

        private static void SetValueMethodInfo(HttpWebRequest request)
        {
            //standard double checked locking
            if (setValueMethodInfo != null)
            {
                return;
            }
            lock (setValueMethodInfoLock)
            {
                if (setValueMethodInfo != null)
                {
                    return;
                }
                var type = request.Headers.GetType();
                var method = type.GetMethod("AddWithoutValidate", BindingFlags.NonPublic | BindingFlags.Instance);
                setValueMethodInfo = method;
            }
        }
    }
}
