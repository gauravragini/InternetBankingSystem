using System;
using System.Configuration;
using System.Diagnostics;
using IBS.PresentationLayer;
using IBS.BussinessLayer;
using IBS.Exceptions;
using IBS.DataAccessLayer;

namespace IBS.ServiceLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            DLAccountCreation da = new DLAccountCreation();
            DLMoneyTransaction dmt = new DLMoneyTransaction();
            DLInterestCalculation di = new DLInterestCalculation();
            DLReports dr = new DLReports();

            BLAccountCreation ba = new BLAccountCreation(da);
            BLMoneyTransaction bmt = new BLMoneyTransaction(dmt);
            BLInterestCalculation bi = new BLInterestCalculation(di);
            BLReports br = new BLReports(dr);

            userPresentation up = new userPresentation(ba,bmt,bi);
            adminPresentation ap = new adminPresentation(ba,bi,br);


        label:
            try
            {               
                heading("IBS");
                serivceMenu();
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        //Login as admin or user
                        Console.Clear();
                        heading("IBS");

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n\n\n\t\t\t\t\t\t      LOGIN PORTAL");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("\t\t\t\t\t\t      -------------");
                        Console.WriteLine("\n\n\t\t\t\t\t    please enter your UserID Number : ");
                        Console.SetCursorPosition(Console.CursorLeft + 55, Console.CursorTop);
                        string userid = Console.ReadLine();
                        Console.WriteLine("\n\t\t\t\t\t\t     Enter Password : ");
                        Console.SetCursorPosition(Console.CursorLeft + 55, Console.CursorTop);
                        string password = Console.ReadLine();
                        // check if login credentials are valid or not
                        bool ifvalid = ba.b_Login(userid, password);
                        if (ifvalid)
                        {
                            string role = ba.b_checkRole(userid, password);
                            Console.WriteLine(role);
                            if (role == "customer")
                            {
                                up.usermenu(userid, password);
                                
                            }
                            else if (role == "admin")
                            {
                                ap.adminMenu(userid);
                                
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.SetCursorPosition(Console.CursorLeft + 50, Console.CursorTop);
                            Console.WriteLine("\nIncorrect Username no or password\nLogin Failed");
                            Console.Beep();
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("\nPress any Key to go back");
                            Console.ReadKey();
                        }
                        
                        break;

                    case 2:
                        //Resgitration for both user and admin
                        Console.Clear();
                        heading("IBS Registration");
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("\n\n\n\n\t\t\tDo u want register as an admin or a user(Press A for admin and U for user)?\n\n");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(Console.CursorLeft + 57, Console.CursorTop);
                        char s = Console.ReadLine()[0];
                        if (s=='A' || s=='a')
                        {
                            Console.Clear();
                            heading("IBS Admin Registration");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\n\n\t\t\t\t\t\t   Register Your Account");
                            Console.WriteLine("\t\t\t\t\t\t   ---------------------");
                            Console.WriteLine("\n\n\tPlease enter your Personal details: ");
                            ap.adminregistration();
                        }
                        else if (s=='u' || s=='U')
                        {
                            Console.Clear();
                            heading("IBS User Registration");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\n\n\t\t\t\t\t\t   Register Your Account");
                            Console.WriteLine("\t\t\t\t\t\t   ---------------------");
                            Console.WriteLine("\n\n\tPlease enter your Personal details: ");
                            up.userRegistration();                                                                                
                        }
                        break;

                    case 3:
                        //Checking status of newly created account of Customer
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("\n\n\t\t\t\t    Enter the temporary User Id no of your application");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.SetCursorPosition(Console.CursorLeft + 60, Console.CursorTop);
                        string uid = Console.ReadLine();
                        string currstatus=ba.b_checkStatus(uid);
                        up.displayStatus(currstatus,uid);
                        Console.WriteLine("\nPress any Key to go back");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Invalid Choice");
                        Console.WriteLine("\nPress any Key to go back");
                        Console.ReadKey();
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("\nPress any Key to go back");
                Console.ReadKey();
            }

            goto label;
        }

        //Display of heading
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

        //display menu -- login/register/check status
        public static void serivceMenu()
        {
            Console.WriteLine("\n\n\n\t\t\t\t\t\tWhat do u want to do?\n");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n\t\t\t\t\t\t1: Log IN \n\t\t\t\t\t\t2: Register \n\t\t\t\t\t\t3: Check Status of Application");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n\n\n\t\t\t\t\t\tPlease Enter your Choice");
            Console.SetCursorPosition(Console.CursorLeft + 60, Console.CursorTop);
        }


    }
}
