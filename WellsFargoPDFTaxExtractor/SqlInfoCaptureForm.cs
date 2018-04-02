using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WellsFargoPDFTaxExtractor {
    public partial class SqlInfoCaptureForm : Form {
        public SqlInfoCaptureForm() {
            InitializeComponent();

            tbServer.Text = Program.Settings?.SqlSettings.server ?? "";
            tbDatabase.Text = Program.Settings?.SqlSettings.database ?? "";
            tbUID.Text = Program.Settings?.SqlSettings.userID ?? "";
            tbPW.Text = DataAccess.sqlpw;
            cbIntegratedSec.Checked = Program.Settings.SqlSettings.IntegratedSec;
        }

        // okay
        private void button1_Click(object sender, EventArgs e) {
            SqlConnection conn;
            if (cbIntegratedSec.Checked) {
                conn = new SqlConnection($"SERVER={tbServer.Text};DATABASE={tbDatabase.Text};Integrated Security = SSPI;");
            }
            else {
                conn = new SqlConnection($"SERVER={tbServer.Text};DATABASE={tbDatabase.Text};UID={tbUID};PWD={tbPW}");
            }
            try {
                conn.Open();
            }
            catch { MessageBox.Show("error, please try again"); return; }

            Program.Settings.SqlSettings.server = tbServer.Text;
            Program.Settings.SqlSettings.database = tbDatabase.Text;
            Program.Settings.SqlSettings.userID = tbUID.Text;
            DataAccess.sqlpw = tbPW.Text;
            Program.Settings.SqlSettings.IntegratedSec = cbIntegratedSec.Checked;

            AppSettings<Program.MySettings>.Save(Program.Settings);

            this.Close();
        }

        // cancel
        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
