using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.IO;

namespace WellsFargoPDFTaxExtractor {
    static class Program {
        public static MySettings Settings;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Settings = MySettings.Load()?? new MySettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public class MySettings : AppSettings<MySettings> {
            public class UserSettings {
                public string server;
                public string database;
                public string userID;
                public bool IntegratedSec;
            }

            public UserSettings SqlSettings = new UserSettings();
        }
    }

    public class AppSettings<T> where T : new() {
        private const string DEFAULT_FILENAME = "settings.json";

        public void Save(string fileName = DEFAULT_FILENAME) {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME) {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        public static T Load(string fileName = DEFAULT_FILENAME) {
            T t = new T();
            if (File.Exists(fileName))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            return t;
        }
    }
}
