﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace PostgRESTSharp.Commands.GenerateRESTRoutes.Templates
{
    using PostgRESTSharp.Commands.GenerateRESTRoutes;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using PostgRESTSharp.REST;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class NancyRESTRoute : NancyRESTRouteBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"using System;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;
using Nancy.Security;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Shared;
using ");
            
            #line 19 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ModelNamespace));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 21 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write(" \r\n{\r\n    public class ");
            
            #line 23 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : NancyModule\r\n    {\r\n    \tpublic ");
            
            #line 25 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(IApiClient client, IAuthenticatorFactory authenticatorFactory, IPostgRESTUrlConf" +
                    "igurationProvider postgRESTConfigProvider, \r\n\t\t\t\tIRestLinkBuilder linkBuilder, I" +
                    "RoleEnforcer roleEnforcer, IPostgRESTUserProvider postgRestUserProvider) \r\n    \t" +
                    "{\r\n");
            
            #line 28 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
 if (Resource.AccessClaims.Count() > 0) { 
            
            #line default
            #line hidden
            this.Write("\t\t\troleEnforcer.EnsureUserBelongsToRoles(this, new[] { \r\n");
            
            #line 30 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
 string prefix = ""; 
            
            #line default
            #line hidden
            
            #line 31 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
foreach(var claim in Resource.AccessClaims){
            
            #line default
            #line hidden
            this.Write("\t\t\t\t");
            
            #line 32 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(prefix));
            
            #line default
            #line hidden
            this.Write(" \"");
            
            #line 32 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(claim));
            
            #line default
            #line hidden
            this.Write("\"\r\n\t");
            
            #line 33 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
 prefix = ","; 
            
            #line default
            #line hidden
            
            #line 34 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            this.Write("\t\t});\r\n");
            
            #line 35 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 37 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
foreach(var method in Resource.Methods){
            
            #line default
            #line hidden
            this.Write("\t\t\t");
            
            #line 38 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetVerbString(method.Verb)));
            
            #line default
            #line hidden
            this.Write("[\"/");
            
            #line 38 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.Uri + GetParameters(method.URIParameters)));
            
            #line default
            #line hidden
            this.Write("\", true] = async (ctx, ct) =>\r\n    \t\t{\r\n");
            
            #line 40 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
if (ErrorHandlingMode.Equals(ErrorHandlingModes.STANDARD)){
            
            #line default
            #line hidden
            this.Write("                throw new NotImplementedException(\"TODO: implement try/catches ar" +
                    "ound modules\");\r\n");
            
            #line 42 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}else{
            
            #line default
            #line hidden
            
            #line 43 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
switch(method.Verb){
            
            #line default
            #line hidden
            
            #line 44 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
case RESTVerbEnum.GET:
            
            #line default
            #line hidden
            
            #line 45 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
if(method.VerbDetail == RESTVerbDetailEnum.Collection){
            
            #line default
            #line hidden
            this.Write("\t\t\t\tvar authenticator = authenticatorFactory.GetPostgrestAuthenticator(postgRestU" +
                    "serProvider.GetDatabaseUser(this), \"\");\r\n    \t\t    var queryStringParameters = n" +
                    "ew[]\r\n                {\r\n                    new KeyValuePair<string, string>(\"o" +
                    "rder\", \"");
            
            #line 49 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.KeyName));
            
            #line default
            #line hidden
            this.Write(".asc\"),\r\n                };\r\n                var models = client.ExecuteGet<List<" +
                    "");
            
            #line 51 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GETModelName));
            
            #line default
            #line hidden
            this.Write(">>(\"");
            
            #line 51 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.PostgRESTUri));
            
            #line default
            #line hidden
            this.Write(@""", postgRESTConfigProvider.Url, queryStringParameters, authenticator);
				foreach(var model in models)
				{
					Url url = new Url();
					url.HostName = this.Request.Url.HostName;
					url.Port = this.Request.Url.Port;
					url.BasePath = this.Request.Url.Path;
					url.Path = ""/"" + model.GetPrimaryKeyValue();
					model.BuildLinks(linkBuilder, url);
				}

				return models;
");
            
            #line 63 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}else{
            
            #line default
            #line hidden
            this.Write(@"				var authenticator = authenticatorFactory.GetPostgrestAuthenticator(postgRestUserProvider.GetDatabaseUser(this), """");
                var queryStringParameters = new[]
                {
                    new KeyValuePair<string, string>(""id"", string.Format(""eq.{0}"", ctx.id)),
                };
                var models = client.ExecuteGet<List<");
            
            #line 69 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GETModelName));
            
            #line default
            #line hidden
            this.Write(">>(\"");
            
            #line 69 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.PostgRESTUri));
            
            #line default
            #line hidden
            this.Write(@""", postgRESTConfigProvider.Url, queryStringParameters, authenticator);
				var model = models.FirstOrDefault();
    		    if (model != null)
    		    {
                    model.BuildLinks(linkBuilder, this.Request.Url);
    		    }
				return model;
");
            
            #line 76 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            
            #line 77 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
break;
            
            #line default
            #line hidden
            
            #line 78 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
case RESTVerbEnum.POST:
            
            #line default
            #line hidden
            this.Write("\t\t\t\tvar model = this.Request.Body.AsString();\r\n                var authenticator " +
                    "= authenticatorFactory.GetPostgrestAuthenticator(postgRestUserProvider.GetDataba" +
                    "seUser(this), \"\");\r\n                var response = client.ExecutePost(\"");
            
            #line 81 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.PostgRESTUri));
            
            #line default
            #line hidden
            this.Write(@""", postgRESTConfigProvider.Url, model, authenticator);
                var locationHeader = response.Headers.First(a => a.Name.Equals(""Location"", StringComparison.OrdinalIgnoreCase));
	            var primaryKeyValue = locationHeaderParser.ParseLocationHeader<int>(""id"", new Uri((string)locationHeader.Value));
                var responseModel = new ");
            
            #line 84 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(POSTResponseModelName));
            
            #line default
            #line hidden
            this.Write("(primaryKeyValue);\r\n    \t\t    responseModel.BuildSelfLink(linkBuilder, this.Reque" +
                    "st.Url);\r\n    \t\t\treturn responseModel;\r\n");
            
            #line 87 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
break;
            
            #line default
            #line hidden
            
            #line 88 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
default:
            
            #line default
            #line hidden
            
            #line 89 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
break;
            
            #line default
            #line hidden
            
            #line 90 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            
            #line 91 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            this.Write("    \t\t};\r\n");
            
            #line 93 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            this.Write("\t\t\r\n    \t}\r\n\r\n    }\r\n\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class NancyRESTRouteBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
