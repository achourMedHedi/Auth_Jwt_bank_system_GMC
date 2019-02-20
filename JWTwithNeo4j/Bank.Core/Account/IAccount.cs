using Bank.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public interface IAccount<TAccountKey, TTransactionKey>
        where TAccountKey : IComparable<TAccountKey> , IEquatable<TAccountKey>
    {
        TAccountKey AccountNumber { get; set; }
        double Balance { get; set; }
        State State { get; set; }
        List<Transaction<TTransactionKey, TAccountKey>> transactions { get; set; }

        IEnumerable<Transaction<TTransactionKey, TAccountKey>> GetAllTransactions();
        IEnumerable<Transaction<TTransactionKey, TAccountKey>> GetTransactionsByDate(DateTime date);
        IEnumerable<Transaction<TTransactionKey, TAccountKey>> GetTransactionsByTarget(TAccountKey targetAccountNumber);
        IEnumerable<Transaction<TTransactionKey, TAccountKey>> GetTransactionsByQuery(Func<Transaction<TTransactionKey, TAccountKey>, bool> query);

        void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey);
        void Credit(double amount, TAccountKey sourceTransactionKey, TTransactionKey transactionNumber);
        void SendMoney(Transaction<TTransactionKey, TAccountKey> transaction);

    }
}
