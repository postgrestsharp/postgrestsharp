using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using PostgRESTSharp.Conventions;
using PostgRESTSharp.Conventions.ViewConventions.ViewFiltering;

namespace PostgRESTSharp
{
    public class ViewMetaModelProcessor : IViewMetaModelProcessor
    {
		private IConventionResolver conventionResolver;

        public ViewMetaModelProcessor(IConventionResolver conventionResolver)
        {
			this.conventionResolver = conventionResolver;
        }

        public IEnumerable<IViewMetaModel> ProcessModels(IEnumerable<ITableMetaModel> models, int viewSchemaVersion)
        {
            List<IViewMetaModel> views = new List<IViewMetaModel>();
            foreach (var tableModel in models.Where(x => x.MetaModelType == TableMetaModelTypeEnum.Table))
            {
				// check for a view table exclusion convention;
				var inclusionConvention = this.conventionResolver.ResolveTableConvention<IViewInclusionConvention>(tableModel);
                var viewBuilderConvention = this.conventionResolver.ResolveTableConvention<IViewBuildingConvention>(tableModel);

                //should we process the table, check using the table exclusion conventions
                var implicitTableExclustionConvention = this.conventionResolver.ResolveTableConvention<ITableExclusionConvention>(tableModel);
                var explicitTableExclustionConvention = this.conventionResolver.ResolveTableConvention<ITableExclusionConvention>(tableModel);
                bool isExcluded = implicitTableExclustionConvention.IsExcluded() || explicitTableExclustionConvention.IsExcluded();

                inclusionConvention.AddView(views, () =>
                {
                    var viewNamingConvention =
                        this.conventionResolver.ResolveTableConvention<IViewNamingConvention>(tableModel);
                    var view = new ViewMetaModel(tableModel.DatabaseName, viewSchemaVersion.ToString(),
                        viewNamingConvention.DetermineViewName(tableModel),
                        viewNamingConvention.DetermineViewModelName(tableModel),
                        viewNamingConvention.DetermineViewPluralisedModelName(tableModel),
                        "Table Description", isExcluded);

                    return viewBuilderConvention.BuildModel(view, tableModel,
                        models.Where(
                            x =>
                                !(x.DatabaseName == tableModel.DatabaseName && x.SchemaName == tableModel.SchemaName &&
                                    x.TableName == tableModel.TableName)));
                }
                    );
                
            }

            // go back and reprocess the view relations
            foreach (var view in views)
            {
                
                // look for foreign keys in all tables that make up the view
                List<ITableMetaModel> sources = new List<ITableMetaModel>();
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