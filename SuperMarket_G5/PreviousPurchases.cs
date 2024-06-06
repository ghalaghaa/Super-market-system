using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuperMarket_G5
{
    public partial class PreviousPurchases : Form
    {

        public static SqlConnection Con = Program.ConSys;

        public PreviousPurchases()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CustomerForm frm = new CustomerForm();
            frm.Show();
            this.Hide();
        }

        private void SearchByProducteName()
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            var dataAdapter = new SqlDataAdapter("SELECT * FROM [SuperMarket].[dbo].[Card]  Where [UserId] ='" +Program.UserID+"'", Con);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_pur.DataSource = dataTable;
            Con.Close();
        }

        private void PreviousPurchases_Load(object sender, EventArgs e)
        {
            SearchByProducteName();
        }
    }
}
