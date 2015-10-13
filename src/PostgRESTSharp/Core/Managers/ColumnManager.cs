using System;
using System.Collections.Generic;
using System.Linq;

namespace PostgRESTSharp.Core.Managers
{
    public static class ColumnManager
    {
        public static void AddViewColumns(IViewMetaModel viewToBuild, ITableMetaModel table)
        {

            foreach (var col in table.Columns)
            {
                // always exclude join columns
                bool relatedTableIsLookup = false;
                var joinTable = viewToBuild.JoinSources.Where(x => x.JoinSource == table).FirstOrDefault();
                if (joinTable != null)
                {
                    if (joinTable.JoinColumn == col)
                    {
                        continue;
                    }
                    relatedTableIsLookup = joinTable.JoinSource.TableName.EndsWith("_enum");
                }

                // use conventions here to check if a column should be included


                //add the enum lookup
                joinTable = viewToBuild.JoinSources.Where(x => x.SourceColumn == col).FirstOrDefault(); // && !table.TableName.EndsWith("_enum") && relatedTableIsLookup);
                var isComplexType = joinTable != null;

                if (isComplexType)
                {
                    if (joinTable.JoinSource.TableName.EndsWith("_enum", StringComparison.InvariantCultureIgnoreCase))
                    {

                        var relateModelName = joinTable.JoinSource.ModelNameCamelCased;
                        if (relateModelName.EndsWith("enum", StringComparison.InvariantCultureIgnoreCase))
                        {
                            relateModelName = relateModelName.Substring(0, relateModelName.Length - 4);
                        }

                        var relationship = new JoinRelationModel();
                        relationship.RelatedModelName = relateModelName;
                        relationship.SourceModelName = joinTable.Source.ModelNameCamelCased;

                        var relaltions = new List<KeyValuePair<string, string>>();
                        viewToBuild.AddColumn(col, table, "", isComplexType, relationship);

                        foreach (var foreignColumn in joinTable.JoinSource.Columns)
                        {
                            if (!foreignColumn.ColumnName.Equals("is_Active", StringComparison.InvariantCultureIgnoreCase))
                            {
                                var columnName = StringManager.ForceStringToStartWithUpper(foreignColumn.FieldName);
                                relaltions.Add(new KeyValuePair<string, string>(relateModelName + columnName,
                                    foreignColumn.FieldName));
                                if (
                                    !joinTable.JoinColumn.ColumnName.Equals(foreignColumn.ColumnName,
                                        StringComparison.InvariantCultureIgnoreCase))
                                {
                                    viewToBuild.AddColumn(foreignColumn, joinTable.JoinSource,
                                        relateModelName + columnName, isComplexType, null);
                                }
                            }
                        }

                        relationship.Fields = relaltions;
                    }
                }
                else
                {
                    viewToBuild.AddColumn(col, table, "", false, null);
                }
            }
        }
    }
}