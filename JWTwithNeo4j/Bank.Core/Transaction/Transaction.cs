using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Transaction
{
    public class Transaction<TTransactionKey, TAccountkey> : IEquatable<TAccountkey> , IComparable<TAccountkey>
        where TAccountkey : IComparable<TAccountkey> , IEquatable<TAccountkey>
    {
        public TTransactionKey TransactionNumber { get; set; }
        public TAccountkey SourceAccountNUmber { get; set; }
        public TAccountkey TargetAccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public State State { get; set; }
        public Direction Direction { get; set; }

        public Transaction() { }
        public Transaction(Direction direction, State state, TTransactionKey transactionNumber, TAccountkey sourceAccountNumber, TAccountkey targetAccountNumber, double amount)
        {
            TransactionNumber = transactionNumber;
            SourceAccountNUmber = sourceAccountNumber;
            TargetAccountNumber = targetAccountNumber;
            Amount = amount;
            Date = new DateTime();
            State = state;
            Direction = direction;
        }

        public bool Equals(TAccountkey other)
        {
            return TargetAccountNumber.Equals(other);
        }

        public int CompareTo(TAccountkey other)
        {
            return TargetAccountNumber.CompareTo(other);
        }
    }
}
