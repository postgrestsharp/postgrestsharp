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
            this.Write("using Nancy;\r\nusing Nancy.Extensions;\r\nusing Nancy.ModelBinding;\r\nusing System.Ne" +
                    "t.Http;\r\nusing Newtonsoft.Json;\r\nusing System.Collections.Generic;\r\nusing System" +
                    ".Linq;\r\nusing PostgRESTSharp.Shared;\r\nusing ");
            
            #line 15 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ModelNamespace));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\nnamespace ");
            
            #line 17 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write(" \r\n{\r\n    public class ");
            
            #line 19 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write(" : NancyModule\r\n    {\r\n    \tpublic ");
            
            #line 21 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("(IRestLinkBuilder linkBuilder, ILocationHeaderParser locationHeaderParser, IPostg" +
                    "RESTUrlConfigurationProvider postgRESTConfigProvider, IRestClient restClient, IR" +
                    "estRequest restRequest) \r\n    \t{\r\n");
            
            #line 23 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
foreach(var method in Resource.Methods){
            
            #line default
            #line hidden
            this.Write("\t\t\t");
            
            #line 24 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GetVerbString(method.Verb)));
            
            #line default
            #line hidden
            this.Write("[\"/");
            
            #line 24 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.Uri + GetParameters(method.URIParameters)));
            
            #line default
            #line hidden
            this.Write("\", true] = async (ctx, ct) =>\r\n    \t\t{\r\n");
            
            #line 26 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
switch(method.Verb){
            
            #line default
            #line hidden
            
            #line 27 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
case RESTVerbEnum.GET:
            
            #line default
            #line hidden
            
            #line 28 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
if(method.VerbDetail == RESTVerbDetailEnum.Collection){
            
            #line default
            #line hidden
            this.Write("\t\t\t\trestClient.BaseUrl = new Uri(\"http://\" + postgRESTConfigProvider.Url);\r\n\t\t\t\tr" +
                    "estRequest.Resource = \"/");
            
            #line 30 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.PostgRESTUri));
            
            #line default
            #line hidden
            this.Write("?order=");
            
            #line 30 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.KeyName));
            
            #line default
            #line hidden
            this.Write(".asc\";\r\n\t\t\t\tvar response = restClient.Execute(restRequest);\r\n\t\t\t\tvar models = Jso" +
                    "nConvert.DeserializeObject<List<");
            
            #line 32 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GETModelName));
            
            #line default
            #line hidden
            this.Write(@">>(response.Content);
				foreach(var model in models)
				{
					var url = new Url();
					url.HostName = this.Request.Url.HostName;
					url.Port = this.Request.Url.Port;
					url.BasePath = this.Request.Url.Path;
					url.Path = ""/"" + model.GetPrimaryKeyValue();
					model.BuildLinks(linkBuilder, url);
				}
				return models;
");
            
            #line 43 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}else{
            
            #line default
            #line hidden
            this.Write("\t\t\t\trestClient.BaseUrl = new Uri(\"http://\" + postgRESTConfigProvider.Url);\r\n\t\t\t\tr" +
                    "estRequest.Resource = string.Format(\"/");
            
            #line 45 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.PostgRESTUri));
            
            #line default
            #line hidden
            this.Write("?");
            
            #line 45 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(method.URIParameters.First().Name));
            
            #line default
            #line hidden
            this.Write("=eq.{0}\", ctx.");
            
            #line 45 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(method.URIParameters.First().Name));
            
            #line default
            #line hidden
            this.Write(");\r\n\t\t\t\tvar response = restClient.Execute(restRequest);\r\n    \t\t\tvar models = Json" +
                    "Convert.DeserializeObject<List<");
            
            #line 47 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(GETModelName));
            
            #line default
            #line hidden
            this.Write(">>(response.Content);\r\n\t\t\t\tvar model = models.First();\r\n\t\t\t\tmodel.BuildLinks(link" +
                    "Builder, this.Request.Url);\r\n\t\t\t\treturn model;\r\n");
            
            #line 51 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            
            #line 52 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
break;
            
            #line default
            #line hidden
            
            #line 53 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
case RESTVerbEnum.POST:
            
            #line default
            #line hidden
            this.Write("\t\t\t\tvar model = this.Request.Body.AsString();\r\n\t\t\t\trestClient.BaseUrl = \"http://\"" +
                    " + postgRESTConfigProvider.Url;\r\n    \t\t\trestRequest.Resource = \"/");
            
            #line 56 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Resource.PostgRESTUri));
            
            #line default
            #line hidden
            this.Write(@""";
				restRequest.Method = Method.POST;
				restRequest.AddJsonBody(model);
				var response = restClient.Execute(restRequest);
				var locationHeader = response.Headers.First(a => a.Name.Equals(""Location"", StringComparison.OrdinalIgnoreCase));
				var primaryKeyValue = locationHeaderParser.ParseLocationHeader<int>(""id"", res.Headers.Location);
    			var responseModel = new ");
            
            #line 62 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(POSTResponseModelName));
            
            #line default
            #line hidden
            this.Write("(primaryKeyValue);\r\n    \t\t\tresponseModel.BuildSelfLink(linkBuilder, this.Request." +
                    "Url);\r\n    \t\t\treturn responseModel;\r\n");
            
            #line 65 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
break;
            
            #line default
            #line hidden
            
            #line 66 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
default:
            
            #line default
            #line hidden
            
            #line 67 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
break;
            
            #line default
            #line hidden
            
            #line 68 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            this.Write("    \t\t};\r\n");
            
            #line 70 "D:\postgrestsharp\src\PostgRESTSharp\Commands\GenerateRESTRoutes\Templates\NancyRESTRoute.tt"
}
            
            #line default
            #line hidden
            this.Write("    \t}\r\n    }\r\n}");
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
