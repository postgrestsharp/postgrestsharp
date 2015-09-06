using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgRESTSharp.Commands.GenerateRESTRoutes.Templates
{
    public partial class NancyRESTRoute
    {
		public NancyRESTRoute(IViewMetaModel metaModel, string fileNamespace, string modelNamespace, bool isReadOnly)
        {
            this.MetaModel = metaModel;
            this.Namespace = fileNamespace;
            this.ModelNamespace = modelNamespace;
            this.ClassName = this.MetaModel.ModelName + "Module";
            this.CollectionRouteUrl = this.MetaModel.ModelNamePluralised.ToLower();
            this.UnderlyingCollectionRouteUrl = this.MetaModel.ViewName;
            this.PrimaryKeyColumnName = this.MetaModel.Columns.Where(x => x.IsKeyColumn == true).OrderBy(x => x.Order).FirstOrDefault().ColumnName;
            this.GETModelName = this.MetaModel.ModelName + "GETModel";
			this.POSTModelName = this.MetaModel.ModelName + "POSTModel";
			this.POSTResponseModelName = this.MetaModel.ModelName + "POSTResponseModel";
			this.IsReadOnly = isReadOnly;
        }

        public IViewMetaModel MetaModel { get; protected set; }

        public string Namespace { get; protected set; }

        public string ModelNamespace { get; protected set; }

        public string ClassName { get; protected set; }

        public string CollectionRouteUrl { get; protected set; }

        public string UnderlyingCollectionRouteUrl { get; protected set; }

        public string PrimaryKeyColumnName { get; protected set; }

        public string GETModelName { get; protected set; }

		public string POSTModelName { get; protected set; }

		public string POSTResponseModelName { get; protected set; }

		public bool IsReadOnly { get; protected set; }
    }
}
