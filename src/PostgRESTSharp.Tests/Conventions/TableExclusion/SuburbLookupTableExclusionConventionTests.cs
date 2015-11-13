using System.Collections.Generic;
using NUnit.Framework;
using PostgRESTSharp.Conventions;

namespace PostgRESTSharp.Tests.Conventions.TableExclusion
{

    [TestFixture]
    public class SuburbLookupTableExclusionConventionTests
    {
        [TestCase("halo", "public", "suburb_lookup") ]
        public void IsMatch_GivenTableMetaModelWithTableNameWhichContainsTheWordSuburbLookupAndIsInTheCorrectSchemaOrDatabase_ShouldReturnIsMatchTrue(string databaseName, string schemaName, string tableName)
        {

            //arrange
            var exclusionConvention = new SuburbLookupExclusionConvention();
            TableMetaModel table = new TableMetaModel(databaseName, schemaName, tableName, new List<TableMetaModelColumn>(), 
                new List<TableMetaModelRelation>(), new List<TableMetaModelPrivilege>(), "", "", "", TableMetaModelTypeEnum.Table, "" );
            
            //action
            var response = exclusionConvention.IsMatch(table);

            //assert
            Assert.AreEqual(true, response);

        }

        [TestCase("halo", "public", "application")]
        [TestCase("halo", "public", "application$mortgage_loan")]
        [TestCase("halo", "1", "suburb_lookup")]
        [TestCase("halo", "etl", "suburb_lookup")]
        public void IsMatch_GivenTableMetaModelWithTableNameWhichDoesNotContainTheWordSuburb_LookupAndIsNotInTheCorrectSchemaOrDatabase_ShouldReturnIsMatchFalse(string databaseName, string schemaName, string tableName)
        {

            //arrange
            var exclusionConvention = new SuburbLookupExclusionConvention();
            TableMetaModel table = new TableMetaModel(databaseName, schemaName, tableName, new List<TableMetaModelColumn>(),
                new List<TableMetaModelRelation>(), new List<TableMetaModelPrivilege>(), "", "", "", TableMetaModelTypeEnum.Table, "");

            //action
            var response = exclusionConvention.IsMatch(table);

            //assert
            Assert.AreEqual(false, response);

        }

    }

}