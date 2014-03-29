using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BankTransferService;

namespace Bank
{
    /// <summary>
    /// Bank account demo class.
    /// </summary>
    /// <remarks>Based on Walkthrough: Creating and Running Unit Tests for Managed Code, 
    /// http://msdn.microsoft.com/en-us/library/ms182532.aspx </remarks>
    public class BankAccount : Bank.IBankAccount
    {
        private string m_customerName;
        private double m_balance;
        private bool m_frozen = false;

        public BankAccount() { }

        private ITransferService _transferService;
        public BankAccount(ITransferService transferService)
        {
            _transferService = transferService;
        }

        public void Transfer(double amount, string destinationAccountName)
        {
            bool result = _transferService.SendFunds(amount, destinationAccountName);

            if (result == true)
            {
                m_balance -= amount;
            }
        }

        public void Debit(double amount)
        {
            if (m_frozen)
            {
                throw new Exception("Account frozen");
            }

            if (amount > m_balance)
            {
                throw new ArgumentOutOfRangeException("amount", amount, 
                    "Debit amount exceeds balance");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, 
                    "Debit amount less than zero");
            }

            m_balance -= amount;
        }

        public void Credit(double amount)
        {
            if (m_frozen)
            {
                throw new Exception("Account frozen");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance += amount;
        }

        private void FreezeAccount()
        {
            m_frozen = true;
        }

        private void UnfreezeAccount()
        {
            m_frozen = false;
        }

        public string CustomerName
        {
            get { return m_customerName; }
            set { m_customerName = value; }
        }

        public double Balance
        {
            get { return m_balance; }
            set { m_balance = value; }
        }
    }
}
