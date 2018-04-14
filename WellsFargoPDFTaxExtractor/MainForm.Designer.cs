namespace WellsFargoPDFTaxExtractor {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.bExtractFolder = new System.Windows.Forms.Button();
            this.bSetUpSql = new System.Windows.Forms.Button();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblDB = new System.Windows.Forms.Label();
            this.lblUID = new System.Windows.Forms.Label();
            this.bExtractCreditFolder = new System.Windows.Forms.Button();
            this.bExtractChecking = new System.Windows.Forms.Button();
            this.dgTransactions = new System.Windows.Forms.DataGridView();
            this.TransactionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Catagory = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BusinessOrPersonalTransaction = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRawIncome = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPersonalExpenses = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBusinessIncome = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBusinessExpenses = new System.Windows.Forms.Label();
            this.lblTaxableIncome = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            // 
            // bExtractFolder
            // 
            this.bExtractFolder.Location = new System.Drawing.Point(12, 12);
            this.bExtractFolder.Name = "bExtractFolder";
            this.bExtractFolder.Size = new System.Drawing.Size(127, 24);
            this.bExtractFolder.TabIndex = 0;
            this.bExtractFolder.Text = "Extract Business Folder";
            this.bExtractFolder.UseVisualStyleBackColor = true;
            this.bExtractFolder.Click += new System.EventHandler(this.bExtractFolder_Click);
            // 
            // bSetUpSql
            // 
            this.bSetUpSql.Location = new System.Drawing.Point(1033, 12);
            this.bSetUpSql.Name = "bSetUpSql";
            this.bSetUpSql.Size = new System.Drawing.Size(127, 24);
            this.bSetUpSql.TabIndex = 1;
            this.bSetUpSql.Text = "Setup Sql Info";
            this.bSetUpSql.UseVisualStyleBackColor = true;
            this.bSetUpSql.Click += new System.EventHandler(this.bSetUpSql_Click);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(549, 18);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(44, 13);
            this.lblServer.TabIndex = 3;
            this.lblServer.Text = "Server: ";
            // 
            // lblDB
            // 
            this.lblDB.AutoSize = true;
            this.lblDB.Location = new System.Drawing.Point(790, 18);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(56, 13);
            this.lblDB.TabIndex = 4;
            this.lblDB.Text = "Database:";
            // 
            // lblUID
            // 
            this.lblUID.AutoSize = true;
            this.lblUID.Location = new System.Drawing.Point(917, 18);
            this.lblUID.Name = "lblUID";
            this.lblUID.Size = new System.Drawing.Size(43, 13);
            this.lblUID.TabIndex = 5;
            this.lblUID.Text = "UserID:";
            // 
            // bExtractCreditFolder
            // 
            this.bExtractCreditFolder.Location = new System.Drawing.Point(145, 12);
            this.bExtractCreditFolder.Name = "bExtractCreditFolder";
            this.bExtractCreditFolder.Size = new System.Drawing.Size(127, 24);
            this.bExtractCreditFolder.TabIndex = 6;
            this.bExtractCreditFolder.Text = "Extract Credit Folder";
            this.bExtractCreditFolder.UseVisualStyleBackColor = true;
            this.bExtractCreditFolder.Click += new System.EventHandler(this.bExtractCreditFolder_Click);
            // 
            // bExtractChecking
            // 
            this.bExtractChecking.Location = new System.Drawing.Point(278, 12);
            this.bExtractChecking.Name = "bExtractChecking";
            this.bExtractChecking.Size = new System.Drawing.Size(127, 24);
            this.bExtractChecking.TabIndex = 7;
            this.bExtractChecking.Text = "Extract Checking Folder";
            this.bExtractChecking.UseVisualStyleBackColor = true;
            this.bExtractChecking.Click += new System.EventHandler(this.bExtractChecking_Click);
            // 
            // dgTransactions
            // 
            this.dgTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TransactionID,
            this.AccountNumber,
            this.TransDate,
            this.Title,
            this.Summary,
            this.Catagory,
            this.BusinessOrPersonalTransaction,
            this.Amount});
            this.dgTransactions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgTransactions.Location = new System.Drawing.Point(12, 42);
            this.dgTransactions.Name = "dgTransactions";
            this.dgTransactions.Size = new System.Drawing.Size(1165, 356);
            this.dgTransactions.TabIndex = 8;
            this.dgTransactions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTransactions_CellEndEdit);
            // 
            // TransactionID
            // 
            this.TransactionID.HeaderText = "TransactionID";
            this.TransactionID.Name = "TransactionID";
            // 
            // AccountNumber
            // 
            this.AccountNumber.HeaderText = "AccountNumber";
            this.AccountNumber.Name = "AccountNumber";
            this.AccountNumber.Width = 150;
            // 
            // TransDate
            // 
            this.TransDate.HeaderText = "TransactionDate";
            this.TransDate.Name = "TransDate";
            // 
            // Title
            // 
            this.Title.HeaderText = "Title";
            this.Title.Name = "Title";
            // 
            // Summary
            // 
            this.Summary.HeaderText = "Summary";
            this.Summary.Name = "Summary";
            this.Summary.Width = 360;
            // 
            // Catagory
            // 
            this.Catagory.HeaderText = "Catagory";
            this.Catagory.Items.AddRange(new object[] {
            "Payments",
            "Purchases",
            "deposited check",
            "Other",
            "online transfer",
            "B2B ACH Debit",
            "OtherCredits",
            "Purchase",
            "ATM Withdrawl"});
            this.Catagory.Name = "Catagory";
            // 
            // BusinessOrPersonalTransaction
            // 
            this.BusinessOrPersonalTransaction.HeaderText = "Business or Personal Transaction";
            this.BusinessOrPersonalTransaction.Items.AddRange(new object[] {
            "Personal Income",
            "Business Income",
            "Business Sales Income",
            "Other Income",
            "Gift",
            "Personal Spending",
            "Business Spending"});
            this.BusinessOrPersonalTransaction.Name = "BusinessOrPersonalTransaction";
            // 
            // Amount
            // 
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Raw Income:";
            // 
            // lblRawIncome
            // 
            this.lblRawIncome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblRawIncome.AutoSize = true;
            this.lblRawIncome.Location = new System.Drawing.Point(90, 414);
            this.lblRawIncome.Name = "lblRawIncome";
            this.lblRawIncome.Size = new System.Drawing.Size(34, 13);
            this.lblRawIncome.TabIndex = 10;
            this.lblRawIncome.Text = "$0.00";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 414);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Personal Expenses:";
            // 
            // lblPersonalExpenses
            // 
            this.lblPersonalExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPersonalExpenses.AutoSize = true;
            this.lblPersonalExpenses.Location = new System.Drawing.Point(275, 414);
            this.lblPersonalExpenses.Name = "lblPersonalExpenses";
            this.lblPersonalExpenses.Size = new System.Drawing.Size(34, 13);
            this.lblPersonalExpenses.TabIndex = 12;
            this.lblPersonalExpenses.Text = "$0.00";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(343, 414);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Business Income:";
            // 
            // lblBusinessIncome
            // 
            this.lblBusinessIncome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBusinessIncome.AutoSize = true;
            this.lblBusinessIncome.Location = new System.Drawing.Point(439, 414);
            this.lblBusinessIncome.Name = "lblBusinessIncome";
            this.lblBusinessIncome.Size = new System.Drawing.Size(34, 13);
            this.lblBusinessIncome.TabIndex = 14;
            this.lblBusinessIncome.Text = "$0.00";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(503, 414);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Business Expenses:";
            // 
            // lblBusinessExpenses
            // 
            this.lblBusinessExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBusinessExpenses.AutoSize = true;
            this.lblBusinessExpenses.Location = new System.Drawing.Point(610, 414);
            this.lblBusinessExpenses.Name = "lblBusinessExpenses";
            this.lblBusinessExpenses.Size = new System.Drawing.Size(34, 13);
            this.lblBusinessExpenses.TabIndex = 16;
            this.lblBusinessExpenses.Text = "$0.00";
            // 
            // lblTaxableIncome
            // 
            this.lblTaxableIncome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTaxableIncome.AutoSize = true;
            this.lblTaxableIncome.Location = new System.Drawing.Point(764, 414);
            this.lblTaxableIncome.Name = "lblTaxableIncome";
            this.lblTaxableIncome.Size = new System.Drawing.Size(34, 13);
            this.lblTaxableIncome.TabIndex = 18;
            this.lblTaxableIncome.Text = "$0.00";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(675, 414);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Taxable Income";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(411, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Print Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 436);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTaxableIncome);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblBusinessExpenses);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblBusinessIncome);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblPersonalExpenses);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRawIncome);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgTransactions);
            this.Controls.Add(this.bExtractChecking);
            this.Controls.Add(this.bExtractCreditFolder);
            this.Controls.Add(this.lblUID);
            this.Controls.Add(this.lblDB);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.bSetUpSql);
            this.Controls.Add(this.bExtractFolder);
            this.Name = "MainForm";
            this.Text = "Awesome Wells Fargo Extractor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bExtractFolder;
        private System.Windows.Forms.Button bSetUpSql;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblDB;
        private System.Windows.Forms.Label lblUID;
        private System.Windows.Forms.Button bExtractCreditFolder;
        private System.Windows.Forms.Button bExtractChecking;
        private System.Windows.Forms.DataGridView dgTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Summary;
        private System.Windows.Forms.DataGridViewComboBoxColumn Catagory;
        private System.Windows.Forms.DataGridViewComboBoxColumn BusinessOrPersonalTransaction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRawIncome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPersonalExpenses;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblBusinessIncome;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBusinessExpenses;
        private System.Windows.Forms.Label lblTaxableIncome;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}

