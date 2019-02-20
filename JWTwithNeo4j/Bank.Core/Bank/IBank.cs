using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Bank.Client;
using Bank.Transaction;

namespace Bank.Core.Bank
{
    public interface IBank<TClientKey, TAccountKey, TTransactionKey>
      where TClientKey : IEquatable<TClientKey>, IComparable<TClientKey>
      where TAccountKey : IEquatable<TAccountKey>, IComparable<TAccountKey>
      where TTransactionKey : IEquatable<TTransactionKey>, IComparable<TTransactionKey>
    {
        string Name { get; set; }
        string SwiftCode { get; set; }
        IList<IClient<TClientKey,TAccountKey,TTransactionKey>> Clients { get; set; }
        IEnumerable<Transaction<TTransactionKey ,TAccountKey>> GetAllTransations();
        IEnumerable<IAccount<TAccountKey , TTransactionKey>> GetAllAccounts();
        void AddTranstion(Transaction<TTransactionKey , TAccountKey> transaction);
        void AddAgent(int number);
        void RemoveAgent(int number);
        void SaveFile(string path);
        IBank<TClientKey, TAccountKey, TTransactionKey> LoadFile(string path);
    }
}
