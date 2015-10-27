using NUnit.Framework;
using PostgRESTSharp.Text;

namespace PostgRESTSharp.Tests.Text
{
    [TestFixture]
    public class TextUtilityTests
    {
        [TestCase("application$mortgageloan", "mortgageloanApplications")]
        [TestCase("application$mortgageloan$new_purchase", "newPurchaseMortgageloanApplications")]
        [TestCase("application$mortgage_loan$refinance", "refinanceMortgageLoanApplications")]
        [TestCase("application$mortgage-loan$refinance", "refinanceMortgageLoanApplications")]
        [TestCase("application$mortgage-loan", "mortgageLoanApplications")]
        public void ToPluralCamelCase_GivenComplexTableName_ShouldReturnCamelCasePuralisedInReverseEntityOrder(string input, string expectedOutput)
        {
            
            //arrange
            var textUtility = new TextUtility();
            
            //action
            var result = textUtility.ToPluralCamelCase(input);

            //assert
            Assert.AreEqual(expectedOutput, result);

        }

        [TestCase("mortgage_loan_id", "mortgageLoanIds")]
        [TestCase("mortgage-loan_id", "mortgageLoanIds")]
        [TestCase("mortgage.loan_id", "mortgageLoanIds")]
        public void ToPluralCamelCase_GivenSimpleColumnName_ShouldReturnCamelCasePuralisedInTheOrderSupplied(string input, string expectedOutput)
        {
            
            //arrange
            var textUtility = new TextUtility();
            
            //action
            var result = textUtility.ToPluralCamelCase(input);

            //assert
            Assert.AreEqual(expectedOutput, result);

        }

        [TestCase("application$mortgageloan", "mortgageloanApplication")]
        [TestCase("application$mortgageloan$new_purchase", "newPurchaseMortgageloanApplication")]
        [TestCase("application$mortgage_loan$refinance", "refinanceMortgageLoanApplication")]
        [TestCase("application$mortgage-loan$refinance", "refinanceMortgageLoanApplication")]
        [TestCase("application$mortgage-loan", "mortgageLoanApplication")]
        public void ToCamelCase_GivenComplexTableName_ShouldReturnCamelCaseInReverseEntityOrder(string input, string expectedOutput)
        {
            
            //arrange
            var textUtility = new TextUtility();
            
            //action
            var result = textUtility.ToCamelCase(input);

            //assert
            Assert.AreEqual(expectedOutput, result);

        }

        [TestCase("mortgage_loan_id", "mortgageLoanId")]
        [TestCase("mortgage-loan_id", "mortgageLoanId")]
        [TestCase("mortgage.loan_id", "mortgageLoanId")]
        public void ToCamelCase_GivenSimpleColumnName_ShouldReturnCamelCasInTheOrderSupplied(string input, string expectedOutput)
        {
            
            //arrange
            var textUtility = new TextUtility();
            
            //action
            var result = textUtility.ToCamelCase(input);

            //assert
            Assert.AreEqual(expectedOutput, result);

        }
        
        [TestCase("application$mortgageloan", "MortgageloanApplication")]
        [TestCase("application$mortgageloan$new_purchase", "NewPurchaseMortgageloanApplication")]
        [TestCase("application$mortgage_loan$refinance", "RefinanceMortgageLoanApplication")]
        [TestCase("application$mortgage-loan$refinance", "RefinanceMortgageLoanApplication")]
        [TestCase("application$mortgage-loan", "MortgageLoanApplication")]
        public void ToCapitalCase_GivenComplexTableName_ShouldReturnCapitalCaseInReverseEntityOrder(string input, string expectedOutput)
        {

            //arrange
            var textUtility = new TextUtility();

            //action
            var result = textUtility.ToCapitalCase(input);

            //assert
            Assert.AreEqual(expectedOutput, result);

        }

        [TestCase("mortgage_loan_id", "MortgageLoanId")]
        [TestCase("mortgage-loan_id", "MortgageLoanId")]
        [TestCase("mortgage.loan_id", "MortgageLoanId")]
        public void ToCapitalCase_GivenSimpleColumnName_ShouldReturnCapitalCaseInTheOrderSupplied(string input, string expectedOutput)
        {

            //arrange
            var textUtility = new TextUtility();

            //action
            var result = textUtility.ToCapitalCase(input);

            //assert
            Assert.AreEqual(expectedOutput, result);

        }

        [TestCase("application$mortgageloan", "MortgageloanApplications")]
        [TestCase("application$mortgageloan$new_purchase", "NewPurchaseMortgageloanApplications")]
        [TestCase("application$mortgage_loan$refinance", "RefinanceMortgageLoanApplications")]
        [TestCase("application$mortgage-loan$refinance", "RefinanceMortgageLoanApplications")]
        [TestCase("application$mortgage-loan", "MortgageLoanApplications")]
        public void ToPluralCapitalCase_GivenComplexTableName_ShouldReturnCapitalCasePluralisedInReverseEntityOrder(string input, string expectedOutput)
        {

            //arrange
            var textUtility = new TextUtility();

            //action
            var result = textUtility.ToPluralCapitalCase(input);

            //assert
            Assert.AreEqual(expectedOutput, result);

        }

        [TestCase("mortgage_loan_id", "MortgageLoanIds")]
        [TestCase("mortgage-loan_id", "MortgageLoanIds")]
        [TestCase("mortgage.loan_id", "MortgageLoanIds")]
        public void ToPluralCapitalCase_GivenSimpleColumnName_ShouldReturnCapitalCasePluralisedInTheOrderSupplied(string input, string expectedOutput)
        {

            //arrange
            var textUtility = new TextUtility();

            //action
            var result = textUtility.ToPluralCapitalCase(input);

            //assert
            Assert.AreEqual(expectedOutput, result);

        }

    }

}