using System;

namespace IBS.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public int Pincode { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public long MobileNumber { get; set; }
        public string EmailAddress { get; set; }

        public User()
        {

        }

        public User(string name, string address, int pin, DateTime dob, string gender, string fname, string mname, long mob, string email)
        {
            this.UserName = name;
            this.UserAddress = address;
            this.Pincode = pin;
            this.Dob = dob;
            this.Gender = gender;
            this.FathersName = fname;
            this.MothersName = mname;
            this.MobileNumber = mob;
            this.EmailAddress = email;
        }
        public User(int uid, string name, string address, DateTime dob, string gender, string fname, string mname, int pin, long mob, string email)
        {
            this.UserId = uid;
            this.UserName = name;
            this.UserAddress = address;
            this.Pincode = pin;
            this.Dob = dob;
            this.Gender = gender;
            this.FathersName = fname;
            this.MothersName = mname;
            this.MobileNumber = mob;
            this.EmailAddress = email;
        }
    }

}
