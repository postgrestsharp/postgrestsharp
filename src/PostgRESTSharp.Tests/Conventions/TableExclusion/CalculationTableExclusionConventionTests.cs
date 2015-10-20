using System.Collections.Generic;
using NUnit.Framework;
using PostgRESTSharp.Conventions;

namespace PostgRESTSharp.Tests.Conventions.TableExclusion
{

    [TestFixture]
    public class CalculationTableExclusionConventionTests
    {
        [TestCase("halo", "public", "calculator_answer_entry") ]
        [TestCase("halo", "public", "calculator_answer_options") ]
        [TestCase("halo", "public", "calculator_question") ]
        [TestCase("halo", "public", "calculator_question_calculator_answer") ]
        [TestCase("halo", "public", "calculator_question_set") ]
        [TestCase("halo", "public", "calculator_question_set_calculator_question") ]
        [TestCase("Halo", "public", "calculator_question_set_calculator_question") ]
        [TestCase("halo", "Public", "calculator_question_set_calculator_question") ]
        [TestCase("halo", "public", "Calculator_question_set_calculator_question") ]
        public void IsMatch_GivenTableMetaModelWithTableNameWhichContainsTheWordCalcualtion_ShouldReturnIsMatchTrue(string databaseName, string schemaName, string tableName)
        {

            //arrange
            var exclusionConvention = new CalculatorQuestionsTableExclusionConvention();
            TableMetaModel table = new TableMetaModel(databaseName, schemaName, tableName, new List<TableMetaModelColumn>(), 
                new List<TableMetaModelRelation>(), new List<TableMetaModelPrivilege>(), "", "", "", TableMetaModelTypeEnum.Table, "" );
            
            //action
            var response = exclusionConvention.IsMatch(table);

            //assert
            Assert.AreEqual(true, response);

        }

        [TestCase("halo", "public", "application")]
        [TestCase("halo", "public", "application$mortgage_loan")]
        [TestCase("halo", "1", "Calculator_question_set_calculator_question")]
        [TestCase("etl", "public", "calculator_question_set_calculator_question")]
        public void IsMatch_GivenTableMetaModelWithTableNameWhichDoesNotContainTheWordCalculator_ShouldReturnIsMatchFalse(string databaseName, string schemaName, string tableName)
        {

            //arrange
            var exclusionConvention = new CalculatorQuestionsTableExclusionConvention();
            TableMetaModel table = new TableMetaModel(databaseName, schemaName, tableName, new List<TableMetaModelColumn>(),
                new List<TableMetaModelRelation>(), new List<TableMetaModelPrivilege>(), "", "", "", TableMetaModelTypeEnum.Table, "");

            //action
            var response = exclusionConvention.IsMatch(table);

            //assert
            Assert.AreEqual(false, response);

        }

    }

}