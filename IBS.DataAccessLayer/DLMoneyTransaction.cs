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
    public class DLMoneyTransaction
    {
        public string d_deposit(double damount, string accountno)
        {
            string bal;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("DepositMoney", c);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accno", accountno);
                cmd.Parameters.AddWithValue("@damount", damount);
                cmd.Parameters.Add("@balance", SqlDbType.Money);
                cmd.Parameters["@balance"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                bal = Convert.ToString(cmd.Parameters["@balance"].Value);           
                c.Close();
            }
            return bal;
        }
        public string d_withdraw(double wamount, string accountno)
        {
            string bal;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("WithdrawMoney", c);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accno", accountno);
                cmd.Parameters.AddWithValue("@wamount", wamount);
                cmd.Parameters.Add("@balance", SqlDbType.Money);
                cmd.Parameters["@balance"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                bal= Convert.ToString(cmd.Parameters["@balance"].Value);
                c.Close();
            }
            return bal;
        }
        public string d_transfer(double tamount, string toaccount, string accountno)
        {
            string bal;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("TransferMoney", c);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tamount", tamount);
                cmd.Parameters.AddWithValue("@toaccno", toaccount);
                cmd.Parameters.AddWithValue("@accno", accountno);
                cmd.Parameters.Add("@balance", SqlDbType.Money);
                cmd.Parameters["@balance"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                bal = Convert.ToString(cmd.Parameters["@balance"].Value);             
                c.Close();
            }
            return bal;
        }
        public void d_updatePassword(string newpassword, string accountno)
        {
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "update Credentials set Password=@pass where ID=@accno";
                cmd.Parameters.AddWithValue("@pass", newpassword);
                cmd.Parameters.AddWithValue("@accno", accountno);
                cmd.ExecuteNonQuery();
                c.Close();
            }
        }
        public double d_availablebalance(string accountno)
        {
            double availbal;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = c;
                cmd1.CommandText = "select AvailableBalance from Accounts where AccountNumber = @accno";
                cmd1.Parameters.AddWithValue("@accno", accountno);
                SqlDataReader rd = cmd1.ExecuteReader();
                rd.Read();
                availbal = rd.GetSqlMoney(0).ToDouble();
                c.Close();
            }
            return availbal;
        }
        public bool d_accountexists(string accountno)
        {
            bool flag;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = c;
                cmd1.CommandText = "select * from Accounts where AccountNumber = @accno";
                cmd1.Parameters.AddWithValue("@accno", accountno);
                SqlDataReader rd = cmd1.ExecuteReader();
                flag = rd.HasRows;
                c.Close();
            }
            return flag;
        }
    }
}
