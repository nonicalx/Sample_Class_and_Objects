using System;
using System.Collections.Generic;
using System.Text;

namespace myApp
{
    public class BankAccount
    {
        public string Number { get; set; }
        public decimal Balance 
        { 
            get
        {
            decimal balance = 0;
            foreach (var item in allTransaction)
            {
                balance += item.Amount;
            }
            return balance;
        } 
        }
        public string Owner { get; set; }

        private static int accountNumberSeed = 1234567890;

        private List<Transaction> allTransaction = new List<Transaction>();
        
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be greater than positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransaction.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("The available balanc in your account is not adequate for this transaction");
            }
            var withdrawal = new Transaction(-amount,date, note);
            allTransaction.Add(withdrawal);

        }

        public BankAccount(string name, decimal initialBalance)
        {
            this.Owner = name;
            this.Number = accountNumberSeed.ToString();
            MakeDeposit(initialBalance,DateTime.Now,"intial balance");
            accountNumberSeed++;
        }

        public string GetAccountHistory()
        {
            var report = new StringBuilder();

            report.AppendLine("Date\tAmount\tNote");
            foreach (var item in allTransaction)
            {
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{item.Note}");
            }
            return report.ToString();
        }
    }
}