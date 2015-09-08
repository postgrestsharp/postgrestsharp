﻿// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace PostgRESTSharp.Commands.GenerateRESTModels.Templates {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class NancyRESTModel : NancyRESTModelBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 6 ""
            this.Write("using System;\nusing PostgRESTSharp.Shared;\nusing Nancy;\n\nnamespace ");
            
            #line default
            #line hidden
            
            #line 10 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            
            #line 10 ""
            this.Write(" \n{\n\t// GET Model\n\tpublic class ");
            
            #line default
            #line hidden
            
            #line 13 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(MetaModel.ModelName));
            
            #line default
            #line hidden
            
            #line 13 ""
            this.Write("GETModel\n\t{\n\t\tpublic ");
            
            #line default
            #line hidden
            
            #line 15 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(MetaModel.ModelName));
            
            #line default
            #line hidden
            
            #line 15 ""
            this.Write("GETModel(");
            
            #line default
            #line hidden
            
            #line 15 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetConstructorArgs(RESTModelTypeEnum.Get)));
            
            #line default
            #line hidden
            
            #line 15 ""
            this.Write(")\n\t\t{\n\t\t\tthis._links = new RestLinks();\n");
            
            #line default
            #line hidden
            
            #line 18 ""
foreach(var assignment in GetConstructorAssignments(RESTModelTypeEnum.Get)) {
            
            #line default
            #line hidden
            
            #line 19 ""
            this.Write("\t\t\t");
            
            #line default
            #line hidden
            
            #line 19 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(assignment));
            
            #line default
            #line hidden
            
            #line 19 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 20 ""
}
            
            #line default
            #line hidden
            
            #line 21 ""
            this.Write("\t\t\n\t\t}\n\n\t\tpublic RestLinks _links {get; protected set; }\n");
            
            #line default
            #line hidden
            
            #line 25 ""
foreach(var property in GetProperties(RESTModelTypeEnum.Get)) {
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write("\t\t");
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(property));
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 27 ""
}
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write("\n\t\tpublic object GetPrimaryKeyValue()\n\t\t{\n\t\t\treturn this.");
            
            #line default
            #line hidden
            
            #line 31 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetPrimaryKeyColumnName()));
            
            #line default
            #line hidden
            
            #line 31 ""
            this.Write(";\n\t\t}\n\n\t\tpublic void BuildLinks(IRestLinkBuilder linkBuilder, Url currentUrl)\n\t\t{\n\t\t\tlinkBuilder.AddSelfLink(this._links, currentUrl);\n");
            
            #line default
            #line hidden
            
            #line 37 ""
foreach(var relation in GetRelations()){
            
            #line default
            #line hidden
            
            #line 38 ""
if(relation.Direction == RelationDirectionEnum.Forward) {
            
            #line default
            #line hidden
            
            #line 39 ""
            this.Write("\t\t\tlinkBuilder.AddRootLink(this._links, currentUrl, \"");
            
            #line default
            #line hidden
            
            #line 39 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetRelationUrl(relation)));
            
            #line default
            #line hidden
            
            #line 39 ""
            this.Write("\", \"");
            
            #line default
            #line hidden
            
            #line 39 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetRelationName(relation)));
            
            #line default
            #line hidden
            
            #line 39 ""
            this.Write("\", this.");
            
            #line default
            #line hidden
            
            #line 39 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.RelationColumn.ColumnName));
            
            #line default
            #line hidden
            
            #line 39 ""
            this.Write(".ToString());\n");
            
            #line default
            #line hidden
            
            #line 40 ""
}else{
            
            #line default
            #line hidden
            
            #line 41 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 42 ""
}
            
            #line default
            #line hidden
            
            #line 43 ""
}
            
            #line default
            #line hidden
            
            #line 44 ""
            this.Write("\t\t}\n\t}\n\n");
            
            #line default
            #line hidden
            
            #line 47 ""
