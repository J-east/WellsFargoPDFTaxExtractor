﻿using System;
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

            LoadTransactions();
        }

        // rows are associated with transaction IDs, really easy, I'm keeping everything writable for ease of use, but you do you
        private void LoadTransactions() {

            List<DataAccess.Transaction> transactions = DataAccess.GetAllTransactions();


            foreach (DataAccess.Transaction t in transactions) {
                DataGridViewRow row = (DataGridViewRow)dgTransactions.Rows[0].Clone();
                row.Cells[0].Value = t.TransactionID;
                row.Cells[1].Value = t.accountNumber;
                row.Cells[2].Value = t.TransDate;
                row.Cells[3].Value = t.Title;
                row.Cells[4].Value = t.Summary;
                row.Cells[5].Value = t.catagory;
                row.Cells[6].Value = t.typeOfTransaction;
                row.Cells[7].Value = t.amount;

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

        // update the sql and reload... because I'm lazy yeah I know it's bad, future job prospects, if you are viewing this
        // know that I'm not usually like this, but it's an open source project and it's super fast at this point so no issue
        // risk/reward
        private void dgTransactions_CellValueChanged(object sender, DataGridViewCellEventArgs e) {

        }
    }
}
