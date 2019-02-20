using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Account
{
    public class Business : AbstractAccount<string, string, string>
    {
        public Business(string ownerKey, string accountNumber) : base(ownerKey, accountNumber)
        {
            TaxRatio = 0.1;
        }
        public override void Debit(double amount, string transactionNumber, string targetKey)
        {
            double calculateResult = amount + (amount * TaxRatio);

            base.Debit(calculateResult, transactionNumber, targetKey);

        }
    }
}
