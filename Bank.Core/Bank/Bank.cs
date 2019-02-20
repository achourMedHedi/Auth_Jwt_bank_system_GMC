using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bank.Core.Bank
{
    class Bank : IBank
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SwiftCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList Clients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Thread[] Agents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddAgent(int number)
        {
            throw new NotImplementedException();
        }

        public void AddTranstion()
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable GetAllTransations()
        {
            throw new NotImplementedException();
        }

        public IBank LoadFile(string path)
        {
            throw new NotImplementedException();
        }

        public void RemoveAgent(int number)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
