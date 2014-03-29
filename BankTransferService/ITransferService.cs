using System;
namespace BankTransferService
{
    public interface ITransferService
    {
        bool ReceiveFunds(double amount, string destinationAccountName);
        bool SendFunds(double amount, string destinationAccountName);
    }
}
