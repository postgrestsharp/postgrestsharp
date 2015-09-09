
using System;
using System.Linq;
using System.Collections.Generic;
using PostgRESTSharp.Conventions;
using PostgRESTSharp.Text;

namespace PostgRESTSharp.Conventions
{
	public class MultiTableViewMetaModelBuilderConvention : IViewMetaModelBuilderConvention, IImplicitTableConvention
	{
        private ITextUtility textUtility;

		public MultiTableViewMetaModelBuilderConvention(ITextUtility textUtility)
		{
            this.textUtility = textUtility;
        }

		public bool IsMatch (IMetaModel metaModel)
		{
			return metaModel.TableName.Contains("$");
		}

		public ViewMetaModelBuilderResult BuildModel (IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName)
		{

				var model = new ViewMetaModel (storageModel.DatabaseName, viewSchemaName, storageModel.ModelNameCamelCased, 
                    this.textUtility.ToCapitalCase(storageModel.TableName),
                    this.textUtility.ToPluralCapitalCase(storageModel.TableName));
                // there are multiple tables involved
                IMetaModel currentTable = null;
                string currentTableName = "";
                var tableNames = storageModel.TableName.Split(new char[] { '$' });
                bool isPrimary = true;
                foreach(var tableName in tableNames)
                {
                    currentTableName = this.BuildTableName(currentTableName, tableName);
                    var table = additionalStorageModels.Where(x => x.DatabaseName == storageModel.DatabaseName && x.SchemaName == storageModel.SchemaName && x.TableName == currentTableName).FirstOrDefault();
                    if(table == null)
                    {
                        if(currentTableName == storageModel.TableName)
                        {
                            table = storageModel;
                        }
                    }
                    //find the actual table

                    if(isPrimary)
                    {
                        model.SetPrimaryTableSource(table);
                        isPrimary = false;
                    }
                    else
                    {
                        // establish the join column between this and the primary source
                        var relation = currentTable.Relations.Where(x => x.RelationTableName == currentTableName).FirstOrDefault();
                        if(relation != null)
                        {
                            // get the source column
                            var sourceColumnName = relation.RelationColumns.FirstOrDefault();
                            var sourceColumn = currentTable.Columns.Where(x => x.ColumnName == sourceColumnName).FirstOrDefault();
                            // get the related table column
                            var joinColumnName = relation.UniqueColumns.FirstOrDefault();
                            var joinColumn = table.Columns.Where(x => x.ColumnName == joinColumnName).FirstOrDefault();
                            model.AddJoinSource(table, joinColumn, currentTable, sourceColumn);
                        }

                        // 
                    }

                    // add the columns from the table
                    if (table != null)
                    {
                        foreach (var col in table.Columns)
                        {
                            // always exclude join columns
                            var joinTable = model.JoinSources.Where(x => x.JoinSource == table).FirstOrDefault();
                            if ( joinTable!= null)
                            {
                                if(joinTable.JoinColumn == col)
                                {
                                    continue;
                                }
                            }

                            // use conventions here to check if a column should be included

                            model.AddColumn(col, table);
                        }
                    }
                    currentTable = table;
                    
                }

                return new ViewMetaModelBuilderResult(true, model);
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