using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Client
{
    public interface IClient<TClientKey , TAccountkey , TTransactionKey>
        where TClientKey : IEquatable<TClientKey> , IComparable<TClientKey>
        where TAccountkey : IEquatable<TAccountkey> , IComparable<TAccountkey>
        where TTransactionKey : IEquatable<TTransactionKey> , IComparable<TTransactionKey>
    {
        TClientKey Cin { get; set; }
        string Name { get; set; }
        List<IAccount<TAccountkey, TTransactionKey>> Accounts { get; set; }

        IEnumerable<IAccount<TAccountkey ,TTransactionKey>> GetAllAccounts();
        IAccount<TAccountkey, TTransactionKey> GetAccount(TAccountkey accountNumber);
        void CreateAccount(IAccount<TAccountkey, TTransactionKey> account);
        void CloseAccount(IAccount<TAccountkey, TTransactionKey> account);
    }
}
