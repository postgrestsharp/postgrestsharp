using System.Collections.Generic;
using NUnit.Framework;
using PostgRESTSharp.Conventions;
using PostgRESTSharp.Text;

namespace PostgRESTSharp.Tests.Conventions.TableConventions.ViewNamingConventions
{
    [TestFixture]
    public class ViewNameForEnumTablesConventionTests
    {
        [Test]
        public void Process_GivenAnInputStringWithEnum_ShouldReturnStringWithoutEnum()
        {
            //arrange
            string input = "addressFormatEnum";
            string tableName = "address_format_enum";
            ITextUtility textUtility = new TextUtility();
            var metaModel = CreateDataModel(tableName, input);

            //action
            ViewNameForEnumTablesConvention viewNameForEnumTablesConvention = new ViewNameForEnumTablesConvention(textUtility);

            //assert
            string view = viewNameForEnumTablesConvention.DetermineViewName(metaModel);
            
            Assert.AreEqual("addressFormat", view);

        }

        [Test]
        public void Process_GivenAnInputStringWithTwoEnum_ShouldReturnStringWithoutSecondEnum()
        {
            //arrange
            string input = "renumerationTypeEnum";
            string tableName = "renumeration_type_enum";
            ITextUtility textUtility = new TextUtility();
            var metaModel = CreateDataModel(tableName, input);

            //action
            ViewNameForEnumTablesConvention viewNameForEnumTablesConvention = new ViewNameForEnumTablesConvention(textUtility);

            //assert
            string view = viewNameForEnumTablesConvention.DetermineViewName(metaModel);

            Assert.AreEqual("renumerationType", view);
        }

        [Test]
        public void IsMatch_GivenTableNameWhichDoesWithEnum_ShouldReturnTrue()
        {

            //arrange
            string input = "renumerationTypeEnum";
            string tableName = "renumeration_type_enum";
            ITextUtility textUtility = new TextUtility();
            var metaModel = CreateDataModel(tableName, input);

            //action
            ViewNameForEnumTablesConvention viewNameForEnumTablesConvention = new ViewNameForEnumTablesConvention(textUtility);
            bool isMatch = viewNameForEnumTablesConvention.IsMatch(metaModel);

            //assert
            Assert.IsTrue(isMatch);

        }

        [Test]
        public void IsMatch_GivenTableNameWhichDoesNotEndWithEnum_ShouldReturnFalse()
        {

            //arrange
            string input = "address";
            string tableName = "address";
            ITextUtility textUtility = new TextUtility();
            var metaModel = CreateDataModel(tableName, input);

            //action
            ViewNameForEnumTablesConvention viewNameForEnumTablesConvention = new ViewNameForEnumTablesConvention(textUtility);
            bool isMatch = viewNameForEnumTablesConvention.IsMatch(metaModel);

            //assert
            Assert.IsFalse(isMatch);

        }

        [Test]
        public void DetermineViewName_GivenTableNameInCamelCase_ShouldReturnCapitalCaseName()
        {

            //arrange
            string input = "renumerationTypeEnum";
            string tableName = "renumeration_type_enum";
            ITextUtility textUtility = new TextUtility();
            var metaModel = CreateDataModel(tableName, input);

            //action
            ViewNameForEnumTablesConvention viewNameForEnumTablesConvention = new ViewNameForEnumTablesConvention(textUtility);
            string viewName = viewNameForEnumTablesConvention.DetermineViewModelName(metaModel);

            //assert
            Assert.AreEqual("RenumerationType", viewName);
            
        }

        [Test]
        public void DetermineViewName_GivenTableNameInCamelCase_ShouldReturnPluralisedCapitalCase()
        {

            //arrange
            string input = "renumerationTypeEnum";
            string tableName = "renumeration_type_enum";
            ITextUtility textUtility = new TextUtility();
            var metaModel = CreateDataModel(tableName, input);

            //action
            ViewNameForEnumTablesConvention viewNameForEnumTablesConvention = new ViewNameForEnumTablesConvention(textUtility);
            string viewName = viewNameForEnumTablesConvention.DetermineViewPluralisedModelName(metaModel);

            //assert
            Assert.AreEqual("RenumerationTypes", viewName);
            
        }

        private static ITableMetaModel CreateDataModel(string tableName, string input)
        {
            return new TableMetaModel("halo", "halo", tableName, new List<TableMetaModelColumn>(),
                new List<TableMetaModelRelation>(),
                new List<TableMetaModelPrivilege>(),
                input, input, input,
                TableMetaModelTypeEnum.Table, "");
        }

    }

}