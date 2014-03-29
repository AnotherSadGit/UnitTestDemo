using System;
namespace Bank
{
    public interface IBankAccount
    {
        double Balance { get; set; }
        void Credit(double amount);
        string CustomerName { get; set; }
        void Debit(double amount);
        void Transfer(double amount, string destinationAccountName);
    }
}
