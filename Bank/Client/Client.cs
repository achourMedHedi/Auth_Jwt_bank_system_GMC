using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Client
{
    class Client<TClientKey, TAccountKey, TTransactionKey> : IClient<TClientKey, TAccountKey, TTransactionKey> , IComparable, IEquatable<TClientKey>
        where TClientKey : IEquatable<TClientKey>, IComparable<TClientKey>
        where TAccountKey : IEquatable<TAccountKey>, IComparable<TAccountKey>
        where TTransactionKey : IEquatable<TTransactionKey>, IComparable<TTransactionKey>
    {
        public TClientKey Cin { get ; set ; }
        public string Name { get; set; }
        public List<IAccount<TAccountKey, TTransactionKey>> Accounts { get ; set; }

        public Client(TClientKey cin , string name)
        {
            Cin = cin;
            Name = name;
            Accounts = new List<IAccount<TAccountKey, TTransactionKey>>();
        }

        public int CompareTo(object obj)
        {
            return Cin.CompareTo(((IClient<TClientKey, TAccountKey, TTransactionKey>)obj).Cin);
        }

        public bool Equals(TClientKey other)
        {
            return Cin.Equals(other);
        }

        public IEnumerable<IAccount<TAccountKey, TTransactionKey>> GetAllAccounts()
        {
            return Accounts;
        }

        public IAccount<TAccountKey, TTransactionKey> GetAccount(TAccountKey accountNumber)
        {
            return (from a in Accounts where a.AccountNumber.Equals(accountNumber) select a).FirstOrDefault();
        }

        public void CreateAccount(IAccount<TAccountKey, TTransactionKey> account)
        {
            Accounts.Add(account);
        }

        public void CloseAccount(IAccount<TAccountKey, TTransactionKey> account)
        {
            Accounts.RemoveAll(e => e.AccountNumber.Equals(account.AccountNumber));
        }
    }
}
