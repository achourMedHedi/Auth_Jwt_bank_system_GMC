using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Account
{
    public class Saving : AbstractAccount<string,  string, string>
    {
        public Saving(string ownerKey, string accountNumber) : base(ownerKey, accountNumber)
        {
            TaxRatio = 0.0001;
        }
        public override void Debit(double amount, string transactionNumber, string targetKey)
        {
            double calculateResult = (TaxRatio * Balance) + amount;

            base.Debit(calculateResult, transactionNumber, targetKey);

            //double calculateResult = ((Balance * (TaxRatio * amount))) + amount;
        }
    }
}
