using System;

namespace BankTransferService
{
    public class TransferService : BankTransferService.ITransferService
    {
        public bool SendFunds(double amount, string destinationAccountName)
        {
            return false;
        }

        public bool ReceiveFunds(double amount, string destinationAccountName)
        {
            return true;
        }
    }
}
