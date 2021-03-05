using System;
using System.Collections.Generic;
using System.Text;

namespace IBS.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public DateTimeOffset TransactionTime { get; set; }
        public string TransactionFrom { get; set; }
        public string TransactionTo { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public string AccountNumber { get; set; }

        public Transaction()
        {

        }
        //public Transaction(int id, DateTimeOffset date, string transacfrom, string transacto, double amount, string status, string accno)
        //{
        //    this.TransactionID = id;
        //    this.TransactionTime = date;
        //    this.TransactionFrom = transacfrom;
        //    this.TransactionTo = transacto;
        //    this.Amount = amount;
        //    this.Status = status;
        //    this.AccountNumber = accno;
        //}


    }
}
