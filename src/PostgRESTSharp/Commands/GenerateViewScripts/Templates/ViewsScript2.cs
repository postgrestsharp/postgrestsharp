using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Commands.GenerateViewScripts.Templates
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
            return string.Join(", \n", view.Columns.Select(x => string.Format("{0}.{1} as \"{2}\"", x.Table.TableName, x.TableColumn.ColumnName, x.ColumnName)));
        }

        public string GetSources(IViewMetaModel view)
        {
            return string.Format("{0} {1} \n", view.PrimarySource.TableName, view.PrimarySource.TableName.Replace("$", "_")) +
                string.Join("\n", view.JoinSources.Select(x => string.Format("JOIN {0} {1} ON {2}.{3} = {1}.{4}", x.JoinSource.TableName, x.JoinSource.TableName.Replace("$", "_"), view.PrimarySource.TableName, x.SourceColumn.ColumnName, x.JoinColumn.ColumnName)));
        }
    }
}