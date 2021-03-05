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
                        Account a = new Account();
                        a.AccountNumber = rd.GetString(0);
                        a.AccountType = rd.GetString(1);
                        a.AccountBalance = rd.GetSqlMoney(2).ToDouble();
                        a.InterestAmount = rd.GetSqlMoney(3).ToDouble();
                        a.AccountCreationTime = rd.GetDateTimeOffset(4);
                        accountlist.Add(a);
                    }
                }
                c.Close();
            }

            return accountlist;
        }
    }
}
