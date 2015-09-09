using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Conventions;

namespace PostgRESTSharp
{
    public class ViewMetaModelProcessor : IViewMetaModelProcessor
    {
        private IViewMetaModelBuilder viewBuilder;
		private IConventionResolver conventionResolver;

        public ViewMetaModelProcessor(IViewMetaModelBuilder builder, IConventionResolver conventionResolver)
        {
            this.viewBuilder = builder;
			this.conventionResolver = conventionResolver;
        }

        public IEnumerable<IViewMetaModel> ProcessModels(IEnumerable<IMetaModel> models, int viewSchemaVersion)
        {
            List<IViewMetaModel> views = new List<IViewMetaModel>();
            foreach (var tableModel in models.Where(x => x.MetaModelType == MetaModelTypeEnum.Table))
            {
				// check for a view table exclusion convention
				var inclusionConvention = this.conventionResolver.ResolveTableConvention<IViewInclusionConvention>(tableModel);
				inclusionConvention.AddView(views, () => 
                	viewBuilder.BuildModel(tableModel, models.Where(x => !(x.DatabaseName == tableModel.DatabaseName && x.SchemaName == tableModel.SchemaName && x.TableName == tableModel.TableName)), viewSchemaVersion.ToString())
				);
            }

            // go back and reprocess the view relations
            foreach (var view in views)
            {
                // look for foreign keys in all tables that make up the view
                List<IMetaModel> sources = new List<IMetaModel>();
                sources.Add(view.PrimarySource);
                sources.AddRange(view.JoinSources.Select(x => x.JoinSource));

                foreach (var source in sources)
                {
                    foreach (var relation in source.Relations)
                    {
                        var relatedViewName = relation.StorageModel.ModelNameCamelCased;
                        var relatedView = views.Where(x => x.ViewName == relatedViewName).FirstOrDefault();
                        var relationColumn = view.Columns.Where(x => x.TableColumn.ColumnName == relation.RelationColumns.First()).FirstOrDefault();
                        if (relatedView != null && relationColumn != null)
                        {
                            var viewRelation = new ViewMetaModelRelation(relatedView, relation.Direction, relationColumn);
                            relationColumn.AddRelation(viewRelation);
                        }
                        else
                        {
                            int a = 0;

                        }
                    }
                }
            }

            return views;
        }
    }
}