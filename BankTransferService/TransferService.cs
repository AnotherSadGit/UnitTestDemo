using System;

namespace BankTransferService
{
    public class TransferService
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
