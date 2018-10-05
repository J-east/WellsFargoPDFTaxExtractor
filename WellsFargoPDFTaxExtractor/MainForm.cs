using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
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
        StringFormat strFormat; //Used to format the grid rows.
        List<int> arrColumnLefts = new List<int>();//Used to save left coordinates of columns
        List<int> arrColumnWidths = new List<int>();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height

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
        List<DataAccess.TransactionContrib> transactions;
        private void LoadTransactions() {
            transactions = DataAccess.GetAllTransactions();

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

            try {
                cbAccountNum.Items.Add("no filter");
                cbTranType.Items.Add("no filter");
                cbAccountNum.SelectedItem = "no filter";
                cbTranType.SelectedItem = "no filter";
                cbAccountNum.Items.AddRange(transactions.Select(a => (object)a.AccountNumber).Distinct().ToArray());
                cbTranType.Items.AddRange(transactions.Select(a => (object)a.TypeOfTransaction?.ToString()??"").Distinct().ToArray());                
            }
            catch  {  }
        }

        private void FilterTransactions() {
            try {
                dgTransactions.Rows.Clear();
                transactions = DataAccess.GetAllTransactions();
                List<DataAccess.TransactionContrib> filteredTransactions = transactions;
                if (cbAccountNum.SelectedItem.ToString() != "no filter")
                    filteredTransactions = filteredTransactions.Where(a => a.AccountNumber.ToString() == cbAccountNum.SelectedItem.ToString()).ToList();
                if (cbTranType.SelectedItem.ToString() != "no filter")
                    filteredTransactions = filteredTransactions.Where(a => (a.TypeOfTransaction == null ? "" : a.TypeOfTransaction) == cbTranType.SelectedItem.ToString()).ToList();

                foreach (DataAccess.TransactionContrib t in filteredTransactions) {
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
                
                double amount = filteredTransactions.Any() ? filteredTransactions.Select(a => a.Amount).Aggregate((a, b) => a + b) : 0;
                lblAmount.Text = $"${amount}";
            }
            catch { }
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

        private void MainForm_Load(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
            //Open the print dialog
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;
            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog()) {
                printDocument1.DocumentName = cbTranType.SelectedItem.ToString();
                printDocument1.Print();
            }
        }

        /// <summary>
        /// Handles the begin print event of print document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            try {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dgTransactions.Columns) {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the print page event of print document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
            try {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage) {
                    foreach (DataGridViewColumn GridCol in dgTransactions.Columns) {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dgTransactions.Rows.Count - 1) {
                    DataGridViewRow GridRow = dgTransactions.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top) {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else {
                        if (bNewPage) {
                            //Draw Header
                            e.Graphics.DrawString($"{cbTranType.SelectedItem.ToString()} total: {lblAmount.Text}", new Font(dgTransactions.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString($"{cbTranType.SelectedItem.ToString()} total: {lblAmount.Text}", new Font(dgTransactions.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(dgTransactions.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dgTransactions.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString($"{cbTranType.SelectedItem.ToString()} total: {lblAmount.Text}", new Font(new Font(dgTransactions.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dgTransactions.Columns) {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells) {
                            if (Cel.Value != null) {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc) {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbAccountNum_SelectedIndexChanged(object sender, EventArgs e) {
            FilterTransactions();
        }

        private void cbTranType_SelectedIndexChanged(object sender, EventArgs e) {
            FilterTransactions();
        }
    }
}
