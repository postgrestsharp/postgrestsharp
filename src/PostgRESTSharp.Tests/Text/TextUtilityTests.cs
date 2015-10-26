using NUnit.Framework;
using PostgRESTSharp.Text;

namespace PostgRESTSharp.Tests.Text
{
    [TestFixture]
    public class TextUtilityTests
    {

        [Test]
        public void ToPluralCapitalCase_GivenComplexTableName_ShouldReturnCamelCasePuralisedInReverseEntityOrder()
        {
            
            //arrange
            var textUtility = new TextUtility();
            var tableName = "application$mortgageloan";

            //action
            var result = textUtility.ToPluralCamelCase(tableName);

            //assert
            Assert.AreEqual("mortgageloanapplications", result);

        }
         
    }

}