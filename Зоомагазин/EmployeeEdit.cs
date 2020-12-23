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
    public partial class EmployeeEdit : Form
    {
        int idemp;
        int position;
        bool edit;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public EmployeeEdit()
        {
            InitializeComponent();
            edit = false;
        }
        public EmployeeEdit(int idemp, int position, string fullName, string schedule, string address, string phone, decimal salary)
        {
            InitializeComponent();
            edit = true;
            this.idemp = idemp;
            this.position = position;
            textBox2.Text = fullName;
            textBox3.Text = schedule;
            textBox4.Text = address;
            textBox5.Text = phone;
            textBox6.Text = salary.ToString();
        }

        private void EmployeeEdit_Load(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT idPos, Position FROM Position", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int temp = 0;
            foreach (DataRow x in dt.Rows)
            {
                comboBox1.Items.Add(x[0].ToString() + " " + x[1].ToString());
                if (Convert.ToInt32(x[0].ToString()) == position)
                    temp = comboBox1.Items.Count - 1;
            }
            comboBox1.SelectedIndex = temp;

        }

        private void button1_Click(object sender, EventArgs e)
        { 
            if (!edit && textBox2.Text.Trim() != "" )
            {
                {
                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand("Insert Into Employee (Position, FullName, Schedule, Address, Phone, Salary) " + "Values (@Position, @FullName, @Schedule, @Address, @Phone, @Salary)", sqlconn);
                    query.Parameters.Add("@Position", SqlDbType.Int).Value = Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = textBox2.Text;
                    query.Parameters.Add("@Schedule", SqlDbType.NVarChar).Value = textBox3.Text;
                    query.Parameters.Add("@Address", SqlDbType.NVarChar).Value = textBox4.Text;
                    query.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = textBox5.Text;
                    query.Parameters.Add("@Salary", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox6.Text);
                    query.ExecuteNonQuery();
                    sqlconn.Close();
                    this.Close();
                }
            }
            else if (edit && textBox2.Text.Trim() != "" )
            {

                SqlConnection sqlconn = new SqlConnection(conn);
                sqlconn.Open();
                SqlCommand query = new SqlCommand(String.Format("UPDATE Employee SET Position = @Position, FullName = @FullName, Schedule = @Schedule, Address = @Address, Phone = @Phone, Salary=@Salary WHERE idemp = {0}", idemp.ToString()), sqlconn);

                query.Parameters.Add("@Position", SqlDbType.Int).Value = Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf(" ")));
                query.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = textBox2.Text;
                query.Parameters.Add("@Schedule", SqlDbType.NVarChar).Value = textBox3.Text;
                query.Parameters.Add("@Address", SqlDbType.NVarChar).Value = textBox4.Text;
                query.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = textBox5.Text;
                query.Parameters.Add("@Salary", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox6.Text);
                query.ExecuteNonQuery();
                sqlconn.Close();
                this.Close();
            }

            else MessageBox.Show("Проверьте данные", "Предупреждение", MessageBoxButtons.OK);
            }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }