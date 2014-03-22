using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Bank;

namespace Bank_UnitTests.BankAccountTests
{
    [TestClass]
    public class BankAccount_Debit
    {
        BankAccount _objectUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            _objectUnderTest = new BankAccount()
                {
                    CustomerName = "Mr. John Smith",
                    Balance = 12.00
                };
        }

        [TestMethod]
        public void Should_DecrementBalance_Given_ValidAmount()
        {
            // Arrange.
            double debitAmount = 4.50;

            // Act.
            _objectUnderTest.Debit(debitAmount);

            // Assert.
            double expectedResult = 7.50;  
            double actualResult = _objectUnderTest.Balance;
            double comparisonPrecision = 0.001;
            Assert.AreEqual(expectedResult, actualResult, comparisonPrecision,
                "Account balance incorrect after debit.");
        }

        [TestMethod]
        public void Should_ThrowArgumentOutOfRangeException_If_AmountNegative()
        {
            // Arrange.
            double initialBalance = _objectUnderTest.Balance;
            double debitAmount = -3.00;

            // Act.
            Exception exceptionThrown = null;
            try
            {
                _objectUnderTest.Debit(debitAmount);
            }
            catch (Exception ex)
            {
                exceptionThrown = ex;
            }

            // Assert.
            double expectedResult = initialBalance;  
            double actualResult = _objectUnderTest.Balance;
            double comparisonPrecision = 0.001;
            Assert.AreEqual(expectedResult, actualResult, comparisonPrecision,
                "Account balance should not have changed when debit amount was negative.");

            Assert.IsNotNull(exceptionThrown, 
                "Expected an exception when debit amount was negative: No exception thrown.");

            Assert.IsInstanceOfType(exceptionThrown, typeof(ArgumentOutOfRangeException), 
                "Expected ArgumentOutOfRangeException when debit amount was negative.");

            string expectedErrorMessage = "Debit amount less than zero";
            string actualErrorMessage = exceptionThrown.Message;
            StringAssert.Contains(actualErrorMessage, expectedErrorMessage,
                "Incorrect error message for negative debit amount");
        }
    }
}
