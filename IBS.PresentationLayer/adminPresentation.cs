using System;
using System.Configuration;
using System.Diagnostics;
using IBS.BussinessLayer;
using IBS.Entities;
using IBS.Exceptions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IBS.PresentationLayer
{
    public class adminPresentation
    {
        BLAccountCreation ba;
        BLInterestCalculation bi;
        BLReports br;
        public adminPresentation(BLAccountCreation ba, BLInterestCalculation bi, BLReports br)
        {
            this.ba = ba;
            this.bi = bi;
            this.br = br;
        }
        //registering admin
        public  void adminregistration()
        {
        namelabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tEnter FullName: ");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Black;
            string name = Console.ReadLine();

            try
            {
                Regex rname = new Regex("^[a-zA-Z\\s]+$");
                if (!(rname.IsMatch(name)))
                    throw new DataEntryException("Please Enter Valid Name(Special Characters and numbers not allowed)");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; 
                Console.WriteLine(e.Message);
                goto namelabel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Beep();
                goto namelabel;
            }
        moblabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tMobile Number: (923XXXXXXX-10 digits) ");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            Console.ForegroundColor = ConsoleColor.Black;

            string mobx = Console.ReadLine();
            try
            {
                Regex rmob = new Regex("^[0-9]{10}$");
                if (!(rmob.IsMatch(mobx.ToString())))
                    throw new DataEntryException("Please Enter Valid Mobile number(10 digit))");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto moblabel;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto moblabel;
            }
            long mob = long.Parse(mobx);

        //input email address
        emaillabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("\n\tEmail Address: (abcd@xyz.ijk)");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Black;

            string email = Console.ReadLine();
            try
            {
                Regex remail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (!(remail.IsMatch(email)))
                    throw new DataEntryException("Please Enter Valid email address");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto emaillabel;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto emaillabel;
            }

            Admins registeradmin = new Admins(name, mob, email);
            string res = ba.b_adminRegistration(registeradmin);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(res+"\n\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\tUse the credentials to login to the system\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\tPress any key to go back");
            Console.ReadKey();
        }


        //admin tasks page
        public void adminMenu(string adminid)
        {
            
                bool flag = true;

                while (flag)
                {
                try
                {
                    Console.Clear();
                    heading("IBS Admin");
                    Console.WriteLine("\n\n\n\t\t\t\t\t\t\tAdmin\n");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("\n\t\t\t\t\t 1. View all new Registered Users\n\t\t\t\t\t 2. View All Account Details\n\t\t\t\t\t 3. View All transactions\n\t\t\t\t\t 4. Calculate interest\n\t\t\t\t\t 5. ");


                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\n\t\t\t\t\t\tEnter Choice");
                    Console.SetCursorPosition(Console.CursorLeft + 57, Console.CursorTop);
                    int choice = int.Parse(Console.ReadLine());

                    Console.WriteLine("\n\n");
                    switch (choice)
                    {
                        case 1:
                            List<User> userlist = ba.b_newRegistrations();
                            if (userlist.Count > 0)
                            {
                                display_newRegistrations(userlist);
                                new_users_action(ba);
                            }
                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 2:
                            try
                            {
                                List<Account> acclist = new List<Account>();
                                acclist = br.b_AccountDetails();
                                display_accountdetails(acclist);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 3:
                            List<Transaction> transaclist = br.b_transactionDetails();
                            display_transactiondetails(transaclist);
                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 4:
                            heading("IBS Admin");
                            List<Account> accountlist = new List<Account>();
                            try
                            {
                                accountlist = br.b_AccountDetails();
                                display_accountdetails(accountlist);
                                bi.b_CalculateInterest(accountlist, adminid);
                                Console.WriteLine("\n\n\t Account details after Calculate Interest\n\tInterest rate is 6% for Fixed Account and 8% for Saving Account\n");
                                accountlist = br.b_AccountDetails();
                                display_accountdetails(accountlist);
                            }
                            catch (InterestException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 5:
                            
                            flag = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                }
            }
           
           
        }

        public void display_newRegistrations(List<User> userlist)
        {

            foreach (User user in userlist)
            {

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("\n\tUser Id                   : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.UserId); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tUser Name                 : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.UserName); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tAddress                   : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.UserAddress); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tDob                       : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.Dob); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tGender                    : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.Gender); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tFathers Name              : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.FathersName); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tMothers Name              : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.MothersName); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tPin                       : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.Pincode); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tMobile                    : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.MobileNumber); Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.Write("\n\tEmail                     : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(user.EmailAddress + "\n"); Console.ForegroundColor = ConsoleColor.Black; ;

                List<Nominee> nomineelist = ba.b_nominees(user.UserId);
                display_nominee(nomineelist);

                Console.WriteLine("----------------------------------------");
            }
        }
        public void display_nominee(List<Nominee> nomineelist)
        {
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            foreach (Nominee n in nomineelist)
            {
                Console.Write("\n\tNominee Name              : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(n.NomineeName + "\n"); Console.ForegroundColor = ConsoleColor.DarkCyan;

                Console.Write("\tNominee Relation          : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(n.NomineeRelation + "\n"); Console.ForegroundColor = ConsoleColor.DarkCyan;

                Console.Write("\tAge                       : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(n.NomineeAge + "\n"); Console.ForegroundColor = ConsoleColor.DarkCyan;

                Console.Write("\tGender                    : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(n.NomineeGender + "\n"); Console.ForegroundColor = ConsoleColor.DarkCyan;

                Console.Write("\tMobile Number             : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(n.NomineeMobileNumber + "\n"); Console.ForegroundColor = ConsoleColor.DarkCyan;

                Console.Write("\tAddress                   : "); Console.ForegroundColor = ConsoleColor.Black; ; Console.Write(n.NomineeAddress + "\n"); Console.ForegroundColor = ConsoleColor.Black; ;
            }

        }

        public void new_users_action(BLAccountCreation ba)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\t\t1.Accept By User Id \n\t\t2.Reject By user Id \n\t\t3.Accept All \n\t\t4.Reject All\n");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\tEnter your choice");
            Console.SetCursorPosition(Console.CursorLeft + 12, Console.CursorTop);

            int action = int.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    Console.WriteLine("\n\tEnter the user id to approve account: ");
                    Console.SetCursorPosition(Console.CursorLeft + 22, Console.CursorTop);
                    string acceptuid = Console.ReadLine();
                    ba.b_approveAccount(acceptuid);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\tAccepted Customer Registration of UserId : " + acceptuid+"\n");
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case 2:
                    Console.WriteLine("\n\tEnter the user id to reject account: ");
                    string rejectuid = Console.ReadLine();
                    ba.b_approveAccount(rejectuid);
                    Console.WriteLine("\tRejected Customer Registration of UserId : "+rejectuid);

                    break;
                case 3:
                    ba.b_approveAll();
                    Console.WriteLine("\tApproved All the new customers registrations");
                    break;
                case 4:
                    ba.b_rejectAll();
                    Console.WriteLine("\tRejected All the new customer registrations");
                    break;
                default:
                    break;
            }
        }

        public void display_transactiondetails(List<Transaction> transaclist)
        {
            if (transaclist.Count > 0)
            {
                Console.WriteLine("\n\n\n\t\t\t\t\t\t\t ADMIN\t");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n\t\t\t\t\t\tAll Transaction details Till Date\n");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\n\t\t Transaction_ID   Transaction_From    Transaction_To    Amount     Action     AccountHolderID");
                //  Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\t\t --------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (Transaction t in transaclist)
                {
                    if (t.TransactionTo == "self")
                        t.TransactionTo = t.TransactionTo + "    ";
                    if (t.TransactionFrom == "self")
                        t.TransactionFrom = t.TransactionFrom + "    ";
                    Console.WriteLine("\t\t\t" + t.TransactionID + "\t\t" + t.TransactionFrom + "\t" + t.TransactionTo + " \t " + t.Amount + " \t " + t.Status + " \t " + t.AccountNumber);
                }
            }
            else
            {
                Console.WriteLine("\n No Transactions yet to display");
            }
            Console.ReadKey();
        }

        public void display_accountdetails(List<Account> accountlist)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("   \n\n\n\n\tAccountNumber\tAccountType\tAvailable Balance\tInterestAmount\tAccountCreationTime");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t--------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Black;

            foreach (Account a in accountlist)
            {
                Console.WriteLine("   \t" + a.AccountNumber + "      \t" + a.AccountType + "  \t\t" + a.AccountBalance + "     \t\t\t" + a.InterestAmount + "\t" + a.AccountCreationTime);

            }
        }

        public static void heading(string str)
        {

            Console.Title = str;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\t\t\t------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\t\t\t\t\tInternet Banking Solutions ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t\t------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Black;

            DateTime d = DateTime.Now;

            Console.Write("\t\t\t" + d.ToString("d"));
            Console.Write("\t\t\t" + d.ToString("dddd"));
            Console.Write("\t\t\t" + d.ToString("t"));

        }
    }
}
