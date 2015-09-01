using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgrestSharp.Pgsql
{
    public class PgSqlDataStorageQueryProvider : IMetaModelQueryProvider
    {
        public string GetTablesQuery(string databaseName, string schemaName)
        {
            return string.Format(@"select
                table_catalog as TableCatalog,
                table_schema as TableSchema,
                table_name as TableName,
                table_type as TableType
                from information_schema.tables where table_catalog = '{0}' and table_schema = '{1}' and (table_type = 'BASE TABLE' OR table_type = 'VIEW')", databaseName, schemaName);
        }

        public string GetColumnsQuery(string databaseName, string schemaName, string tableName)
        {
            return string.Format(@"select
                sc.table_catalog as TableCatalog,
                sc.table_schema as TableSchema,
                sc.table_name as TableName,
                sc.column_name as ColumnName,
                sc.ordinal_position OrdinalPosition,
                sc.column_default as ColumnDefault,
                case sc.is_nullable when 'YES' then true else false end as IsNullable,
                sc.data_type as DataType,
                sc.character_maximum_length as CharacterMaximumLength,
                sc.numeric_precision as NumericPrecision,
                sc.numeric_scale as NumericScale,
                sc.datetime_precision as DatetimePrecision,
            (select count(*) = 1
                    from information_schema.key_column_usage kcu
                    join information_schema.table_constraints tc on tc.constraint_catalog = kcu.constraint_catalog and tc.constraint_schema = kcu.constraint_schema and tc.constraint_name = kcu.constraint_name
                    where kcu.table_catalog = '{0}' and kcu.table_schema = '{1}' and kcu.table_name = '{2}' and kcu.column_name = sc.column_name and tc.constraint_type  = 'PRIMARY KEY') as IsPrimaryKeyColumn,

                (SELECT count(*) = 1
                    FROM   pg_attribute  a
                    JOIN   pg_constraint c ON c.conrelid  = a.attrelid
                                  AND c.conkey[1] = a.attnum
                    JOIN   pg_attrdef   ad ON ad.adrelid  = a.attrelid
                                  AND ad.adnum    = a.attnum
                    WHERE  a.attrelid = '""{0}"".""{1}"".""{2}""'::regclass
                    AND    a.attnum > 0
                    AND    NOT a.attisdropped
                    AND    a.atttypid = ANY('{{int,int8,int2}}'::regtype[]) -- integer type
                    AND    c.contype = 'p'                           -- PK
                    AND    array_length(c.conkey, 1) = 1             -- single column
                    AND    ad.adsrc = 'nextval('''
                            || (pg_get_serial_sequence (a.attrelid::regclass::text, a.attname))::regclass
                            || '''::regclass)'
                    AND    a.attname = sc.column_name) as IsAutoIncrementColumn,
                case sc.is_updatable when 'YES' then true else false end as IsUpdatable,
                (select count(*) = 1
                    from information_schema.key_column_usage kcu
                    join information_schema.table_constraints tc on tc.constraint_catalog = kcu.constraint_catalog and tc.constraint_schema = kcu.constraint_schema and tc.constraint_name = kcu.constraint_name
                    where kcu.table_catalog = '{0}' and kcu.table_schema = '{1}' and kcu.table_name = '{2}' and kcu.column_name = sc.column_name and tc.constraint_type  = 'UNIQUE') as IsUnique
                from information_schema.columns sc where sc.table_catalog = '{0}' and sc.table_schema = '{1}' and sc.table_name = '{2}' order by sc.ordinal_position", databaseName, schemaName, tableName);
        }

        public string GetColumnKeyUsageQuery(string databaseName, string schemaName, string tableName)
        {
            return string.Format(@"select
                kcu.constraint_catalog as ConstraintCatalog,
                kcu.constraint_schema as ConstraintSchema,
                kcu.constraint_name as ConstraintName,
                kcu.table_catalog as TableCatalog,
                kcu.table_schema as TableSchema,
                kcu.table_name as TableName,
                kcu.column_name as ColumnName,
                tc.constraint_type as ConstraintType,
                kcu.ordinal_position as OrdinalPosition,
                kcu.position_in_unique_constraint as PositionInUniqueConstraint
                from information_schema.key_column_usage kcu
                join information_schema.table_constraints tc on tc.constraint_catalog = kcu.constraint_catalog and tc.constraint_schema = kcu.constraint_schema and tc.constraint_name = kcu.constraint_name
                where kcu.table_catalog = '{0}' and kcu.table_schema = '{1}' and kcu.table_name = '{2}'", databaseName, schemaName, tableName); //and kcu.column_name = '{3}'
        }

        public string GetForeignKeysQuery(string databaseName, string schemaName, string tableName)
        {
            return string.Format(@"(select
                kcu1.table_catalog as FkTableCatalog,
                kcu1.table_schema as FkTableSchema,
                kcu1.table_name as FkTableName,
                kcu1.constraint_name as FkConstraintName,
                kcu1.column_name as FkColumnName,
                kcu1.ordinal_position as FkOrdinalPosition,
                kcu2.table_catalog as UqTableCatalog,
                kcu2.table_schema as UqTableSchema,
                kcu2.table_name as UqTableName,
                kcu2.constraint_name as UqConstraintName,
                kcu2.column_name as UqColumnName,
                kcu2.ordinal_position as UqOrdinalPosition,
                'FORWARD' as Direction
            from information_schema.referential_constraints rc
            join information_schema.key_column_usage kcu1
            on kcu1.constraint_catalog = rc.constraint_catalog
                and kcu1.constraint_schema = rc.constraint_schema
                and kcu1.constraint_name = rc.constraint_name
            join information_schema.key_column_usage kcu2
            on kcu2.constraint_catalog =
            rc.unique_constraint_catalog
                and kcu2.constraint_schema =
            rc.unique_constraint_schema
                and kcu2.constraint_name =
            rc.unique_constraint_name
                and kcu2.ordinal_position = kcu1.ordinal_position
                and kcu1.table_catalog = '{0}' and kcu1.table_schema = '{1}' and kcu1.table_name = '{2}'
                order by UqTableCatalog, UqTableSchema, UqTableName, UqConstraintName, UqColumnName, UqOrdinalPosition)
            union all
            (select
                kcu2.table_catalog as FkTableCatalog,
                kcu2.table_schema as FkTableSchema,
                kcu2.table_name as FkTableName,
                kcu2.constraint_name as FkConstraintName,
                kcu2.column_name as FkColumnName,
                kcu2.ordinal_position as FkOrdinalPosition,
                kcu1.table_catalog as UqTableCatalog,
                kcu1.table_schema as UqTableSchema,
                kcu1.table_name as UqTableName,
                kcu1.constraint_name as UqConstraintName,
                kcu1.column_name as UqColumnName,
                kcu1.ordinal_position as UqOrdinalPosition,
                'REVERSE' as Direction
            from information_schema.referential_constraints rc
            join information_schema.key_column_usage kcu1
            on kcu1.constraint_catalog = rc.constraint_catalog
                and kcu1.constraint_schema = rc.constraint_schema
                and kcu1.constraint_name = rc.constraint_name
            join information_schema.key_column_usage kcu2
            on kcu2.constraint_catalog =
            rc.unique_constraint_catalog
                and kcu2.constraint_schema =
            rc.unique_constraint_schema
                and kcu2.constraint_name =
            rc.unique_constraint_name
                and kcu2.ordinal_position = kcu1.ordinal_position
                and kcu2.table_catalog = '{0}' and kcu2.table_schema = '{1}' and kcu2.table_name = '{2}'
                order by UqTableCatalog, UqTableSchema, UqTableName, UqConstraintName, UqColumnName, UqOrdinalPosition)", databaseName, schemaName, tableName);
        }
    }
}