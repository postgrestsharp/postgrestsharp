﻿using System.Linq;

namespace PostgRESTSharp.Commands.GenerateViewScripts.Templates
{
    public partial class ViewScript
    {
        public ViewScript(IViewMetaModel metaModel, string viewSchemaOwner, int viewSchemaVersion)
        {
            this.MetaModel = metaModel;
            this.ViewSchemaOwner = viewSchemaOwner;
            this.ViewSchemaVersion = viewSchemaVersion.ToString();
        }

        public IViewMetaModel MetaModel { get; protected set; }

        public string ViewSchemaOwner { get; protected set; }

        public string ViewSchemaVersion { get; protected set; }

        public string GetColumns(IViewMetaModel view)
        {
            return string.Join(", \n", view.Columns
                .Where(x => !x.IsHidden)
                .Select(x => string.Format("{0}.{1} as \"{2}\"", x.Table.TableName.Replace("$","_"), x.TableColumn.ColumnName, x.ColumnAlias)));
        }

        public string GetSources(IViewMetaModel view)
        {
			if (view.JoinSources.Count () > 0) {
				return string.Format ("{0} {1} \n", view.PrimarySource.TableName, view.PrimarySource.TableName.Replace ("$", "_")) +
				string.Join ("\n", view.JoinSources.Select (x => string.Format ("{0} {1} {2} ON {3}.{4} = {2}.{5}",
                    GetJoinType(x.SourceColumn), x.JoinSource.TableName, x.JoinSource.TableName.Replace ("$", "_"), 
					x.Source.TableName.Replace ("$", "_"), x.SourceColumn.ColumnName, x.JoinColumn.ColumnName))) + " \n";
			} else {
				return string.Format ("{0} \n", view.PrimarySource.TableName);
			}
        }

		public bool HasWhereClause(IViewMetaModel view)
		{
			return view.FilterElements.Count () != 0;
		}

        public string GetWhereClause(IViewMetaModel view)
        {
            if (view.FilterElements.Count() == 0) return "";

            string where = string.Join("", view.FilterElements.Select(x => string.Format(" AND " + x.ToString())));
            return "WHERE " + where.Substring(5, where.Length - 5);

        }

        public string GetJoinType(TableMetaModelColumn column)
        {
            if (column.IsNullable)
            {
                return "LEFT JOIN";
            }
            return "JOIN";
        }
    }
}