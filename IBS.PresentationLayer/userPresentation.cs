using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using IBS.BussinessLayer;
using IBS.Entities;
using IBS.Exceptions;


namespace IBS.PresentationLayer
{
    public class userPresentation
    {
        BLAccountCreation ba;
        BLMoneyTransaction bmt;
        BLInterestCalculation bi; 
        public userPresentation(BLAccountCreation ba, BLMoneyTransaction bmt, BLInterestCalculation bi)
        {
            this.ba = ba;
            this.bmt = bmt;
            this.bi = bi;
        }

        public void userRegistration()
        {
            //taking input of user's personal details 
            User registeruser = userinput();

            //Taking input of nominee details
            List<Nominee> nomineelist = nomineeinput();

            //Taking input of bank details
            string atype = atypeinput();

            //Passing user details to bussiness layer
            string uid;
            try
            {
                uid = ba.b_createAccount(registeruser, nomineelist, atype);
                //Waiting for admin to approve or disapprove
                Console.WriteLine("\nApplied for registering the bank account");
                Console.WriteLine("Please wait for approval from bank administrator....");
                System.Threading.Thread.Sleep(3000);

                string currstatus = ba.b_checkStatus(uid);
                displayStatus(currstatus, uid);
                Console.WriteLine("\npress any key to go back");
                Console.ReadKey();
            }
            catch (DataValidationException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine(e.Message);
                Console.Beep();
                Console.WriteLine("Press any key to go back...");
                Console.ReadKey();
            }
            catch (NoAccountException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine(e.Message);
                Console.Beep();
                Console.WriteLine("Press any key to go back...");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Console.Beep();
            }
            

        }
        public  User userinput()
        {
        //input user name
        namelabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tEnter FullName: ");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Black;
            string name = Console.ReadLine();

            try
            {
                Regex rname = new Regex("^[a-zA-Z ]+$");
                if (!(rname.IsMatch(name)))
                    throw new DataEntryException("Please Enter Valid Name(Special Characters and numbers not allowed)");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(e.Message);
                goto namelabel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Beep();
                goto namelabel;
            }

        //input user address
        addresslabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tAddress: (address should include house no,name,street no,area)");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
            string add = Console.ReadLine();

            try
            {
                if (add.Length < 15)
                    throw new DataEntryException("Please Enter Detailed Address");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; 
                Console.WriteLine(e.Message);
                goto addresslabel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Beep();
                goto addresslabel;
            }


        //input pin
        pinlabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tPincode: (xxxxxx- 6 Digits)");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            Console.ForegroundColor = ConsoleColor.Black;

            string pinx = (Console.ReadLine());
            try
            {
                Regex rpin = new Regex("^[1-9]{1}[0-9]{5}$");
                if (!(rpin.IsMatch(pinx.ToString())))

                    throw new DataEntryException("Please Enter Valid Pincode(Alphabets,Characters and Space are not allowed and length of pin should be 6)");

            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;

                goto pinlabel;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;

                goto pinlabel;
            }
            int pin = int.Parse(pinx);

        //input date of birth
        doblabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tDate of Birth(DD-MM-YYYY): ) ");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            Console.ForegroundColor = ConsoleColor.Black;

            string dobx = Console.ReadLine();
            try
            {
                // Regex rdob = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
                //   if (!(rdob.IsMatch(dobx.ToString())))
                DateTime X = Convert.ToDateTime(dobx);
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto doblabel;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto doblabel;
            }
            DateTime dob = Convert.ToDateTime(dobx);

        //input Gender
        genderlabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tGender:  Enter M for Male and F for Female");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            Console.ForegroundColor = ConsoleColor.Black;

            string gender = Console.ReadLine();
            try
            {
                if (!(gender == "M" || gender == "F" || gender == "m" || gender == "f"))
                    throw new DataEntryException("Please enter M for Male or F for Female");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto genderlabel;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto genderlabel;
            }

        //input father name
        fnamelabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tFather's Name: ");
           

            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            Console.ForegroundColor = ConsoleColor.Black;

