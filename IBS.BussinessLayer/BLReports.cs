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
    public class BLReports
    {
        DLReports dr;
        public BLReports(DLReports dr)
        {
            this.dr = dr;
        }
        public List<Transaction> b_transactionDetails()
        {
            List<Transaction> transaclist = dr.d_transactionDetails();
            return transaclist;
        }
        public List<Account> b_AccountDetails()
        {
            return dr.d_AccountDetails();
        }
    }
}
