using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Generic;
using IBS.Entities;
using IBS.Exceptions;
using System.Collections;

namespace IBS.DataAccessLayer
{
    public class DLAccountCreation
    {

        //Creating account for Customers
        public string d_createAccount(User registeruser, List<Nominee> nomineelist, string atype)
        {

            //inserting data into tables

            //inserting data in User table with status = applied
            string uid;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("AddUser", c);
                cmd.CommandType = CommandType.StoredProcedure;
                string status = "applied";
                cmd.Parameters.AddWithValue("@uname", registeruser.UserName);
                cmd.Parameters.AddWithValue("@uadd", registeruser.UserAddress);
                cmd.Parameters.AddWithValue("@dob", registeruser.Dob);
                cmd.Parameters.AddWithValue("@gender", registeruser.Gender);
                cmd.Parameters.AddWithValue("@fname", registeruser.FathersName);
                cmd.Parameters.AddWithValue("@mname", registeruser.MothersName);
                cmd.Parameters.AddWithValue("@pin", registeruser.Pincode);
                cmd.Parameters.AddWithValue("@mob", registeruser.MobileNumber);
                cmd.Parameters.AddWithValue("@email", registeruser.EmailAddress);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.Add("@uid", SqlDbType.Int);
                cmd.Parameters["@uid"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                uid = Convert.ToString(cmd.Parameters["@uid"].Value);
                c.Close();
            }
                

            //inserting data in Nominees table
            foreach (Nominee n in nomineelist)
            {
                using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
                {
                    c.Open();
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = c;
                    cmd2.CommandText = "insert into Nominees values(@nname,@nrel,@nage,@ngen,@nmob,@nadd,@nuid)";
                    cmd2.Parameters.AddWithValue("@nname", n.NomineeName);
                    cmd2.Parameters.AddWithValue("@nrel", n.NomineeRelation);
                    cmd2.Parameters.AddWithValue("@nage", n.NomineeAge);
                    cmd2.Parameters.AddWithValue("@ngen", n.NomineeGender);
                    cmd2.Parameters.AddWithValue("@nmob", n.NomineeMobileNumber);
                    cmd2.Parameters.AddWithValue("@nadd", n.NomineeAddress);
                    cmd2.Parameters.AddWithValue("@nuid", uid);
                    cmd2.ExecuteNonQuery();
                    c.Close();
                }
               
            }

            //inserting data in Accounts Table
            //generating Accno and passwrd
            string accno = "IBS0000" + uid;
           // string pass = "IBS" + registeruser.UserName.Substring(0, 3).ToUpper() + "@X" + uid;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = c;
                cmd3.CommandText = "insert into Accounts(AccountNumber,AccountType,AccountCreationTime,UserID) values(@accno,@atype,GETDATE(),@auid)";
                cmd3.Parameters.AddWithValue("@accno", accno);
                cmd3.Parameters.AddWithValue("@atype", atype);
                cmd3.Parameters.AddWithValue("@auid", uid);
                cmd3.ExecuteNonQuery();
                c.Close();
            }
            return uid;

        }