            string fname = Console.ReadLine();
            try
            {
                Regex rname = new Regex("^[a-zA-Z\\s]+$");
                if (!(rname.IsMatch(fname)))
                    throw new DataEntryException("Please Enter Valid Name(Special Characters and numbers not allowed)");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto fnamelabel;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto fnamelabel;
            }


        //input mothers name
        mnamelabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tMother's Name: ");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            Console.ForegroundColor = ConsoleColor.Black;

            string mname = Console.ReadLine();
            try
            {
                Regex rname = new Regex("^[a-zA-Z\\s]+$");
                if (!(rname.IsMatch(mname)))
                    throw new DataEntryException("Please Enter Valid Name(Special Characters and numbers not allowed)");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto mnamelabel;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto mnamelabel;
            }

        //input mobile number
        moblabel:
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\tMobile Number: (xxxxxxxxxx- only 10 digits) ");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            Console.ForegroundColor = ConsoleColor.Black;

            string mobx = Console.ReadLine();
            try
            {
                Regex rmob = new Regex("^[2-9]{1}[0-9]{9}$");
                if (!(rmob.IsMatch(mobx.ToString())))
                    throw new DataEntryException("Please Enter Valid Mobile number(10 digit) and should not start with 0 or 1)");
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

            Console.WriteLine("\n\tEmail Address: (abc@xyz.abc)");
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

            User registeruser = new User(name, add, pin, dob, gender, fname, mname, mob, email);

            return registeruser;
        }
        public  List<Nominee> nomineeinput()
        {
            List<Nominee> nomineelist = new List<Nominee>();
            Console.WriteLine("\n\tDo You want to add Nominee(Y/N)");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            string n;
        label:
            n = Console.ReadLine();
            try
            {
                if (!(n == "y" || n == "Y" || n == "N" || n == "n"))
                    throw new DataEntryException("Please enter valid option (Y/N)");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto label;
            }

            if (n == "Y" || n == "y")
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("\n\tPlease Enter Nominee details");
                Console.ForegroundColor = ConsoleColor.Black;

            //input nominee name
            namelabel:
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("\n\tNominee Name: ");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

                Console.ForegroundColor = ConsoleColor.Black;

                string nname = Console.ReadLine();
                try
                {
                    Regex rname = new Regex("^[a-zA-Z\\s]+$");
                    if (!(rname.IsMatch(nname)))
                        throw new DataEntryException("Please Enter Valid Name(Special Characters and numbers not allowed)");
                }
                catch (DataEntryException e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                    Console.WriteLine(e.Message);
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Black;
                    goto namelabel;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                    Console.WriteLine(e.Message);
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Black;
                    goto namelabel;
                }

            //input nominee relation
            relationlabel:
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("\n\tNominess's Relation: ");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

                Console.ForegroundColor = ConsoleColor.Black;

                string nrelation = Console.ReadLine();
                try
                {
                    Regex rrel = new Regex("^[a-zA-Z\\s]+$");
                    if (!(rrel.IsMatch(nrelation)))
                        throw new DataEntryException("Invalid input");
                }
                catch (DataEntryException e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                    Console.WriteLine(e.Message);
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Black;
                    goto relationlabel;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                    Console.WriteLine(e.Message);
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Black;
                    goto relationlabel;
                }

            //input age
            agelabel:
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("\n\tAge: ");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

                Console.ForegroundColor = ConsoleColor.Black;

                string nagex = Console.ReadLine();
                try
                {
                    Regex rage = new Regex("^[1-9]|[0-9]{2}$");
                    if (!(rage.IsMatch(nagex)))
                        throw new DataEntryException("Enter valid Age of the nominee");
                }
                catch (DataEntryException e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                    Console.WriteLine(e.Message);
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Black;
                    goto agelabel;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.Black;
                    goto agelabel;
                }
                int nage = int.Parse(nagex);

            //input gender
            genderlabel:
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("\n\tGender: Enter M for Male or F for Female");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

                Console.ForegroundColor = ConsoleColor.Black;

                string ngender = Console.ReadLine();
                try
                {
                    if (!(ngender == "M" || ngender == "F" || ngender == "m" || ngender == "f"))
                        throw new DataEntryException("Please enter M for Male and F for Female");
                }
                catch (DataEntryException e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                    Console.WriteLine(e.Message);
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Black;
                    goto genderlabel;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                    Console.WriteLine(e.Message);
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Black;
                    goto genderlabel;
                }


            //input mob number
            moblabel:
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                Console.WriteLine("\n\tMobile Number: (XXXXXXXXXX 10 Digit Number) ");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

                Console.ForegroundColor = ConsoleColor.Black;

                string nmobx = Console.ReadLine();
                try
                {
                    Regex rmob = new Regex("^[2-9]{1}[0-9]{9}$");
                    if (!(rmob.IsMatch(nmobx.ToString())))
                        throw new DataEntryException("Please Enter Valid Mobile number(10 digit) and it should not start with  0 or 1)");
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
                long nmob = long.Parse(nmobx);

            //input address
            naddresslabel:
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n\tAddress:  (address should include house no,name,street no,area)");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                string nadd = Console.ReadLine();

                try
                {
                    if (nadd.Length < 15)
                        throw new DataEntryException("Please Enter Detailed Address");
                }
                catch (DataEntryException e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e.Message);
                    Console.Beep();
                    goto naddresslabel;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Beep();
                    goto naddresslabel;
                }

                nomineelist.Add(new Nominee(nname, nrelation, nage, ngender, nmob, nadd));

                Console.WriteLine("\n\tDo You want to add more Nominee(Y/N)");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

                goto label;
            }

