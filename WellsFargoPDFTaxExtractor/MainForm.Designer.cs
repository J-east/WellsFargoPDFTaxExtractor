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
            ((System.ComponentModel.ISupportInitialize)(this.dgTransactions)).BeginInit();
            this.SuspendLayout();
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
            this.dgTransactions.Location = new System.Drawing.Point(12, 42);
            this.dgTransactions.Name = "dgTransactions";
            this.dgTransactions.Size = new System.Drawing.Size(1154, 333);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 416);
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
    }
}

