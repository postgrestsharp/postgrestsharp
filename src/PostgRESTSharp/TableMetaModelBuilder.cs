using PostgRESTSharp.Text;
using System.Collections.Generic;

namespace PostgRESTSharp
{
    public class TableMetaModelBuilder : ITableMetaModelBuilder
    {
        private ITextUtility textUtility;
        private ITableMetaModelTypeConvertor dataStorageTypeConvertor;

        public TableMetaModelBuilder(ITextUtility textUtility, ITableMetaModelTypeConvertor dataStorageTypeConvertor)
        {
            this.textUtility = textUtility;
            this.dataStorageTypeConvertor = dataStorageTypeConvertor;
        }

        public TableMetaModel BuildMetaModel(InfoSchemaTable table, IEnumerable<InfoSchemaForeignKeys> foreignKeys,
                                                   IEnumerable<InfoSchemaColumn> tableColumns, IEnumerable<InfoSchemaTableGrant> tablePrivileges)
        {
            List<TableMetaModelColumn> columns = new List<TableMetaModelColumn>();
            List<TableMetaModelRelation> relations = new List<TableMetaModelRelation>();
            List<TableMetaModelPrivilege> privileges = new List<TableMetaModelPrivilege>();

            string modelName = this.textUtility.ToCapitalCase(table.TableName);
            string camelCasedModelName = this.textUtility.ToCamelCase(table.TableName);
            string pluralisedModelName = this.textUtility.ToPluralCapitalCase(table.TableName);

            foreach (var col in tableColumns)
            {
                string columnName = this.textUtility.Sanitise(this.textUtility.ToCapitalCase(col.ColumnName));
                var storCol = new TableMetaModelColumn(col.ColumnName, col.IsPrimaryKeyColumn, col.IsAutoIncrementColumn, col.IsNullable, col.IsUpdatable, col.IsUnique, col.OrdinalPosition,
                    col.DataType, col.CharacterMaximumLength.HasValue ? col.CharacterMaximumLength.Value : 0,
                    columnName, textUtility.ToCamelCase(col.ColumnName),
                    this.dataStorageTypeConvertor.GetNativeTypeForSqlType(col.DataType));

                columns.Add(storCol);
            }

            foreach (var fk in foreignKeys)
            {
                RelationDirectionEnum direction = fk.Direction == "FORWARD" ? RelationDirectionEnum.Forward : RelationDirectionEnum.Reverse;
                var storRelation = new TableMetaModelRelation(fk.FkTableCatalog, fk.FkTableSchema, fk.FkTableName, fk.UqTableName, fk.FkConstraintName, new string[] { fk.FkColumnName }, new string[] { fk.UqColumnName }, direction);
                relations.Add(storRelation);
            }

            foreach(var tabPrivilege in tablePrivileges)
            {
                var privilege = new TableMetaModelPrivilege(tabPrivilege.PrivilegeType, tabPrivilege.Grantee);
                privileges.Add(privilege);
            }

            TableMetaModelTypeEnum type = table.TableType == "VIEW" ? TableMetaModelTypeEnum.View : TableMetaModelTypeEnum.Table;

            return new TableMetaModel(table.TableCatalog, table.TableSchema, table.TableName, columns, relations, privileges,
                                            modelName, camelCasedModelName, pluralisedModelName, type);
        }
    }
}