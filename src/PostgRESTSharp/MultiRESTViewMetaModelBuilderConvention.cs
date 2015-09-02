using System;
using System.Linq;
using System.Collections.Generic;

namespace PostgRESTSharp
{
	public class MultiRESTViewMetaModelBuilderConvention : IViewMetaModelBuilderConvention
	{
		public MultiRESTViewMetaModelBuilderConvention()
		{
			this.Level = ViewModelBuilderConventionLevel.Convention;
			this.ConventionType = ViewModelBuilderConventionType.Inclusion;
		}

		public ViewMetaModelBuilderResult BuildModel (IMetaModel storageModel, IEnumerable<IMetaModel> additionalStorageModels, string viewSchemaName)
		{
			if (storageModel.TableName.Contains ("$")) 
			{
				var model = new ViewMetaModel (storageModel.DatabaseName, viewSchemaName, storageModel.ModelNameCamelCased);
                // there are multiple tables involved
                IMetaModel currentTable = null;
                string currentTableName = "";
                var tableNames = storageModel.TableName.Split(new char[] { '$' });
                bool isPrimary = true;
                foreach(var tableName in tableNames)
                {
                    currentTableName = this.BuildTableName(currentTableName, tableName);
                    var table = additionalStorageModels.Where(x => x.DatabaseName == storageModel.DatabaseName && x.SchemaName == storageModel.SchemaName && x.TableName == currentTableName).FirstOrDefault();
                    //find the actual table

                    if(isPrimary)
                    {
                        model.SetPrimaryTableSource(table);
                        isPrimary = false;
                    }
                    else
                    {
                        // establish the join column between this and the primary source
                        // I should get this from the foreign key but its not there
                        int a = 0;
                        // model.AddJoinSource(table, )
                    }

                    // add the columns from the table
                    foreach (var col in table.Columns)
                    {
                        // use conventions here to check if a column should be included

                        model.AddColumn(col, table);
                    }
                    currentTable = table;
                    
                }

                return new ViewMetaModelBuilderResult(true, model);
            } 
			else 
			{
				return new ViewMetaModelBuilderResult (false, null);
			}
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

        public ViewModelBuilderConventionType ConventionType { get; protected set; }

		public ViewModelBuilderConventionLevel Level  { get; protected set; }

	}
}