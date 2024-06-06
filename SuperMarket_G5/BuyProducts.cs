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
    public partial class BuyProducts : Form
    {

        public static SqlConnection Con = Program.ConSys;

        public BuyProducts()
        {
            InitializeComponent();
        }

        private void DisplayAllProducts()
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            var dataAdapter = new SqlDataAdapter("SELECT * FROM [SuperMarket].[dbo].[Products]", Con);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dgv_products.DataSource = dataTable;
            Con.Close();
        }

        private void BuyProducts_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            DisplayAllProducts();
        }

        private void t5_TextChanged(object sender, EventArgs e)
        {
            int qua =Int32.Parse(t5.Text);
            int pr = Int32.Parse(t4.Text);
            if (qua >= 1)
            {
                Total.Text = (pr * qua).ToString();
            }

            else
            {
                Total.Text = "0";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int Qua = Convert.ToInt32(dgv_products.CurrentRow.Cells[5].Value);

            int parsedQuantity;
            bool isValidQuantity = Int32.TryParse(t5.Text, out parsedQuantity);

            if (isValidQuantity && parsedQuantity > Qua)
            {
                MessageBox.Show("The quantity entered is greater than the quantity in stock!");
                return;
            }
            else if (!isValidQuantity)
            {
                MessageBox.Show("Invalid quantity entered. Please enter a valid integer.");
                return;
            }


            dgv_card.Rows.Add(dgv_products.CurrentRow.Cells[0].Value, dgv_products.CurrentRow.Cells[1].Value, dgv_products.CurrentRow.Cells[2].Value, dgv_products.CurrentRow.Cells[3].Value, t5.Text, t4.Text, Total.Text,"N");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int RowIndex = dgv_card.CurrentRow.Index;
            dgv_card.Rows.RemoveAt(RowIndex);
        }


        private void UpdateQuantityInStock()
        {
            int Qua = Convert.ToInt32(dgv_products.CurrentRow.Cells[5].Value);
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            SqlCommand command = new SqlCommand("UPDATE [SuperMarket].[dbo].[Products] SET [QuantityInStatick] = QuantityInStatick - " + Qua + " WHERE [ID] = @id", Con);
            command.Parameters.AddWithValue("@id", (int)dgv_card.CurrentRow.Cells[0].Value);

            command.ExecuteNonQuery();

            Con.Close();
        }

        private void AddPayment()
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [SuperMarket].[dbo].[Payments] (CID, TotalAmount, PaymentMethod) VALUES (@CID, @TotalAmount, @PaymentMethod)", Con);

         
            command.Parameters.AddWithValue("@CID", Program.UserID);
            command.Parameters.AddWithValue("@TotalAmount", dgv_card.CurrentRow.Cells[6].Value);
            command.Parameters.AddWithValue("@PaymentMethod", comboBox1.Text);
        
            command.ExecuteNonQuery();

            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Con.State == ConnectionState.Open) { Con.Close(); }
            Con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [SuperMarket].[dbo].[Card] ([Category],[ProdectId], [ProdectName], [Brand], [Price], [Quantity], [Total], [Reg_date], [UserId]) VALUES (@category,@ProdectId, @productName, @brand, @price, @quantity, @total, @regDate, @userId)", Con);
            command.Parameters.AddWithValue("@category", dgv_card.CurrentRow.Cells[2].Value);
            command.Parameters.AddWithValue("@ProdectId", dgv_card.CurrentRow.Cells[0].Value);
            command.Parameters.AddWithValue("@productName", dgv_card.CurrentRow.Cells[1].Value);
            command.Parameters.AddWithValue("@brand", dgv_card.CurrentRow.Cells[3].Value);
            command.Parameters.AddWithValue("@price",(double)dgv_products.CurrentRow.Cells[4].Value);
            command.Parameters.AddWithValue("@quantity", dgv_card.CurrentRow.Cells[5].Value);
            command.Parameters.AddWithValue("@total", dgv_card.CurrentRow.Cells[6].Value);
            command.Parameters.AddWithValue("@regDate", DateTime.Today);
            command.Parameters.AddWithValue("@userId", Program.UserID);

        
            command.ExecuteNonQuery();
            Con.Close();
            dgv_card.CurrentRow.Cells[7].Value = "Y";
            button3.Enabled = false;

            AddPayment();

            UpdateQuantityInStock();

            MessageBox.Show("Total: " + dgv_card.CurrentRow.Cells[6].Value);
        }


        private void dgv_products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            t1.Text = (string)dgv_products.CurrentRow.Cells[1].Value;
            t2.Text = (string)dgv_products.CurrentRow.Cells[2].Value;
            t3.Text = (string)dgv_products.CurrentRow.Cells[3].Value;
            t4.Text = ((double)dgv_products.CurrentRow.Cells[4].Value).ToString();
            t5.Text = ((int)dgv_products.CurrentRow.Cells[5].Value).ToString();
        }

        private void dgv_products_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            t1.Text = (string)dgv_products.CurrentRow.Cells[1].Value;
            t2.Text = (string)dgv_products.CurrentRow.Cells[2].Value;
            t3.Text = (string)dgv_products.CurrentRow.Cells[3].Value;
            t4.Text = ((double)dgv_products.CurrentRow.Cells[4].Value).ToString();
           
        }

        private void dgv_card_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string st = (string)dgv_card.CurrentRow.Cells[7].Value;
            if (st == "Y")
            {
                button3.Enabled = false;

            }

            else
            {
                button3.Enabled = true;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CustomerForm frm = new CustomerForm();
            frm.Show();
            this.Hide();
        }


    }
}
