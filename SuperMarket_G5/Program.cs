using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperMarket_G5
{
    static class Program
    {

        private static string ConStr = "Data Source=GHALA\SQLEXPRESS; Initial Catalog=SuperMarket;Integrated Security=True";
        public static SqlConnection ConSys = new SqlConnection(ConStr);
        public static string UserSystem = "Admin";
        public static int UserID;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
