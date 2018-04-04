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
            string conStr;
            if (Program.Settings.SqlSettings.IntegratedSec) {
                conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};Integrated Security = SSPI;";
            }
            else {
                conStr = $"SERVER={Program.Settings.SqlSettings.server};DATABASE={Program.Settings.SqlSettings.database};UID={Program.Settings.SqlSettings.userID};PWD={tbPW}";
            }
            using (SqlConnection conn = new SqlConnection(conStr)) {
                try {
                    conn.Open();
                }
                catch { MessageBox.Show("error, please try again"); return; }
            }

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
