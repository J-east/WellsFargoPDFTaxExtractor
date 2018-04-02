namespace WellsFargoPDFTaxExtractor {
    partial class SqlInfoCaptureForm {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.tbUID = new System.Windows.Forms.TextBox();
            this.tbPW = new System.Windows.Forms.TextBox();
            this.cbIntegratedSec = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Databse";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "UserIID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(109, 12);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(166, 20);
            this.tbServer.TabIndex = 6;
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(109, 38);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(166, 20);
            this.tbDatabase.TabIndex = 7;
            // 
            // tbUID
            // 
            this.tbUID.Location = new System.Drawing.Point(109, 64);
            this.tbUID.Name = "tbUID";
            this.tbUID.Size = new System.Drawing.Size(166, 20);
            this.tbUID.TabIndex = 8;
            // 
            // tbPW
            // 
            this.tbPW.Location = new System.Drawing.Point(109, 90);
            this.tbPW.Name = "tbPW";
            this.tbPW.PasswordChar = '*';
            this.tbPW.Size = new System.Drawing.Size(166, 20);
            this.tbPW.TabIndex = 9;
            // 
            // cbIntegratedSec
            // 
            this.cbIntegratedSec.AutoSize = true;
            this.cbIntegratedSec.Location = new System.Drawing.Point(35, 129);
            this.cbIntegratedSec.Name = "cbIntegratedSec";
            this.cbIntegratedSec.Size = new System.Drawing.Size(174, 17);
            this.cbIntegratedSec.TabIndex = 10;
            this.cbIntegratedSec.Text = "Uses Windows Authentication?";
            this.cbIntegratedSec.UseVisualStyleBackColor = true;
            // 
            // SqlInfoCaptureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 198);
            this.Controls.Add(this.cbIntegratedSec);
            this.Controls.Add(this.tbPW);
            this.Controls.Add(this.tbUID);
            this.Controls.Add(this.tbDatabase);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SqlInfoCaptureForm";
            this.Text = "SqlInfoCaptureForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.TextBox tbUID;
        private System.Windows.Forms.TextBox tbPW;
        private System.Windows.Forms.CheckBox cbIntegratedSec;
    }
}