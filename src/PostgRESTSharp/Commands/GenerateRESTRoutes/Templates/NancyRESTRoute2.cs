using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes.Templates
{
    public partial class NancyRESTRoute
    {
        public NancyRESTRoute(IViewMetaModel metaModel, string fileNamespace, string modelNamespace)
        {
            this.MetaModel = metaModel;
            this.Namespace = fileNamespace;
            this.ModelNamespace = modelNamespace;
            this.ClassName = this.MetaModel.ModelName + "Module";
            this.CollectionRouteUrl = this.MetaModel.ModelNamePluralised.ToLower();
            this.UnderlyingCollectionRouteUrl = this.MetaModel.ViewName;
            this.PrimaryKeyColumnName = this.MetaModel.Columns.Where(x => x.IsKeyColumn == true).OrderBy(x => x.Order).FirstOrDefault().ColumnName;
            this.GETModelName = this.MetaModel.ModelName + "GETModel";
        }

        public IViewMetaModel MetaModel { get; protected set; }

        public string Namespace { get; protected set; }

        public string ModelNamespace { get; protected set; }

        public string ClassName { get; protected set; }

        public string CollectionRouteUrl { get; protected set; }

        public string UnderlyingCollectionRouteUrl { get; protected set; }

        public string PrimaryKeyColumnName { get; protected set; }

        public string GETModelName { get; protected set; }

        public IEnumerable<ViewMetaModelRelation> GetRelations()
        {
            return this.MetaModel.Columns.Where(x => x.RelatedView != null).Select(y=>y.RelatedView);
        }

        public string GetRelationUrl(ViewMetaModelRelation relation)
        {
            return relation.RelatedView.ModelNamePluralised.ToLower();
        }

        public string GetRelationName(ViewMetaModelRelation relation)
        {
            var colName = relation.RelationColumn.ColumnName.ToLower();
            if(colName.EndsWith("id"))
            {
                colName = colName.Substring(0, colName.Length - 2);
            }
            return colName;
        }
    }
}
