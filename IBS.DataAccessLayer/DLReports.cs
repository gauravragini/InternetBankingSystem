using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Generic;
using IBS.Entities;
using IBS.Exceptions;

namespace IBS.DataAccessLayer
{
    public class DLReports
    {
        public List<Transaction> d_transactionDetails()
        {
            List<Transaction> transactionlist = new List<Transaction>();

            using(SqlConnection con = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = "select * from Transactions";
                SqlDataReader rd = cmd1.ExecuteReader();
                bool flag = rd.HasRows;
                if (flag)
                {
                    while (rd.Read())
                    {
                        Transaction t = new Transaction();
                        t.TransactionID = rd.GetInt32(0);
                        t.TransactionTime = rd.GetDateTimeOffset(1);
                        t.TransactionFrom = rd.GetString(2);
                        t.TransactionTo = rd.GetString(3);
                        t.Amount = rd.GetSqlMoney(4).ToDouble();
                        t.Status = rd.GetString(5);
                        t.AccountNumber = rd.GetString(6);
                        transactionlist.Add(t);
                    }
                }
                con.Close();
            }
            

            return transactionlist;
        }

        public List<Account> d_AccountDetails()
        {
            List<Account> accountlist = new List<Account>();
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = c;
                cmd1.CommandText = "select * from Accounts";
                SqlDataReader rd = cmd1.ExecuteReader();
                bool flag = rd.HasRows;
                if (flag)
                {
                    while (rd.Read())
                    {
                        string accno = rd.GetString(0);
                        string atype = rd.GetString(1);
                        double bal = rd.GetSqlMoney(2).ToDouble();
                        double intrestamt = rd.GetSqlMoney(3).ToDouble();
                        DateTimeOffset time = rd.GetDateTimeOffset(4);
                        accountlist.Add(new Account(accno, atype, bal, intrestamt, time));
                    }
                }
                c.Close();
            }

            return accountlist;
        }
    }
}
