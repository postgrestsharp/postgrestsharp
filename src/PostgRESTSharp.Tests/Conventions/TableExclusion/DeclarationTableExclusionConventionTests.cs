using System.Collections.Generic;
using NUnit.Framework;
using PostgRESTSharp.Conventions;

namespace PostgRESTSharp.Tests.Conventions.TableExclusion
{

    [TestFixture]
    public class DeclarationTableExclusionConventionTests
    {
        [TestCase("halo", "public", "declaration_answer_entry") ]
        [TestCase("halo", "public", "declaration_answer_options") ]
        [TestCase("halo", "public", "declaration_question") ]
        [TestCase("halo", "public", "declaration_question_declaration_answer") ]
        [TestCase("halo", "public", "declaration_question_set") ]
        [TestCase("halo", "public", "declaration_question_set_declaration_question") ]
        [TestCase("Halo", "public", "declaration_question_set_declaration_question") ]
        [TestCase("halo", "Public", "declaration_question_set_declaration_question") ]
        [TestCase("halo", "public", "Declaration_question_set_declaration_question") ]
        public void IsMatch_GivenTableMetaModelWithTableNameWhichContainsTheWordDeclaration_ShouldReturnIsMatchTrue(string databaseName, string schemaName, string tableName)
        {

            //arrange
            var exclusionConvention = new DeclationQuestionsTableExclusionConvention();
            TableMetaModel table = new TableMetaModel(databaseName, schemaName, tableName, new List<TableMetaModelColumn>(), 
                new List<TableMetaModelRelation>(), new List<TableMetaModelPrivilege>(), "", "", "", TableMetaModelTypeEnum.Table, "" );
            
            //action
            var response = exclusionConvention.IsMatch(table);

            //assert
            Assert.AreEqual(true, response);

        }

        [TestCase("halo", "public", "application")]
        [TestCase("halo", "public", "application$mortgage_loan")]
        [TestCase("halo", "1", "declaration_question_set_declaration_question")]
        [TestCase("etl", "public", "declaration_question_set_declaration_question")]
        public void IsMatch_GivenTableMetaModelWithTableNameWhichDoesNotContainTheWordDeclaration_ShouldReturnIsMatchFalse(string databaseName, string schemaName, string tableName)
        {

            //arrange
            var exclusionConvention = new DeclationQuestionsTableExclusionConvention();
            TableMetaModel table = new TableMetaModel(databaseName, schemaName, tableName, new List<TableMetaModelColumn>(),
                new List<TableMetaModelRelation>(), new List<TableMetaModelPrivilege>(), "", "", "", TableMetaModelTypeEnum.Table, "");

            //action
            var response = exclusionConvention.IsMatch(table);

            //assert
            Assert.AreEqual(false, response);

        }

    }

}