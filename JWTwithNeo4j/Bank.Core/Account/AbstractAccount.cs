using Bank.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bank.Account
{
    public abstract class AbstractAccount<TClientKey, TAccountKey, TTransactionKey> : IAccount<TAccountKey, TTransactionKey>, IEquatable<IAccount<TAccountKey, TTransactionKey>> , IComparable<IAccount<TAccountKey, TTransactionKey>>
        where TAccountKey : IEquatable<TAccountKey> , IComparable<TAccountKey>
    {
        public double Balance { get; set; }
        public TClientKey Owner { get; set; }
        public DateTime Date { get; set; }
        public TAccountKey AccountNumber { get; set; }
        public State State { get; set; }
        public double TaxRatio { get; set; }
        public List<Transaction<TTransactionKey,TAccountKey>> transactions { get; set; }

        public AbstractAccount(TClientKey owner, TAccountKey accountNumber)
        {
            Balance = 0;
            AccountNumber = accountNumber;
            Owner = owner;
            transactions = new List<Transaction.Transaction<TTransactionKey, TAccountKey>>();
            Date = new DateTime();
            State = State.Active;
        }


        public IEnumerable<Transaction.Transaction<TTransactionKey, TAccountKey>> GetAllTransactions()
        {
            return transactions;
        }

        public static bool operator ==(AbstractAccount<TClientKey, TAccountKey, TTransactionKey> a, AbstractAccount<TClientKey, TAccountKey, TTransactionKey> b)
        {
            return a.AccountNumber.Equals(b.AccountNumber);
            //AccountNumber.Equals(b.AccountNumber)
        }
        public static bool operator !=(AbstractAccount<TClientKey, TAccountKey, TTransactionKey> a, AbstractAccount<TClientKey, TAccountKey, TTransactionKey> b)
        {
            return !a.AccountNumber.Equals(b.AccountNumber);

            //return !a.AccountNumber.Equals(b.AccountNumber);
        }
        
        public virtual void Debit(double amount, TTransactionKey transactionNumber, TAccountKey targetKey)
        {
            Transaction<TTransactionKey, TAccountKey> transaction = new Transaction<TTransactionKey, TAccountKey>(Direction.Outgoing, Transaction.State.Ready, transactionNumber, AccountNumber, targetKey, amount);
            // LoggingTransation((TTransaction)transaction);
            if (Balance > amount)
            {
                transaction.State = Transaction.State.Accepted;
                Balance -= amount;
                SendMoney(transaction);
            }
            else
            {
                transaction.State = Transaction.State.Rejected;
                LoggingTransation(transaction);
                throw new Exception("amount too high");

            }
        }

        public virtual void Credit(double amount, TAccountKey sourceTransactionKey, TTransactionKey transactionNumber)
        {
            if (State == State.Closed)
            {
                throw new Exception("account closed");
            }
            else
            {
                Balance += amount;
                Transaction<TTransactionKey, TAccountKey> transaction = new Transaction<TTransactionKey, TAccountKey>(Direction.Incoming, Transaction.State.Accepted, transactionNumber, sourceTransactionKey, AccountNumber, amount);
                SendMoney((Transaction<TTransactionKey, TAccountKey>)transaction);
            }
        }

        public void SendMoney(Transaction<TTransactionKey, TAccountKey> transaction)
        {
            transactions.Add(transaction);
            // logging transaction
            LoggingTransation(transaction);
        }
        public void LoggingTransation(Transaction<TTransactionKey, TAccountKey> transaction)
        {
            // logging transaction

            Console.WriteLine("logging transaction " + transaction.TransactionNumber);
            /*var log = new LoggerConfiguration()
           .WriteTo.File(@"C:\Users\achou\Desktop\bankGmc\Next-Lvl-D3otNet\bankGoMyCode\bank.log")
           .CreateLogger();
            log.Information("Transaction : Executing transaction " + transaction.State + " " + transaction.TransactionNumber); ;*/

        }

        public IEnumerable<Transaction<TTransactionKey, TAccountKey>> GetTransactionsByDate(DateTime date)
        {
            return from t in transactions where t.Date.Equals(date) select t;
        }

        public IEnumerable<Transaction<TTransactionKey, TAccountKey>> GetTransactionsByTarget(TAccountKey targetAccountNumber)
        {
            return from t in transactions where t.Equals(targetAccountNumber) select t;
        }

        public IEnumerable<Transaction<TTransactionKey, TAccountKey>> GetTransactionsByQuery(Func<Transaction<TTransactionKey, TAccountKey>, bool> query)
        {
            return transactions.Where(query);
        }

        public bool Equals(IAccount<TAccountKey, TTransactionKey> other)
        {
            return AccountNumber.Equals(other.AccountNumber);
        }

        public int CompareTo(IAccount<TAccountKey, TTransactionKey> other)
        {
            return AccountNumber.CompareTo(other.AccountNumber);
        }
    }
}
