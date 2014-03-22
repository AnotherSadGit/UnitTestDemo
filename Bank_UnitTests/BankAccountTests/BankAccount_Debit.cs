using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Bank;

namespace Bank_UnitTests.BankAccountTests
{
    [TestClass]
    public class BankAccount_Debit
    {
        [TestMethod]
        public void Should_DecrementBalance_Given_ValidAmount()
        {
            // Arrange.
            BankAccount account = new BankAccount()
                {
                    CustomerName = "Mr. John Smith",
                    Balance = 12.00
                };
            double debitAmount = 4.50;

            // Act.
            account.Debit(debitAmount);

            // Assert.
            double expectedResult = 7.50;  
            double actualResult = account.Balance;
            double comparisonPrecision = 0.001;
            Assert.AreEqual(expectedResult, actualResult, comparisonPrecision,
                "Account balance incorrect after debit.");
        }

        [TestMethod]
        public void Should_ThrowArgumentOutOfRangeException_If_AmountNegative()
        {
            // Arrange.
            double initialBalance = 12.00;
            BankAccount account = new BankAccount()
            {
                CustomerName = "Mr. John Smith",
                Balance = initialBalance
            };
            double debitAmount = -3.00;

            // Act.
            Exception exceptionThrown = null;
            try
            {
                account.Debit(debitAmount);
            }
            catch (Exception ex)
            {
                exceptionThrown = ex;
            }

            // Assert.
            double expectedResult = initialBalance;  
            double actualResult = account.Balance;
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