            return nomineelist;
        }
        public  string atypeinput()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine("\n\tWhat Type of account do you want to register for? (Enter S for Savings or F for Fixed ) ");
            Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);

            Console.ForegroundColor = ConsoleColor.Black;


        label:
            string atype = Console.ReadLine();
            try
            {
                if (!(atype == "S" || atype == "s" || atype == "F" || atype == "f"))
                    throw new DataEntryException("Please enter valid option (S/F)");
            }
            catch (DataEntryException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; ///Code lines ///

                Console.WriteLine(e.Message);
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Black;
                goto label;
            }

            return atype;
        }
        public void heading()
        {
            Console.Title = "IBS USer";
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
        
        //displaying status using temporary user id
        public void displayStatus(string currstatus, string uid)
        {
            if (currstatus == "approved")
            {
                Console.WriteLine("\nCongratulations ! Your Application Has been Approved !!!");
                string data = ba.b_getaccNumberPassword(uid);
                Console.WriteLine(data);
                Console.WriteLine("\nYou need to deposit a minimum amount of 1000 to open and use your account ");
                Console.WriteLine("\nPlease Login to your account and deposit money");
            }
            else if (currstatus == "rejected")
            {
                Console.WriteLine("\nSorry... Your Application Has been Rejected By Bank Administrator.\n Please Contact the Bank For Details.");
                Console.Beep();
            }
            else if (currstatus == "applied")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\n\t\t\t\t\t      Your Application is under Review\n");
                Console.ForegroundColor = ConsoleColor.Black; ;
                Console.WriteLine("\t\t\t\tUse your temporary User Id to check your registration Status");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\t\t\t\t\t          Temporary User Id : " + uid);
                Console.ForegroundColor = ConsoleColor.Black; ;
            }
            else if (currstatus == "created")
            {
                Console.WriteLine("\nYour Account Exists");
                Console.WriteLine("\nUse the Account number and Password provided to login into Your Account and make Transactions");
            }
        }


        //user actions --MONEY TRANSACTIONs
        public void usermenu(string accountno, string password)
        {

            heading();

            Console.WriteLine("\n\n\n\t\t\t\t\t\t  Login Successful!!!");
            Console.WriteLine("\t\t\t\t\t\t------------------------");

            bool flag = true;
            while (flag)
            {
                Console.Clear();
                heading();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n\n\n\t\t\t\t\t\t\tCUSTOMER");
                Console.WriteLine("\n\t\t\t\t\t\t  1. Deposit Money \n\t\t\t\t\t\t  2. WithDraw Money \n\t\t\t\t\t\t  3. Transfer Money \n\t\t\t\t\t\t  4. Interest Amount \n\t\t\t\t\t\t  5. View Balance \n\t\t\t\t\t\t  6. Update Password \n\t\t\t\t\t\t  7. LOG OUT");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n\t\t\t\t\t\t Enter Your Choice :");
                Console.SetCursorPosition(Console.CursorLeft + 60, Console.CursorTop);
                int choice = int.Parse(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("\t\t\tEnter Amount You Want to Deposit :");
                            Console.SetCursorPosition(Console.CursorLeft + 24, Console.CursorTop);

                            Console.ForegroundColor = ConsoleColor.Black;
                            double damount = double.Parse(Console.ReadLine());
                            string bal = bmt.b_deposit(damount, accountno);
                            Console.WriteLine(bal);

                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 2:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("\t\t\tEnter Amount You Want to Withdraw :");
                            Console.SetCursorPosition(Console.CursorLeft + 24, Console.CursorTop);

                            Console.ForegroundColor = ConsoleColor.Black;
                            double wamount = double.Parse(Console.ReadLine());
                            string bal2 = bmt.b_withdraw(wamount, accountno);
                            Console.WriteLine(bal2);

                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 3:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("\t\t\tEnter Account number to which you want to transfer money");
                            Console.SetCursorPosition(Console.CursorLeft + 60, Console.CursorTop);

                            string toaccount = (Console.ReadLine());
                            Console.WriteLine("\t Enter Amount You Want to Transfer");
                            Console.ForegroundColor = ConsoleColor.Black;
                            double tamount = double.Parse(Console.ReadLine());
                            string bal3 = bmt.b_transfer(tamount, toaccount, accountno);
                            Console.WriteLine(bal3);

                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 4:
                            double interest = bi.b_ViewInterest(accountno);
                            Console.WriteLine("\n\t\t\tInterest Amount " + interest);
                            if (interest > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.WriteLine("\n\t What do you want to do with the interest amount\n");
                                Console.WriteLine("\t  1. Withdraw \n\t  2. Add to Account Balance \n\t  3. Exit ");
                                Console.ForegroundColor = ConsoleColor.Black;
                                int iaction = int.Parse(Console.ReadLine());
                                switch (iaction)
                                {
                                    case 1:
                                        string bal4 = bi.b_WithdrawInterest(interest, accountno);
                                        Console.WriteLine("\t Interest Amount Withdrawn \n\t  Interest Balance: 0.00 \n\t Available Balance : " + bal4);

                                        Console.WriteLine("\nPress any key to go back");
                                        Console.ReadKey();
                                        break;
                                    case 2:
                                        string bal5 = bi.b_AddInterest(interest, accountno);
                                        Console.WriteLine("\t Interest Amount Added to Account Balance \n\t Interest Balance: 0.00 \n\t Available Balance : " + bal5);
                                        Console.WriteLine("\nPress any key to go back");
                                        Console.ReadKey();
                                        break;
                                    case 3:
                                        break;
                                    default:
                                        break;
                                }                               
                            }
                            else
                            {
                                Console.WriteLine("\nPress any key to go back");
                                Console.ReadKey();
                            }
                            break;

                        case 5:
                            double balance = bmt.b_availablebalance(accountno);
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("\n\t\t\tAvailable Balance :" + balance);
                            Console.ForegroundColor = ConsoleColor.Black;

                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 6:
                            passlabel:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("\n\t\t\tEnter New Password");
                            Console.SetCursorPosition(Console.CursorLeft + 24, Console.CursorTop);
                            Console.ForegroundColor = ConsoleColor.Black;
                            string newpassword = (Console.ReadLine());

                            try
                            {
                                if (newpassword.Length < 6)
                                    throw new DataEntryException("password length should be greater than 5");
                            }
                            catch (DataEntryException e)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(e.Message);
                                Console.Beep();
                                goto passlabel;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.Beep();
                                goto passlabel;
                            }
                            bmt.b_updatePassword(newpassword, accountno);
                            Console.WriteLine("\n\t Password Updated \n\t New Password : " + newpassword);

                            Console.WriteLine("\nPress any key to go back");
                            Console.ReadKey();
                            break;

                        case 7:
                            flag = false;
                            break;

                        default:
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

               // Console.WriteLine("\nPress any key to go back");
               // Console.ReadKey();
            }


        }

    }
}