if(!IsReadOnly){
            
            #line default
            #line hidden
            
            #line 48 ""
            this.Write("\t// POST Model\n\tpublic class ");
            
            #line default
            #line hidden
            
            #line 49 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(MetaModel.ModelName));
            
            #line default
            #line hidden
            
            #line 49 ""
            this.Write("POSTModel\n\t{\n\t\tpublic ");
            
            #line default
            #line hidden
            
            #line 51 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(MetaModel.ModelName));
            
            #line default
            #line hidden
            
            #line 51 ""
            this.Write("POSTModel(");
            
            #line default
            #line hidden
            
            #line 51 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetConstructorArgs(RESTModelTypeEnum.Post)));
            
            #line default
            #line hidden
            
            #line 51 ""
            this.Write(")\n\t\t{\n");
            
            #line default
            #line hidden
            
            #line 53 ""
foreach(var assignment in GetConstructorAssignments(RESTModelTypeEnum.Post)) {
            
            #line default
            #line hidden
            
            #line 54 ""
            this.Write("\t\t\t");
            
            #line default
            #line hidden
            
            #line 54 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(assignment));
            
            #line default
            #line hidden
            
            #line 54 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 55 ""
}
            
            #line default
            #line hidden
            
            #line 56 ""
            this.Write("\t\n\t\t}\n\n");
            
            #line default
            #line hidden
            
            #line 59 ""
foreach(var property in GetProperties(RESTModelTypeEnum.Post)) {
            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write("\t\t");
            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(property));
            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 61 ""
}
            
            #line default
            #line hidden
            
            #line 62 ""
            this.Write("\t}\n\n\t// POST Response Model\n\tpublic class ");
            
            #line default
            #line hidden
            
            #line 65 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(MetaModel.ModelName));
            
            #line default
            #line hidden
            
            #line 65 ""
            this.Write("POSTResponseModel\n\t{\n\t\tpublic ");
            
            #line default
            #line hidden
            
            #line 67 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(MetaModel.ModelName));
            
            #line default
            #line hidden
            
            #line 67 ""
            this.Write("POSTResponseModel(");
            
            #line default
            #line hidden
            
            #line 67 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetConstructorArgs(RESTModelTypeEnum.PostResponse)));
            
            #line default
            #line hidden
            
            #line 67 ""
            this.Write(")\n\t\t{\n\t\t\tthis._links = new RestLinks();\n");
            
            #line default
            #line hidden
            
            #line 70 ""
foreach(var assignment in GetConstructorAssignments(RESTModelTypeEnum.PostResponse)) {
            
            #line default
            #line hidden
            
            #line 71 ""
            this.Write("\t\t\t");
            
            #line default
            #line hidden
            
            #line 71 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(assignment));
            
            #line default
            #line hidden
            
            #line 71 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 72 ""
}
            
            #line default
            #line hidden
            
            #line 73 ""
            this.Write("\t\n\t\t}\n\n\t\tpublic RestLinks _links {get; protected set; }\n");
            
            #line default
            #line hidden
            
            #line 77 ""
foreach(var property in GetProperties(RESTModelTypeEnum.PostResponse)) {
            
            #line default
            #line hidden
            
            #line 78 ""
            this.Write("\t\t");
            
            #line default
            #line hidden
            
            #line 78 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(property));
            
            #line default
            #line hidden
            
            #line 78 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 79 ""
}
            
            #line default
            #line hidden
            
            #line 80 ""
            this.Write("\n\t\tpublic void BuildSelfLink(IRestLinkBuilder linkBuilder, Url currentUrl)\n\t\t{\n\t\t\tlinkBuilder.AddSelfLink(this._links, currentUrl);\n\t\t}\n\t} \n");
            
            #line default
            #line hidden
            
            #line 86 ""
}
            
            #line default
            #line hidden
            
            #line 87 ""
            this.Write("}\n\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class NancyRESTModelBase {
        
        private global::System.Text.StringBuilder builder;
        
        private global::System.Collections.Generic.IDictionary<string, object> session;
        
        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;
        
        private string currentIndent = string.Empty;
        
        private global::System.Collections.Generic.Stack<int> indents;
        
        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();
        
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session {
            get {
                return this.session;
            }
            set {
                this.session = value;
            }
        }
        
        public global::System.Text.StringBuilder GenerationEnvironment {
            get {
                if ((this.builder == null)) {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set {
                this.builder = value;
            }
        }
        
        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors {
            get {
                if ((this.errors == null)) {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }
        
        public string CurrentIndent {
            get {
                return this.currentIndent;
            }
        }
        
        private global::System.Collections.Generic.Stack<int> Indents {
            get {
                if ((this.indents == null)) {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }
        
        public ToStringInstanceHelper ToStringHelper {
            get {
                return this._toStringHelper;
            }
        }
        
        public void Error(string message) {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }
        
        public void Warning(string message) {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }
        
        public string PopIndent() {
            if ((this.Indents.Count == 0)) {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }
        
        public void PushIndent(string indent) {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }
        
        public void ClearIndent() {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }
        
        public void Write(string textToAppend) {
            this.GenerationEnvironment.Append(textToAppend);
        }
        
        public void Write(string format, params object[] args) {
            this.GenerationEnvironment.AppendFormat(format, args);
        }
        
        public void WriteLine(string textToAppend) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }
        
        public void WriteLine(string format, params object[] args) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }
        
        public class ToStringInstanceHelper {
            
            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;
            
            public global::System.IFormatProvider FormatProvider {
                get {
                    return this.formatProvider;
                }
                set {
                    if ((value != null)) {
                        this.formatProvider = value;
                    }
                }
            }
            
            public string ToStringWithCulture(object objectToConvert) {
                if ((objectToConvert == null)) {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type)) {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                            iConvertibleType});
                if ((methInfo != null)) {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                                this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}