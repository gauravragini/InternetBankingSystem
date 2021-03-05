using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using IBS.DataAccessLayer;
using IBS.Entities;
using IBS.Exceptions;

namespace IBS.BussinessLayer
{
   public class BLInterestCalculation
    {
       
       
        DLInterestCalculation di ;

        public BLInterestCalculation(DLInterestCalculation di) 
        {
            this.di = di;
        }
        public void b_CalculateInterest(List<Account> accountlist, string adminid)
        {
            DateTime datecheck = new DateTime(1000, 01, 01, 12, 00, 00);

            DateTime last = di.d_lastInterestCalculation();

            if (last == datecheck)
            {
                di.d_CalculateInterest(accountlist,adminid);
            }
            else
            {
                int numberofdays = (last - DateTime.Now).Days;

                if (numberofdays < 30)
                {
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    throw new InterestException("\n\t\t\tYou can Calculate the Interest after 30 days only \n\t\t\tLast Calculated interest on :" + last + "By Admin Id :"+adminid+"\n Days left : " + numberofdays);
                    Console.ForegroundColor = ConsoleColor.Black;
                    
                }
                else
                {
                    di.d_CalculateInterest(accountlist,adminid);
                }
            }

        }

        public double b_ViewInterest(string accountno)
        {
            return di.d_ViewInterest(accountno);
        }
        public string b_WithdrawInterest(double interest, string accountno)
        {
           return di.d_WithdrawInterest(interest, accountno);
        }
        public string b_AddInterest(double interest, string accountno)
        {
            return di.d_AddInterest(interest, accountno);
        }
    }
}
