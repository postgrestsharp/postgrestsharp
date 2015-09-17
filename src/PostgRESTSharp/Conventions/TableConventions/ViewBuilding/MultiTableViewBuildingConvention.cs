using AutoMapper.Internal;
using PostgRESTSharp.Conventions.ViewConventions.ViewFiltering;
using PostgRESTSharp.Text;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Conventions
{
    public class MultiTableViewBuildingConvention : IViewBuildingConvention, IImplicitTableConvention
    {
        private IConventionResolver conventionResolver;

        public MultiTableViewBuildingConvention(IConventionResolver conventionResolver)
        {
            this.conventionResolver = conventionResolver;
        }

        public bool IsMatch(ITableMetaModel metaModel)
        {
            return metaModel.TableName.Contains("$");
        }

        public IViewMetaModel BuildModel(IViewMetaModel viewToBuild, ITableMetaModel storageModel, IEnumerable<ITableMetaModel> additionalStorageModels)
        {
            // there are multiple tables involved
            ITableMetaModel currentTable = null;
            string currentTableName = "";
            var tableNames = storageModel.TableName.Split(new char[] { '$' });
            bool isPrimary = true;
            foreach (var tableName in tableNames)
            {
                currentTableName = this.BuildTableName(currentTableName, tableName);
                var table = additionalStorageModels.Where(x => x.DatabaseName == storageModel.DatabaseName && x.SchemaName == storageModel.SchemaName && x.TableName == currentTableName).FirstOrDefault();
                if (table == null)
                {
                    if (currentTableName == storageModel.TableName)
                    {
                        table = storageModel;
                    }
                }
                //find the actual table

                if (isPrimary)
                {
                    viewToBuild.SetPrimaryTableSource(table);
                    isPrimary = false;
                }
                else
                {
                    // establish the join column between this and the primary source
                    var relation = currentTable.Relations.Where(x => x.RelationTableName == currentTableName).FirstOrDefault();
                    if (relation != null)
                    {
                        // get the source column
                        var sourceColumnName = relation.RelationColumns.FirstOrDefault();
                        var sourceColumn = currentTable.Columns.Where(x => x.ColumnName == sourceColumnName).FirstOrDefault();
                        // get the related table column
                        var joinColumnName = relation.UniqueColumns.FirstOrDefault();
                        var joinColumn = table.Columns.Where(x => x.ColumnName == joinColumnName).FirstOrDefault();
                        viewToBuild.AddJoinSource(table, joinColumn, currentTable, sourceColumn);
                    }

                    //
                }

                // add the columns from the table
                if (table != null)
                {
                    foreach (var col in table.Columns)
                    {
                        // always exclude join columns
                        var joinTable = viewToBuild.JoinSources.Where(x => x.JoinSource == table).FirstOrDefault();
                        if (joinTable != null)
                        {
                            if (joinTable.JoinColumn == col)
                            {
                                continue;
                            }
                        }

                        // use conventions here to check if a column should be included

                        viewToBuild.AddColumn(col, table);
                    }
                }
                currentTable = table;
            }

            foreach (var filteringConvention in conventionResolver.ResolveViewConventions<IViewFilteringConvention>(viewToBuild))
            {
                viewToBuild.AddFilterElements(filteringConvention.FilterElements());
                viewToBuild.FilterElements.Each(x => x.SetTableName(viewToBuild.PrimarySource.TableName));
            }

            foreach (var columnRemovalConvention in conventionResolver.ResolveViewConventions<IColumnRemovalConvention>(viewToBuild))
            {
                viewToBuild.SetColumnToHidden(columnRemovalConvention.ColumnToRemove());
            }
            
            return viewToBuild;
        }

        private string BuildTableName(string currentTableName, string tableName)
        {
            if (currentTableName.Length == 0)
            {
                return tableName;
            }
            else
            {
                return string.Format("{0}${1}", currentTableName, tableName);
            }
        }
    }
}