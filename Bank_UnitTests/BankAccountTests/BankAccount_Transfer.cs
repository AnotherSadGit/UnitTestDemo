using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Bank;

namespace Bank_UnitTests.BankAccountTests
{
    [TestClass]
    public class BankAccount_Transfer
    {
        [TestMethod]
        public void Should_DecrementBalance_Given_ValidAmount()
        {
            // Arrange.
            BankAccount objectUnderTest = new BankAccount()
                {
                    CustomerName = "Mr. John Smith",
                    Balance = 12.00
                };
            double transferAmount = 4.50;
            string destinationAccountName = "Jane Doe";
            double initialBalance = objectUnderTest.Balance;

            // Act.
            objectUnderTest.Transfer(transferAmount, destinationAccountName);

            // Assert.
            double expectedResult = initialBalance - transferAmount;  // Correct result: 7.50
            double actualResult = objectUnderTest.Balance;
            double comparisonPrecision = 0.001;
            Assert.AreEqual(expectedResult, actualResult, comparisonPrecision,
                "Account balance incorrect after transfer.");
        }
    }
}
