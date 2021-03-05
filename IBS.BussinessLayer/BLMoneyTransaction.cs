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
   public class BLMoneyTransaction
    {

        DLMoneyTransaction dmt;
        public BLMoneyTransaction(DLMoneyTransaction dmt)
        {
            this.dmt = dmt; 
        }
        //account and balance validation can be done by making respective functions in dataAccessLayer and using it here
        public string b_deposit(double damount, string accountno)
        {
            double availbal = dmt.d_availablebalance(accountno);
            string bal;
            if (damount < 0)
            {
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Red;
                bal = " Sorry ... you have entered Invalid Amount";                               
            }
            else
            {
                if (availbal == 0 && damount < 1000)
                {
                    bal = "To do Transactions, first deposit should be minimum amount of 1000";
                }
                else
                {
                    bal = dmt.d_deposit(damount, accountno);
                    bal = " Amount " + damount + " deposited to Account Number : " + accountno + "\n Available Balance : " + bal;
                }             

            }
            return bal;
        }
        public string b_withdraw(double wamount, string accountno)
        {
            //setting minimum balance to 1000
            string bal;
            double minbal = 1000;
            double availbal = dmt.d_availablebalance(accountno);
            if (availbal - wamount < minbal)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Beep();
                bal = "\t\tInsufficient Balance\n\n" +
                    "\t\tMinimium Balance for the account existence is 1000\n\t\tAvailable Balance: "+availbal;
                Console.ForegroundColor = ConsoleColor.Black;
                
            }
            else
            {
                bal = dmt.d_withdraw(wamount, accountno);
                bal = " Amount " + wamount + " withdrawn to Account Number : " + accountno + "\n Available Balance : " + bal;
            }

            return bal;
        }
        public string b_transfer(double tamount, string toaccount, string accountno)
        {
            string bal;
            double minbal = 1000;
            double availbal = dmt.d_availablebalance(accountno);
            if (availbal - tamount < minbal)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Beep();
                bal = "\t\tInsufficient Balance\n\n" +
                    "\t\tMinimium Balance for the account existence is 1000\n\t\tAvailable Balance: " + availbal;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                bool flag = dmt.d_accountexists(toaccount);
                if (flag)
                {
                    bal = dmt.d_transfer(tamount, toaccount, accountno);
                    bal = " Amount " + tamount + " Transferred to Account Number : " + toaccount + "\n Available Balance : "+bal;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    bal = " Sorry... You have entered an account number That Does not exist";
                    Console.Beep();
                }

            }
            return bal;

        }

        public double b_availablebalance(string accno)
        {
            return dmt.d_availablebalance(accno);
        }
        public void b_updatePassword(string newpassword, string accountno)
        {
            dmt.d_updatePassword(newpassword, accountno);        
        }

    }
}
