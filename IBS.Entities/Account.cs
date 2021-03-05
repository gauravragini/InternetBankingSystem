using System;
using System.Collections.Generic;
using System.Text;

namespace IBS.Entities
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string Password { get; set; }
        public string AccountType { get; set; }
        public double AccountBalance { get; set; }
        public double InterestAmount { get; set; }
        public DateTimeOffset AccountCreationTime { get; set; }

        public Account()
        {

        }
        public Account(string accno, string pass, string atype)
        {
            this.AccountNumber = accno;
            this.Password = pass;
            this.AccountType = atype;
        }
        public Account(string accno, string atype, double bal, double interestamt, DateTimeOffset time)
        {
            this.AccountNumber = accno;
            this.AccountType = atype;
            this.AccountBalance = bal;
            this.InterestAmount = interestamt;
            this.AccountCreationTime = time;
        }

    }
}
