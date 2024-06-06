using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuperMarket_G5
{
    public partial class Login : Form
    {

        public static SqlConnection Con = Program.ConSys;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
      }
        

        private void t1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                t2.Select();
            }
        }

        private void t2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                t1.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = t1.Text;
            string password = t2.Text;
            string typeUser;

            if (username == "" || password == "")
            {
                MessageBox.Show("Username or password is empty");
                return;
            }
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [SuperMarket].[dbo].[Usres] WHERE Username = @username AND Password = @password", Con);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                typeUser = reader["UserType"].ToString();
                Program.UserSystem = reader["username"].ToString();
                Program.UserID = Convert.ToInt32(reader["ID"].ToString());

                Con.Close();
                if (typeUser == "Manager")
                {
                    ManagerForm frm = new ManagerForm();
                    frm.Show();
                    this.Hide();
                }
                else if (typeUser == "Customer")
                {
                    CustomerForm frm = new CustomerForm();
                    frm.Show();
                    this.Hide();
                }

            }

            else
            {
                MessageBox.Show("Incorrect: Username or password !");
                t1.Text = "";
                t2.Text = "";
                t1.Select();
            }
            Con.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            t1.Select();
        }
    }
}
