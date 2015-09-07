using PostgRESTSharp.Text;
using System.Collections.Generic;

namespace PostgRESTSharp
{
    public class MetaModelBuilder : IMetaModelBuilder
    {
        private ITextUtility textUtility;
        private IMetaModelTypeConvertor dataStorageTypeConvertor;
        private IEnumerable<IMetaModelFieldNamingConvention> fieldNamingConventions;

        public MetaModelBuilder(ITextUtility textUtility, IMetaModelTypeConvertor dataStorageTypeConvertor,
                                           IEnumerable<IMetaModelFieldNamingConvention> fieldNamingConventions)
        {
            this.textUtility = textUtility;
            this.dataStorageTypeConvertor = dataStorageTypeConvertor;
            this.fieldNamingConventions = fieldNamingConventions;
        }

        public MetaModel BuildMetaModel(InfoSchemaTable table, IEnumerable<InfoSchemaForeignKeys> foreignKeys,
                                                   IEnumerable<InfoSchemaColumn> tableColumns)
        {
            List<MetaModelColumn> columns = new List<MetaModelColumn>();
            List<MetaModelRelation> relations = new List<MetaModelRelation>();

            string modelName = this.textUtility.ToCapitalCase(table.TableName);
            string camelCasedModelName = this.textUtility.ToCamelCase(table.TableName);
            string pluralisedModelName = this.textUtility.ToPluralCapitalCase(table.TableName);

            foreach (var col in tableColumns)
            {
                string columnName = this.textUtility.Sanitise(this.textUtility.ToCapitalCase(col.ColumnName));
                foreach (var namingConvention in this.fieldNamingConventions)
                {
                    columnName = namingConvention.Process(columnName);
                }
                var storCol = new MetaModelColumn(col.ColumnName, col.IsPrimaryKeyColumn, col.IsAutoIncrementColumn, col.IsNullable, col.IsUpdatable, col.IsUnique, col.OrdinalPosition,
                    col.DataType, col.CharacterMaximumLength.HasValue ? col.CharacterMaximumLength.Value : 0,
                    columnName, textUtility.ToCamelCase(col.ColumnName),
                    this.dataStorageTypeConvertor.GetNativeTypeForSqlType(col.DataType));

                columns.Add(storCol);
            }

            foreach (var fk in foreignKeys)
            {
                RelationDirectionEnum direction = fk.Direction == "FORWARD" ? RelationDirectionEnum.Forward : RelationDirectionEnum.Reverse;
                var storRelation = new MetaModelRelation(fk.FkTableCatalog, fk.FkTableSchema, fk.FkTableName, fk.UqTableName, fk.FkConstraintName, new string[] { fk.FkColumnName }, new string[] { fk.UqColumnName }, direction);
                relations.Add(storRelation);
            }

            MetaModelTypeEnum type = table.TableType == "VIEW" ? MetaModelTypeEnum.View : MetaModelTypeEnum.Table;

            return new MetaModel(table.TableCatalog, table.TableSchema, table.TableName, columns, relations,
                                            modelName, camelCasedModelName, pluralisedModelName, type);
        }
    }
}