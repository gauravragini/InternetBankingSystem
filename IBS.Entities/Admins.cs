using System;
using System.Collections.Generic;
using System.Text;

namespace IBS.Entities
{
    public class Admins
    {
        public int AdminID { get; set; }
        public string AdminUserID { get; set; }
        public string AdminName { get; set; }
        public long MobileNumber { get; set; }
        public string EmailAddress { get; set; }

        public Admins()
        {

        }

        public Admins(int id,string adminuserid,string adminname,long mobilenumber,string emailaddress)
        {
            this.AdminID = id;
            this.AdminUserID = adminuserid;
            this.AdminName = AdminName;
            this.MobileNumber = mobilenumber;
            this.EmailAddress = emailaddress;
        }
        public Admins( string adminname, long mobilenumber, string emailaddress)
        {
            
            this.AdminName = adminname;
            this.MobileNumber = mobilenumber;
            this.EmailAddress = emailaddress;
        }
    }
}
