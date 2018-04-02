using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace WellsFargoPDFTaxExtractor {
    public static class DataAccess {
        public static string sqlpw;
        // makes sure that everything is in the right place, throws an exception if there's some weirdness
        public static void HandleHouseCleaning() {
            string conStr;
            if (Program.Settings.SqlSettings.IntegratedSec) {
                conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};Integrated Security = SSPI;";
            }
            else {
                conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};UID={Program.Settings.SqlSettings.userID};PWD={sqlpw}";
            }


            string sqlStr = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Transactions'";
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            if ((int)new SqlCommand(sqlStr, con).ExecuteScalar() == 0) {
                sqlStr = @"
create table Transactions (
	TransactionID int IDENTITY (1,1) NOT NULL,
    accountNumber bigint not null,
	TransDate DateTime Not Null,
	Title varchar(32) not null,
	Summary varchar(200) not null,
	catagory varchar(32) not null,
	typeOfTransaction varchar(32) not null,
	amount DECIMAL(19,4) not null,
	)
	)";
                new SqlCommand(sqlStr).ExecuteNonQuery();
            }
        }

        public class Transaction {
            public int TransactionID;
            public long accountNumber;
            public DateTime TransDate;
            public string Title;
            public string Summary;
            public string catagory;
            public string typeOfTransaction;
            public double amount;
        }

        public static bool UpdateRow(Transaction t) {
            string sql = "update TRANSACTIONS SET ???? where TransactionID = @transactionID";

            // code to take t and update the rows where things are different

            return true;
        }

        /// <summary>
        /// returns all the transactions
        /// </summary>                
        public static List<Transaction> GetAllTransactions() {
            List<Transaction> toRet = new List<Transaction>();

            try {
                string conStr;
                if (Program.Settings.SqlSettings.IntegratedSec) {
                    conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};Integrated Security = SSPI;";
                }
                else {
                    conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};UID={Program.Settings.SqlSettings.userID};PWD={sqlpw}";
                }

                SqlConnection con = new SqlConnection(conStr);
                con.Open();

                IEnumerable<Transaction> resultList = con.Query<Transaction>(@"SELECT * FROM Transactions ORDER BY TransDate");
                return resultList.ToList();
            }
            catch {
                return null;
            }

            return toRet;
        }

        public static bool CheckAndInsertTransaction(long accountNumber, DateTime dateOfTransaction, string typeOfTrans, string desc, double amount) {
            try {
                string conStr;
                if (Program.Settings.SqlSettings.IntegratedSec) {
                    conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};Integrated Security = SSPI;";
                }
                else {
                    conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};UID={Program.Settings.SqlSettings.userID};PWD={sqlpw}";
                }

                SqlConnection con = new SqlConnection(conStr);
                string sql = @"SELECT COUNT(*) FROM Transactions WHERE amount = @amount AND Summary = @summary";
                con.Open();
                IDictionary<string, object> existingRows = con.QuerySingle(sql, new { amount = amount, summary = desc });

                if ((int)(existingRows.Values.First()) > 0) {
                    return false;
                }

                sql = @"INSERT INTO Transactions 
(accountNumber, TransDate, Title, Summary, catagory, amount) 
Values 
(@accountNumber, @TransDate, @Title, @Summary, @catagory, @amount)";

                var affectedRows = con.Execute(sql, new {
                    accountNumber = accountNumber,
                    TransDate = dateOfTransaction,
                    Title = desc.Length > 30 ? desc.Substring(0, 30) : desc,
                    Summary = desc,
                    catagory = typeOfTrans,
                    amount = amount
                });

                return affectedRows == 1;
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
