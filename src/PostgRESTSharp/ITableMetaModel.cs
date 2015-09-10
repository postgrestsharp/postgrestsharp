using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface ITableMetaModel
    {
        string DatabaseName { get; }

        string SchemaName { get; }

        string TableName { get; }

        string ModelName { get; }

        string ModelNameCamelCased { get; }

        string ModelNamePluralised { get; }

		TableMetaModelTypeEnum MetaModelType { get; }

        IEnumerable<TableMetaModelColumn> Columns { get; }

        IEnumerable<TableMetaModelColumn> InsertColumns { get; }

        IEnumerable<TableMetaModelColumn> PrimaryKeyColumns { get; }

        IEnumerable<TableMetaModelRelation> Relations { get; }

        IEnumerable<TableMetaModelColumn> UpdateColumns { get; }

        IEnumerable<TableMetaModelPrivilege> Privileges { get; }

        bool HasPrimaryKeys();

        bool HasAutoIncrementPrimaryKey();
    }
}