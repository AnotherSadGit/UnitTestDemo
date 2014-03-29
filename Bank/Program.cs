using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

using BankTransferService;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            string accountName = "Mr. Bryan Walton";
            double openingBalance = 11.99;
            Console.WriteLine("Opening balance is ${0}", openingBalance);

            IUnityContainer diContainer = new UnityContainer();
            diContainer.LoadConfiguration();

            // Unity will recursively resolves all object references: 
            //  IBankAccount will be resolved to a BankAccount object, 
            //	When it tries to create a BankAccount object, Unity will see the BankAccount 
            //      constructor takes an ITransferService argument,
            //  ITransferService will be resolved to a TransferService object.
            IBankAccount ba = diContainer.Resolve<IBankAccount>();

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
