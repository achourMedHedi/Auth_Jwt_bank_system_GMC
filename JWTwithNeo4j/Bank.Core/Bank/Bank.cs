using Bank.Client;
using Bank.Transaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Bank.Core.Bank
{
    public class Bank<TClientKey , TAccountKey , TTransactionKey> : IBank<TClientKey, TAccountKey, TTransactionKey>
        where TClientKey : IEquatable<TClientKey>, IComparable<TClientKey>
        where TAccountKey : IEquatable<TAccountKey>, IComparable<TAccountKey>
        where TTransactionKey : IEquatable<TTransactionKey>, IComparable<TTransactionKey>
    {
        public string Name { get; set; }
        public string SwiftCode { get; set; }
        public IList<IClient<TClientKey , TAccountKey , TTransactionKey>> Clients { get; set; }

        private readonly object LockTransaction = new object();

        private Queue<Transaction<TTransactionKey , TAccountKey>> TransactionQueue = new Queue<Transaction<TTransactionKey , TAccountKey>>();
        public Bank()
        {
            Name = "achour";
            SwiftCode = "si4445s";
            Clients = new List<IClient<TClientKey, TAccountKey, TTransactionKey>>();
        }
        public Bank (string name , string swiftCode)
        {
            Name = name;
            SwiftCode = swiftCode;
            Clients = new List<IClient<TClientKey, TAccountKey, TTransactionKey>>();
        }

        public void AddAgent(int number=1)
        {
            for (int i = 0; i < number; i++)
            {
                Thread thread = new Thread(new ThreadStart(ExecuteTransaction));
                thread.Name = i.ToString();
                thread.Start();
            }
        }

        public void ExecuteTransaction ()
        {
            while (true)
            {
                Console.WriteLine(TransactionQueue.Count);
                lock (LockTransaction)
                {
                    if (TransactionQueue.Count > 0)
                    {
                        var transaction = TransactionQueue.Dequeue();
                        IAccount<TAccountKey , TTransactionKey> sender = Clients.Select(a => a.GetAccount(transaction.SourceAccountNUmber)).FirstOrDefault();
                        IAccount<TAccountKey, TTransactionKey> receiver = Clients.Select(a => a.GetAccount(transaction.TargetAccountNumber)).FirstOrDefault();
                        if (sender.Equals(receiver))
                        {
                            sender.Credit(transaction.Amount, transaction.SourceAccountNUmber, transaction.TransactionNumber);
                        }
                        else
                        {
                            try
                            {
                                sender.Debit(transaction.Amount, transaction.TransactionNumber, transaction.TargetAccountNumber);
                                receiver.Credit(transaction.Amount, transaction.SourceAccountNUmber, transaction.TransactionNumber);
                            }
                            catch (Exception e)
                            {

                                Console.WriteLine(e.Message);

                            }
                        }
                        Console.WriteLine($"user receiver {receiver.Balance}");
                        Console.WriteLine($"//user sender {sender.Balance}");
                        Console.WriteLine("dequeue-------- thread name : " + Thread.CurrentThread.Name + " " + Thread.CurrentThread.ThreadState);
                        Thread.Sleep(3000);

                    }
                }
                Console.WriteLine(Thread.CurrentThread.Name + " etc " + Thread.CurrentThread.ThreadState);

                Thread.Sleep(1000);

            }
        }

        public void AddTranstion(Transaction<TTransactionKey,TAccountKey> transaction)
        {
            TransactionQueue.Enqueue(transaction);
            Console.WriteLine("yooooooooooooooooooooooooooooooooooooooooooooooo");
           
        }

        public IEnumerable<IAccount<TAccountKey, TTransactionKey>> GetAllAccounts()
        {
            foreach (var client in Clients)
            {
                foreach (var account in client.GetAllAccounts())
                {
                    yield return account;
                }
            }
        }



        public void RemoveAgent(int number = 1)
        {
            for (int i = 0; i < number; i++)
            {
                //Thread.CurrentThread.Abort();
                Console.WriteLine("join ++++++++++ " + Thread.CurrentThread.Name);
            }
        }

        public IBank<TClientKey, TAccountKey, TTransactionKey> LoadFile(string path)
        {
            throw new NotImplementedException();
        }


        public void SaveFile(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction<TTransactionKey , TAccountKey>> GetAllTransations()
        {
            foreach (var client in Clients)
            {
                foreach (var account in client.GetAllAccounts())
                {
                    foreach (var transaction in account.GetAllTransactions())
                    {
                        yield return transaction;
                    }
                }
            }
        }
    }
}