        //returing current status of Customer Application
        public string d_checkStatus(string uid)
        {
            string currstatus;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = c;
                cmd1.CommandText = "select Status from Customers where UserId = @suid";
                cmd1.Parameters.AddWithValue("@suid", uid);
                SqlDataReader rd = cmd1.ExecuteReader();
                bool flag = rd.HasRows;


                if (flag)
                {
                    rd.Read();
                    currstatus = rd.GetString(0);
                    c.Close();
                    if (currstatus == "approved")
                    {
                        c.Open();
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Connection = c;
                        cmd3.CommandText = "update Customers set Status='created' where USERID=@uuid";
                        cmd3.Parameters.AddWithValue("@uuid", uid);
                        cmd3.ExecuteNonQuery();
                        c.Close();
                    }
                    return currstatus;
                }
                else
                {
                    c.Close();
                    throw new NoAccountException("\nUser Id Does not Exist\nPlease Register to Create Bank Account");
                }
            }
        }

        //getting accno nd passwrd of Customer
        public string d_getaccNumberPassword(string uid)
        {
            string data;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = c;
                cmd2.CommandText = "select AccountNumber from Accounts where UserId = @cuid";
                cmd2.Parameters.AddWithValue("@cuid", uid);
                SqlDataReader rd2 = cmd2.ExecuteReader();
                if (rd2.HasRows)
                {
                    rd2.Read();
                    data = "\nYour Account Number: " + rd2.GetString(0) + "\nYour Account Password: IBSPASS@X" + uid + "\nUse the above Account number and Password to login into Your Account and make Transactions";
                    c.Close();
                }
                else
                {
                    data = "No account exists";
                }

            }
            return data;
        }





        //Creating admin accounts
        public string d_adminRegistration(Admins registeradmin)
        {
            string uid;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand("AddAdmin", c);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uname", registeradmin.AdminName);
                cmd.Parameters.AddWithValue("@mob", registeradmin.MobileNumber);
                cmd.Parameters.AddWithValue("@email", registeradmin.EmailAddress);
                cmd.Parameters.Add("@uid", SqlDbType.Int);
                cmd.Parameters["@uid"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                uid = Convert.ToString(cmd.Parameters["@uid"].Value);
                c.Close();
            }
            //generating user id and passwrd for admin 
            string auserid = "ADMIN0000" + uid;
            string pass = "AIBS" + registeradmin.AdminName.Substring(0, 3).ToUpper() + "@X" + uid;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = c;
                cmd3.CommandText = "update Admins set AdminUserID = @auserid where AdminID = @uid";
                cmd3.Parameters.AddWithValue("@uid", uid);
                cmd3.Parameters.AddWithValue("@auserid", auserid);
                cmd3.ExecuteNonQuery();
                c.Close();
            }
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = c;
                cmd3.CommandText = "insert into Credentials   values(@auserid,@pass,'admin')";
                cmd3.Parameters.AddWithValue("@auserid", auserid);
                cmd3.Parameters.AddWithValue("@pass", pass);
                cmd3.ExecuteNonQuery();
                c.Close();
            }

            return "\n\n\tUserID   : " + auserid +  "\n\tPassword : " + pass;
        }
        
        //retrieving list of all new registrations
        public List<User> d_newRegistrations()
        {
            List<User> userlist = new List<User>();
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = c;
                cmd1.CommandText = "select * from Customers where status = 'applied'";
                SqlDataReader rd = cmd1.ExecuteReader();
                bool flag = rd.HasRows;

                if (flag)
                {                  
                    while (rd.Read())
                    {
                        User u = new User();
                        u.UserId = rd.GetInt32(0);
                        u.UserName = rd.GetString(1);
                        u.UserAddress = rd.GetString(2);
                        u.Dob = rd.GetDateTime(3);
                        u.Gender = rd.GetString(4);
                        u.FathersName = rd.GetString(5);
                        u.MothersName = rd.GetString(6);
                        u.Pincode = rd.GetInt32(7);
                        u.MobileNumber = rd.GetInt64(8);
                        u.EmailAddress = rd.GetString(9);
                        userlist.Add(u);
                    }
                    c.Close();
                }
             
                else
                {
                    c.Close();
                    throw new NoAccountException("\t\t\t\t\t\t  No new Registrations.");
                }
            }
            return userlist;
        }
        public List<Nominee> d_nominees(int userid)
        {
            List<Nominee> nomineelist = new List<Nominee>();

            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = c;
                cmd2.CommandText = "select * from Nominees where UserID = @uid";
                cmd2.Parameters.AddWithValue("@uid", userid);
                SqlDataReader rd2 = cmd2.ExecuteReader();
                while (rd2.Read())
                {
                    Nominee n = new Nominee();
                    n.NomineeName = rd2.GetString(1);
                    n.NomineeRelation = rd2.GetString(2);
                    n.NomineeAge = rd2.GetInt32(3);
                    n.NomineeGender = rd2.GetString(4);
                    n.NomineeMobileNumber = rd2.GetInt64(5);
                    n.NomineeAddress = rd2.GetString(6);
                    nomineelist.Add(n);
                }
                c.Close();
            }
            return nomineelist;
        }

        //approving/rejecting new customers
        public void  d_approveAccount(string acceptuid)
        {
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "update Customers set Status='approved' where USERID=@uuid";
                cmd.Parameters.AddWithValue("@uuid", acceptuid);
                cmd.ExecuteNonQuery();
                c.Close();
            }
            string accountno = "IBS0000" + acceptuid;
            string pass = "IBSPASS@X" + acceptuid;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "insert into Credentials values(@userid,@pass,'customer')";
                cmd.Parameters.AddWithValue("@userid", accountno);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.ExecuteNonQuery();
                c.Close();
            }
        }
        public void d_rejectAccount(string rejectuid)
        {
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "delete from Customers where USERID=@uuid and status = 'applied'";
                cmd.Parameters.AddWithValue("@uuid", rejectuid);
                cmd.ExecuteNonQuery();
                c.Close();
            }
 
        }
        public void d_approveAll()
        {
            ArrayList a = new ArrayList();
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = c;
                cmd2.CommandText = "select UserID from Customers where Status ='applied'";
                SqlDataReader rd2 = cmd2.ExecuteReader();
                if (rd2.HasRows)
                {
                    while (rd2.Read())
                    {
                        a.Add(rd2.GetInt32(0));
                    }               
                    c.Close();
                }
                else
                {
                    c.Close();
                }
            }
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "Update Customers set Status='approved' where Status='applied'";
                cmd.ExecuteNonQuery();
                c.Close();
            }
            foreach(int  i in a)
            {
                string accountno = "IBS0000" + i;
                string pass = "IBSPASS@X" + i;
                using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
                {
                    c.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = c;
                    cmd.CommandText = "insert into Credentials values(@userid,@pass,'customer')";
                    cmd.Parameters.AddWithValue("@userid", accountno);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.ExecuteNonQuery();
                    c.Close();
                }
            }

        }
        public void d_rejectAll()
        {
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandText = "delete from Customers where status = 'applied'";
                cmd.ExecuteNonQuery();
                c.Close();
            }

        }





        //check if account number and passwrd exits and is correct 
        public bool d_Login(string accountno, string password)
        {
            bool flag;
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = c;
                cmd1.CommandText = "select * from Credentials where ID = @accno and Password=@pass";
                cmd1.Parameters.AddWithValue("@accno", accountno);
                cmd1.Parameters.AddWithValue("@pass", password);
                SqlDataReader rd = cmd1.ExecuteReader();
                flag = rd.HasRows;
                c.Close();
            }            
            return flag;
        }

        //check whether the user is customer or admin
        public string d_checkRole(string userid, string pass)
        {
            using (SqlConnection c = new SqlConnection("Data Source=DESKTOPRAGINI;Initial Catalog=IBS;Integrated Security=True"))
            {
                c.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = c;
                cmd2.CommandText = "select Role from Credentials where ID=@userid and Password = @pass";
                cmd2.Parameters.AddWithValue("@userid", userid);
                cmd2.Parameters.AddWithValue("@pass", pass);

                SqlDataReader rd = cmd2.ExecuteReader();
                rd.Read();
                return rd.GetString(0);

            }
        }
    }
}
