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
    public partial class SuppEdit : Form
    {
        int idsuppliers;
        bool edit;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public SuppEdit()
        {
            InitializeComponent();
            edit = false;
        }
        public SuppEdit(int idsuppliers, string name, string address, string phone)
        {
            InitializeComponent();
            textBox1.Text = name;
            textBox2.Text = address;
            textBox3.Text = phone;
            this.idsuppliers = idsuppliers;
            edit = true;
        }
        private void SuppEdit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int temp = 0;
            if (!edit && textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "")
            {
                {
                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand("Insert Into Suppliers (Name, Address, Phone) " + "Values (@Name, @Address, @Phone)", sqlconn);

                    query.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBox1.Text;
                    query.Parameters.Add("@Address", SqlDbType.NVarChar).Value = textBox2.Text;
                    query.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = textBox3.Text;
                    query.ExecuteNonQuery();
                    sqlconn.Close();
                    this.Close();
                }
            }
            else if (edit && textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "")
            {
                {

                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    string quer = String.Format("Update Suppliers Set Name = '{0}', Address = '{1}', Phone = {2} WHERE idSuppliers = {3}",
                    textBox1.Text, textBox2.Text, textBox3.Text, idsuppliers.ToString());
                    SqlCommand query = new SqlCommand(quer, sqlconn);
                    query.ExecuteNonQuery();
                    sqlconn.Close();
                    this.Close();
                }
            }
            else MessageBox.Show("Проверьте данные", "Предупреждение", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
