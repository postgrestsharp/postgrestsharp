using System.Collections.Generic;

namespace PostgRESTSharp
{
    public interface IMetaModel
    {
        string DatabaseName { get; }

        string SchemaName { get; }

        string TableName { get; }

        string ModelName { get; }

        string ModelNameCamelCased { get; }

        string ModelNamePluralised { get; }

		MetaModelTypeEnum MetaModelType { get; }

        IEnumerable<MetaModelColumn> Columns { get; }

        IEnumerable<MetaModelColumn> InsertColumns { get; }

        IEnumerable<MetaModelColumn> PrimaryKeyColumns { get; }

        IEnumerable<MetaModelRelation> Relations { get; }

        IEnumerable<MetaModelColumn> UpdateColumns { get; }

        bool HasPrimaryKeys();

        bool HasAutoIncrementPrimaryKey();
    }
}