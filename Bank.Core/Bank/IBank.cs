using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bank.Core.Bank
{
    interface IBank
    {
        string Name { get; set; }
        string SwiftCode { get; set; }
        IList Clients { get; set; }
        Thread[] Agents { get; set; }
        IEnumerable GetAllTransations();
        IEnumerable GetAllAccounts();
        void AddTranstion();
        void AddAgent(int number);
        void RemoveAgent(int number);
        void SaveFile(string path);
        IBank LoadFile(string path);
    }
}
