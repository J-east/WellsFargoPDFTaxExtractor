using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WellsFargoPDFTaxExtractor {
    public partial class MainForm : Form {
        bool checkedForCorrectTables = false;

        public MainForm() {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(Program.Settings.SqlSettings.server)) {
                lblServer.Text = $"Server: {Program.Settings.SqlSettings.server}";
                lblDB.Text = $"Database: {Program.Settings.SqlSettings.database}";
                lblUID.Text = string.IsNullOrWhiteSpace(Program.Settings.SqlSettings.userID) ? "Integrated Security" : $"UserID: {Program.Settings.SqlSettings.userID}";
            }

            LoadTransactions();

            LoadTransactionOverview();
        }



        /// <summary>
        /// typesOfTransactions:
        ///     Personal Income
        ///     Business Income
        ///     Business Sales Income
        ///     Other Income
        ///     Gift
        ///     Personal Spending
        ///     Business Spending
        /// </summary>
        private void LoadTransactionOverview() {
            // laziness knows no bounds
            List<DataAccess.TransactionContrib> transactions = DataAccess.GetAllTransactions();

            double businessPurchases = transactions.Any(a => a.TypeOfTransaction == "Business Spending") ? transactions.Where(a => a.TypeOfTransaction == "Business Spending").Select(a => a.Amount).Aggregate((a, b) => a + b) : 0;
            double personalPurchases = transactions.Any(a => a.TypeOfTransaction == "Personal Spending") ? transactions.Where(a => a.TypeOfTransaction == "Personal Spending").Select(a => a.Amount).Aggregate((a, b) => a + b) : 0;

            double personalIncome = transactions.Any(a => a.TypeOfTransaction == "Personal Income") ? transactions.Where(a => a.TypeOfTransaction == "Personal Income").Select(a => a.Amount).Aggregate((a, b) => a + b) : 0;
            double businessIncome = transactions.Any(a => a.TypeOfTransaction == "Business Income") ? transactions.Where(a => a.TypeOfTransaction == "Business Income").Select(a => a.Amount).Aggregate((a, b) => a + b) : 0;
            double businessSales = transactions.Any(a => a.TypeOfTransaction == "Business Sales Income") ? transactions.Where(a => a.TypeOfTransaction == "Business Sales Income").Select(a => a.Amount).Aggregate((a, b) => a + b) : 0;

            double businessProfitLoss = (businessIncome + businessSales) - businessPurchases;
            double personalMoney = personalIncome - personalPurchases;

            double gift = transactions.Any(a => a.TypeOfTransaction == "Gift") ? transactions.Where(a => a.TypeOfTransaction == "Gift").Select(a => a.Amount).Aggregate((a, b) => a + b) : 0;
            double otherIncome = transactions.Any(a => a.TypeOfTransaction == "Other Income") ? transactions.Where(a => a.TypeOfTransaction == "Other Income").Select(a => a.Amount).Aggregate((a, b) => a + b) : 0;

            lblBusinessExpenses.Text = $"${businessPurchases}";
            lblPersonalExpenses.Text = $"${personalPurchases}";
            lblRawIncome.Text = $"${personalIncome + businessIncome + businessSales}";
            lblBusinessIncome.Text = $"${businessSales + businessIncome}";

            lblTaxableIncome.Text = $"${personalIncome + businessIncome + businessSales - businessPurchases}";
        }

        // rows are associated with transaction IDs, really easy, I'm keeping everything writable for ease of use, but you do you
        private void LoadTransactions() {
            List<DataAccess.TransactionContrib> transactions = DataAccess.GetAllTransactions();

            foreach (DataAccess.TransactionContrib t in transactions) {
                DataGridViewRow row = (DataGridViewRow)dgTransactions.Rows[0].Clone();
                row.Cells[0].Value = t.TransactionID;
                row.Cells[1].Value = t.AccountNumber;
                row.Cells[2].Value = t.TransDate;
                row.Cells[3].Value = t.Title;
                row.Cells[4].Value = t.Summary;
                row.Cells[5].Value = t.Catagory;
                row.Cells[6].Value = t.TypeOfTransaction;
                row.Cells[7].Value = t.Amount;

                dgTransactions.Rows.Add(row);
            }
        }

        // select the folder and extract all the of the data from the contained pdf files
        private void bExtractFolder_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(Program.Settings.SqlSettings.server)) {
                MessageBox.Show("Please setup sql connection info");
            }
            // handle some house cleaning
            if (!checkedForCorrectTables) {
                DataAccess.HandleHouseCleaning();
            }

            // pick the folder
            var directoryDialog = new CommonOpenFileDialog {
                IsFolderPicker = true,
                Title = "Select Folder"
            };

            directoryDialog.ShowDialog();

            try {
                if (!string.IsNullOrWhiteSpace(directoryDialog.FileName)) {
                    string directory = directoryDialog.FileName;
                    PDFExtractor.ExtractBusinessData(directory);
                }
            }
            catch { }
        }

        private void bSetUpSql_Click(object sender, EventArgs e) {
            SqlInfoCaptureForm sqlinfo = new SqlInfoCaptureForm();
            sqlinfo.ShowDialog();
            if (!string.IsNullOrWhiteSpace(Program.Settings.SqlSettings.server)) {
                lblServer.Text = $"Server: {Program.Settings.SqlSettings.server}";
                lblDB.Text = $"Database: {Program.Settings.SqlSettings.database}";
                lblUID.Text = string.IsNullOrWhiteSpace(Program.Settings.SqlSettings.userID) ? "Integrated Security" : $"UserID: {Program.Settings.SqlSettings.userID}";
            }
        }

        private void bExtractCreditFolder_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(Program.Settings.SqlSettings.server)) {
                MessageBox.Show("Please setup sql connection info");
            }
            // handle some house cleaning
            if (!checkedForCorrectTables) {
                DataAccess.HandleHouseCleaning();
            }

            // pick the folder
            var directoryDialog = new CommonOpenFileDialog {
                IsFolderPicker = true,
                Title = "Select Folder"
            };

            directoryDialog.ShowDialog();

            try {
                if (!string.IsNullOrWhiteSpace(directoryDialog.FileName)) {
                    string directory = directoryDialog.FileName;
                    PDFExtractor.ExtractCreditData(directory);
                }
            }
            catch { }
        }

        private void bExtractChecking_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(Program.Settings.SqlSettings.server)) {
                MessageBox.Show("Please setup sql connection info");
            }
            // handle some house cleaning
            if (!checkedForCorrectTables) {
                DataAccess.HandleHouseCleaning();
            }

            // pick the folder
            var directoryDialog = new CommonOpenFileDialog {
                IsFolderPicker = true,
                Title = "Select Folder"
            };

            directoryDialog.ShowDialog();

            try {
                if (!string.IsNullOrWhiteSpace(directoryDialog.FileName)) {
                    string directory = directoryDialog.FileName;
                    PDFExtractor.ExtractCheckingData(directory);
                }
            }
            catch { }
        }

        private void updateTotals() {

        }

        private void dgTransactions_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex > -1 && dgTransactions.Rows[e.RowIndex].Cells[0].Value != null) {

                DataAccess.TransactionContrib t = new DataAccess.TransactionContrib {
                    TransactionID = (int)dgTransactions.Rows[e.RowIndex].Cells[0].Value,
                    AccountNumber = (long)dgTransactions.Rows[e.RowIndex].Cells[1].Value,
                    TransDate = (DateTime)dgTransactions.Rows[e.RowIndex].Cells[2].Value,
                    Title = (string)dgTransactions.Rows[e.RowIndex].Cells[3].Value,
                    Summary = (string)dgTransactions.Rows[e.RowIndex].Cells[4].Value,
                    Catagory = (string)dgTransactions.Rows[e.RowIndex].Cells[5].Value,
                    TypeOfTransaction = (string)dgTransactions.Rows[e.RowIndex].Cells[6].Value,
                    Amount = (double)dgTransactions.Rows[e.RowIndex].Cells[7].Value
                };

                DataAccess.UpdateRow(t);

                LoadTransactionOverview();
            }


        }
    }
}
