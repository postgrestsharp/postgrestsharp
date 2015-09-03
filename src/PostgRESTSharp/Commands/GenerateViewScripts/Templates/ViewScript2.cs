using System.Linq;

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
            return string.Join(", \n", view.Columns.Select(x => string.Format("{0}.{1} as \"{2}\"", x.Table.TableName, x.TableColumn.ColumnName, x.ColumnName)));
        }

        public string GetSources(IViewMetaModel view)
        {
            return view.PrimarySource.TableName +
                string.Join("\n", view.JoinSources.Select(x => string.Format("JOIN \"{0}\" {1} ON {2}.{3} = {4}.{5}", x.JoinSource.TableName, x.JoinSource.TableName, view.PrimarySource.TableName, x.SourceColumn.ColumnName, x.JoinSource.TableName, x.JoinColumn.ColumnName)));
        }
    }
}