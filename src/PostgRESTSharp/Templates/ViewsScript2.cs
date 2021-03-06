﻿using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Templates
{
    public partial class ViewsScript
    {
        public ViewsScript(IEnumerable<IViewMetaModel> metaModels, string viewSchemaOwner, int viewSchemaVersion)
        {
            this.MetaModels = new List<IViewMetaModel>(metaModels);
            this.ViewSchemaOwner = viewSchemaOwner;
            this.ViewSchemaVersion = viewSchemaVersion.ToString();
        }

        public IEnumerable<IViewMetaModel> MetaModels { get; protected set; }

        public string ViewSchemaOwner { get; protected set; }

        public string ViewSchemaVersion { get; protected set; }

        public string GetColumns(IViewMetaModel view)
        {
            return string.Join(", \n", view.Columns.Select(x => string.Format("{0}.{1} as \"{2}\"", view.ViewName, x.ColumnName, x.ColumnName)));
        }

        public string GetSources(IViewMetaModel view)
        {
            return view.PrimarySource.TableName;
        }
    }
}