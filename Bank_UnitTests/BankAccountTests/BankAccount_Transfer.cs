using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Bank;
using BankTransferService;

namespace Bank_UnitTests.BankAccountTests
{
    [TestClass]
    public class BankAccount_Transfer
    {
        [TestMethod]
        public void Should_DecrementBalance_Given_ValidAmount()
        {
            // Arrange.
            Mock<ITransferService> transferServiceMock = new Mock<ITransferService>();
            transferServiceMock.Setup(transferService =>
                transferService.SendFunds(It.IsAny<double>(), It.IsAny<string>())).Returns(true);
            // We must pass the Object property of the mock object, not the mock object itself.
            BankAccount objectUnderTest = new BankAccount(transferServiceMock.Object)
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

            // Verify that the object under test did call the TransferService.SendFunds method 
            //  exactly one time.
            transferServiceMock.Verify(transferService =>
                transferService.SendFunds(It.IsAny<double>(), It.IsAny<string>()), Times.Once());
        }
    }
}
