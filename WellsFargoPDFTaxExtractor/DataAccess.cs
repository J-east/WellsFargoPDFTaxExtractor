using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

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
            using (SqlConnection con = new SqlConnection(conStr)) {
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
        }

        [Table("Transactions")]
        public class TransactionContrib {
            [Key]
            public int TransactionID { get; set; }

            public long AccountNumber { get; set; }
            public DateTime TransDate { get; set; }
            public string Title { get; set; }
            public string Summary { get; set; }
            public string Catagory { get; set; }
            public string TypeOfTransaction { get; set; }
            public double Amount { get; set; }
        }

        public static bool UpdateRow(TransactionContrib t) {
            string conStr;
            if (Program.Settings.SqlSettings.IntegratedSec) {
                conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};Integrated Security = SSPI;";
            }
            else {
                conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};UID={Program.Settings.SqlSettings.userID};PWD={sqlpw}";
            }
            using (SqlConnection connection = new SqlConnection(conStr)) {
                return connection.Update(t);
            }            
        }

        /// <summary>
        /// returns all the transactions
        /// </summary>                
        public static List<TransactionContrib> GetAllTransactions() {
            List<TransactionContrib> toRet = new List<TransactionContrib>();

            try {
                string conStr;
                if (Program.Settings.SqlSettings.IntegratedSec) {
                    conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};Integrated Security = SSPI;";
                }
                else {
                    conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};UID={Program.Settings.SqlSettings.userID};PWD={sqlpw}";
                }

                using (SqlConnection con = new SqlConnection(conStr)) {
                    con.Open();
                    IEnumerable<TransactionContrib> resultList = con.Query<TransactionContrib>(@"SELECT * FROM Transactions ORDER BY TransDate");

                    return resultList.ToList();
                }
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

                using (SqlConnection con = new SqlConnection(conStr)) {
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
            }
            catch (Exception e) {
                return false;
            }
        }
    }
}
