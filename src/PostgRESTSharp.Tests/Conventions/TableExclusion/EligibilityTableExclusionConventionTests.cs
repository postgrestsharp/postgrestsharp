using System.Collections.Generic;
using NUnit.Framework;
using PostgRESTSharp.Conventions;

namespace PostgRESTSharp.Tests.Conventions.TableExclusion
{

    [TestFixture]
    public class EligibilityTableExclusionConventionTests
    {
        [TestCase("halo", "public", "eligibility_answer_entry") ]
        [TestCase("halo", "public", "eligibility_answer_options") ]
        [TestCase("halo", "public", "eligibility_question") ]
        [TestCase("halo", "public", "eligibility_question_eligibility_answer") ]
        [TestCase("halo", "public", "eligibility_question_set") ]
        [TestCase("halo", "public", "eligibility_question_set_eligibility_question") ]
        [TestCase("Halo", "public", "eligibility_question_set_eligibility_question") ]
        [TestCase("halo", "Public", "eligibility_question_set_eligibility_question") ]
        [TestCase("halo", "public", "eligibility_question_set_eligibility_question") ]
        public void IsMatch_GivenTableMetaModelWithTableNameWhichContainsTheWordEligibilityAndIsInTheCorrectSchemaOrDatabase_ShouldReturnIsMatchTrue(string databaseName, string schemaName, string tableName)
        {

            //arrange
            var exclusionConvention = new EligibilityQuestionsTableExclusionConvention();
            TableMetaModel table = new TableMetaModel(databaseName, schemaName, tableName, new List<TableMetaModelColumn>(), 
                new List<TableMetaModelRelation>(), new List<TableMetaModelPrivilege>(), "", "", "", TableMetaModelTypeEnum.Table, "" );
            
            //action
            var response = exclusionConvention.IsMatch(table);

            //assert
            Assert.AreEqual(true, response);

        }

        [TestCase("halo", "public", "application")]
        [TestCase("halo", "public", "application$mortgage_loan")]
        [TestCase("halo", "1", "eligibility_question_set_eligibility_question")]
        [TestCase("etl", "public", "eligibility_question_set_eligibility_question")]
        public void IsMatch_GivenTableMetaModelWithTableNameWhichDoesNotContainTheWordEligibilityAndIsNotInTheCorrectSchemaOrDatabase_ShouldReturnIsMatchFalse(string databaseName, string schemaName, string tableName)
        {

            //arrange
            var exclusionConvention = new EligibilityQuestionsTableExclusionConvention();
            TableMetaModel table = new TableMetaModel(databaseName, schemaName, tableName, new List<TableMetaModelColumn>(),
                new List<TableMetaModelRelation>(), new List<TableMetaModelPrivilege>(), "", "", "", TableMetaModelTypeEnum.Table, "");

            //action
            var response = exclusionConvention.IsMatch(table);

            //assert
            Assert.AreEqual(false, response);

        }

    }

}