using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Зоомагазин
{
    public partial class ProductFilter : Form
    {
        ProductForm main;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";
        public ProductFilter(ProductForm main)
        {
            InitializeComponent();
            this.main = main;
        }

        public ProductFilter()
        {
            InitializeComponent();
        }

        private void ProductFilter_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM Product WHERE ";
                int t = 0;
                if (Int32.TryParse(textBox1.Text, out t))
                    query += " Price>" + textBox1.Text + " AND ";
                if (Int32.TryParse(textBox2.Text, out t))
                    query += " Price<" + textBox2.Text + " AND ";
               
                main.Product();
                SqlConnection sqlconn = new SqlConnection(conn);
                SqlDataAdapter sda = new SqlDataAdapter(query.Substring(0, query.Length - 4), sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                main.dataGridView1.DataSource = dt;
            }
            catch (ArgumentOutOfRangeException t) { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Product", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            main.dataGridView1.DataSource = dt;
            this.Close();
        }
    }
}
