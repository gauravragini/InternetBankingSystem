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
    public class DLInterestCalculation
    {
        public void d_CalculateInterest(List<Account> accountlist,string adminid)
        {
            double fixedinterstrate = 6;
            double savinginterestrate = 8;

            foreach (Account a in accountlist)
            {
                if (a.AccountType == "F")
                {
                    double interestamount = a.InterestAmount + (fixedinterstrate / 100) * a.AccountBalance;
                    using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog = IBS; Integrated Security = True"))
                    {
                        c.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = c;
                        cmd.CommandText = "update Accounts set InterestAmount=@intamt where AccountNumber = @accno";
                        cmd.Parameters.AddWithValue("@intamt", interestamount);
                        cmd.Parameters.AddWithValue("@accno", a.AccountNumber);
                        cmd.ExecuteNonQuery();
                        c.Close();
                    }

                }
                else
                {
                    double interestamount = a.InterestAmount + (savinginterestrate / 100) * a.AccountBalance;
                    using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog = IBS; Integrated Security = True"))
                    {
                        c.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = c;
                        cmd.CommandText = "update Accounts set InterestAmount=@intamt where AccountNumber = @accno";
                        cmd.Parameters.AddWithValue("@intamt", interestamount);
                        cmd.Parameters.AddWithValue("@accno", a.AccountNumber);
                        cmd.ExecuteNonQuery();
                        c.Close();
                    }

                }

            }

            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog = IBS; Integrated Security = True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "insert into  Adminwork(AdminID,WorkTime,WorkType) values(@aid,GETDATE(),'InterestCalculation')";
                cmd.Parameters.AddWithValue("@aid", adminid); ///admin id
                cmd.ExecuteNonQuery();
                c.Close();
            }
        }

        public DateTime d_lastInterestCalculation()
        {
            DateTime date = new DateTime(1000, 01, 01, 12, 00, 00);

            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog = IBS; Integrated Security = True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "select WorkTime from Adminwork where workType = 'InterestCalculation'";
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    date = rd.GetDateTime(0);
                }

                c.Close();
            }
            return date;
        }
        public double d_ViewInterest(string accountno)
        {
            double amt;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "select InterestAmount from Accounts where AccountNumber = @accno";
                cmd.Parameters.AddWithValue("@accno", accountno);
                SqlDataReader rd = cmd.ExecuteReader();
                rd.Read();
                amt = rd.GetSqlMoney(0).ToDouble();
                c.Close();
            }

            return amt;
        }
        public string d_WithdrawInterest(double interest, string accountno)
        {
            string bal;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("WithdrawInterest", c);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accno", accountno);
                cmd.Parameters.AddWithValue("@interest", interest);
                cmd.ExecuteNonQuery();
                bal = Convert.ToString(cmd.Parameters["@balance"].Value);
                c.Close();
            }
            return bal;
        }
        public string d_AddInterest(double interest, string accountno)
        {
            string bal;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("AddInterest", c);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accno", accountno);
                cmd.Parameters.AddWithValue("@interest", interest);
                cmd.Parameters.Add("@balance", SqlDbType.Money);
                cmd.Parameters["@balance"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                bal = Convert.ToString(cmd.Parameters["@balance"].Value);
                c.Close();
            }
            return bal;
        }
    }
}
