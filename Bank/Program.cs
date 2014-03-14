using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            string accountName = "Mr. Bryan Walton";
            double openingBalance = 11.99;
            Console.WriteLine("Opening balance is ${0}", openingBalance);

            BankAccount ba = new BankAccount();

            ba.CustomerName = accountName;
            ba.Balance = openingBalance;

            Console.WriteLine();
            double amountToCredit = 5.77;
            Console.WriteLine("Amount to be credited to account is ${0}", amountToCredit);
            ba.Credit(amountToCredit);
            Console.WriteLine("New balance is ${0}", ba.Balance);

            Console.WriteLine();
            double amountToDebit = 11.22;
            Console.WriteLine("Amount to be debited from account is ${0}", amountToDebit);
            ba.Debit(amountToDebit);
            Console.WriteLine("New balance is ${0}", ba.Balance);

            Console.WriteLine();
            double amountToTransfer = 5;
            string destinationAccountName = "John Smith";
            Console.WriteLine("Amount to be transferred to another account is ${0}", amountToTransfer);
            ba.Transfer(amountToTransfer, destinationAccountName);
            Console.WriteLine("New balance is ${0}", ba.Balance);

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
