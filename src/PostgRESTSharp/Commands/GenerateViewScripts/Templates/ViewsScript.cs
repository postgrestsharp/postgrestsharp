﻿// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace PostgRESTSharp.Commands.GenerateViewScripts.Templates {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class ViewsScript : ViewsScriptBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 6 ""
            this.Write("CREATE SCHEMA IF NOT EXISTS \"");
            
            #line default
            #line hidden
            
            #line 6 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewSchemaVersion));
            
            #line default
            #line hidden
            
            #line 6 ""
            this.Write("\"\n  AUTHORIZATION ");
            
            #line default
            #line hidden
            
            #line 7 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewSchemaOwner));
            
            #line default
            #line hidden
            
            #line 7 ""
            this.Write(";\n\n");
            
            #line default
            #line hidden
            
            #line 9 ""
foreach (var view in MetaModels){
            
            #line default
            #line hidden
            
            #line 10 ""
            this.Write("\n-- Start ");
            
            #line default
            #line hidden
            
            #line 11 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(view.ViewName));
            
            #line default
            #line hidden
            
            #line 11 ""
            this.Write(" --\n\nCREATE OR REPLACE VIEW \"");
            
            #line default
            #line hidden
            
            #line 13 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewSchemaVersion));
            
            #line default
            #line hidden
            
            #line 13 ""
            this.Write("\".\"");
            
            #line default
            #line hidden
            
            #line 13 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(view.ViewName));
            
            #line default
            #line hidden
            
            #line 13 ""
            this.Write("\" AS \n SELECT ");
            
            #line default
            #line hidden
            
            #line 14 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetColumns(view)));
            
            #line default
            #line hidden
            
            #line 14 ""
            this.Write("\n   FROM ");
            
            #line default
            #line hidden
            
            #line 15 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetSources(view)));
            
            #line default
            #line hidden
            
            #line 15 ""
if(!HasWhereClause(view)){
            
            #line default
            #line hidden
            
            #line 16 ""
            this.Write(";\n");
            
            #line default
            #line hidden
            
            #line 17 ""
}
            
            #line default
            #line hidden
            
            #line 18 ""
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 18 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(GetWhereClause(view)));
            
            #line default
            #line hidden
            
            #line 18 ""
if(HasWhereClause(view)){
            
            #line default
            #line hidden
            
            #line 19 ""
            this.Write(";\n");
            
            #line default
            #line hidden
            
            #line 20 ""
}
            
            #line default
            #line hidden
            
            #line 21 ""
            this.Write("\nALTER VIEW \"");
            
            #line default
            #line hidden
            
            #line 22 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewSchemaVersion));
            
            #line default
            #line hidden
            
            #line 22 ""
            this.Write("\".\"");
            
            #line default
            #line hidden
            
            #line 22 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(view.ViewName));
            
            #line default
            #line hidden
            
            #line 22 ""
            this.Write("\"\n  OWNER TO ");
            
            #line default
            #line hidden
            
            #line 23 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewSchemaOwner));
            
            #line default
            #line hidden
            
            #line 23 ""
            this.Write(";\n\n");
            
            #line default
            #line hidden
            
            #line 25 ""
foreach(var priv in view.PrimarySource.Privileges){
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write("GRANT ");
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(priv.Type));
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write(" ON \"");
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(ViewSchemaVersion));
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write("\".\"");
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(view.ViewName));
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write("\" TO \"");
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(priv.Grantee));
            
            #line default
            #line hidden
            
            #line 26 ""
            this.Write("\";\n");
            
            #line default
            #line hidden
            
            #line 27 ""
}
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write("-- End ");
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write(this.ToStringHelper.ToStringWithCulture(view.ViewName));
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write(" --\n\n");
            
            #line default
            #line hidden
            
            #line 30 ""
}
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ViewsScriptBase {
        
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
