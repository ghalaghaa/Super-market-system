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
    public partial class ManagerForm : Form
    {

        public static SqlConnection Con = Program.ConSys;

        public ManagerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t1.Text = "";
            t2.Text = "";
            t3.Text = "";
            t4.Text = "";
            t5.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [SuperMarket].[dbo].[Products] ([ProdectName], [Category], [Brand], [Price], [QuantityInStatick]) VALUES (@productName, @category, @brand, @price, @quantityInStatick)", Con);
            command.Parameters.AddWithValue("@productName", t1.Text );
            command.Parameters.AddWithValue("@category", t2.Text );
            command.Parameters.AddWithValue("@brand", t3.Text );
            command.Parameters.AddWithValue("@price", t4.Text );
            command.Parameters.AddWithValue("@quantityInStatick", t5.Text);
            command.ExecuteNonQuery();

            SearchByProducteName("");
            MessageBox.Show("The product has been added successfuly");
            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            SqlCommand command = new SqlCommand("UPDATE [SuperMarket].[dbo].[Products] SET [ProdectName] = @productName, [Category] = @category, [Brand] = @brand, [Price] = @price, [QuantityInStatick] = @quantityInStatick WHERE [ID] = @id", Con);
            command.Parameters.AddWithValue("@productName", t1.Text);
            command.Parameters.AddWithValue("@category", t2.Text);
            command.Parameters.AddWithValue("@brand", t3.Text);
            command.Parameters.AddWithValue("@price", t4.Text);
            command.Parameters.AddWithValue("@quantityInStatick", t5.Text);
            command.Parameters.AddWithValue("@id", (int)Dataview1.CurrentRow.Cells[0].Value);

            command.ExecuteNonQuery();

            Con.Close();
            SearchByProducteName("");
            MessageBox.Show("The product has been Updated successfuly");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [SuperMarket].[dbo].[Products] WHERE [ID] = @id", Con);
            command.Parameters.AddWithValue("@id", (int)Dataview1.CurrentRow.Cells[0].Value);
            command.ExecuteNonQuery();
            Con.Close();
            SearchByProducteName("");
            MessageBox.Show("The product has been Deleted successfuly");
        }

        private void SearchByProducteName(string ser)
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            var dataAdapter = new SqlDataAdapter("SELECT * FROM [SuperMarket].[dbo].[Products]  Where [ProdectName] Like '%" + TxtSer.Text + "%'", Con);      
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            Dataview1.DataSource = dataTable;
            Con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SearchByProducteName(TxtSer.Text);
        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            SearchByProducteName("");
            Con.Close();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.Show();
            this.Hide();
        }

        private void Dataview1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            t1.Text = (string)Dataview1.CurrentRow.Cells[1].Value;
            t2.Text = (string)Dataview1.CurrentRow.Cells[2].Value;
            t3.Text = (string)Dataview1.CurrentRow.Cells[3].Value;
            t4.Text = ((double)Dataview1.CurrentRow.Cells[4].Value).ToString();
            t5.Text = ((int)Dataview1.CurrentRow.Cells[5].Value).ToString();
        }
    }
}
