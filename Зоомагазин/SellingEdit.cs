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
    public partial class SellingEdit : Form
    {
        int idsell;
        int idproduct = 0;
        int employee = 0;
        bool edit;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public SellingEdit()
        {
            InitializeComponent();
            edit = false;
        }
        public SellingEdit(int idsell, int idproduct, DateTime date, string time, int ammount, decimal price, int employee)
        {
            InitializeComponent();
            edit = true;
            this.idsell = idsell;
            this.idproduct = idproduct;
            this.employee = employee;
            textBox1.Text = time;
            textBox3.Text = price.ToString();
            textBox2.Text = ammount.ToString();

            dateTimePicker1.Value = date;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int ammount = 0;
            if (!edit && textBox1.Text.Trim() != "" && Int32.TryParse(textBox2.Text, out ammount))
            {
                {
                    SqlConnection sqlconn = new SqlConnection(conn);
                    //sqlconn.Open();
                    //Check avalible
                    SqlCommand query = new SqlCommand("SELECT p.ammount FROM Product p WHERE p.idproduct = @pid;", sqlconn);
                    query.Parameters.AddWithValue("@pid", comboBox1.SelectedItem.ToString().Split(' ')[0]);
                    sqlconn.Open();
                    int avalible = Convert.ToInt32(query.ExecuteScalar());
                    sqlconn.Close();
                    query = new SqlCommand("SELECT SUM(s.ammount) FROM Sellings s WHERE s.idproduct = @pid;", sqlconn);
                    query.Parameters.AddWithValue("@pid", comboBox1.SelectedItem.ToString().Split(' ')[0]);
                    sqlconn.Open();
                    string tmp = query.ExecuteScalar().ToString();
                    sqlconn.Close();
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        avalible -= int.Parse(tmp);
                    }

                    if (avalible < ammount)
                    {
                        MessageBox.Show($"Нельзя продать больше, чем есть. В наличии {avalible}", "Добавление продажи", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    //INSERT
                    /*  SqlCommand*/
                    query = new SqlCommand("Insert Into Sellings (idProduct, Date, Time, Ammount,Price, Employee) " + "Values (@idProduct, @Date, @Time, @Ammount, @Price, @Employee)", sqlconn);
                    query.Parameters.Add("@idProduct", SqlDbType.Int).Value = Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@Date", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                    query.Parameters.Add("@Time", SqlDbType.NVarChar).Value = textBox1.Text;
                    query.Parameters.Add("@Ammount", SqlDbType.Int).Value = Convert.ToInt32(textBox2.Text);
                    query.Parameters.Add("@Price", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox3.Text);
                    query.Parameters.Add("@Employee", SqlDbType.Int).Value = Convert.ToInt32(comboBox2.SelectedItem.ToString().Substring(0, comboBox2.SelectedItem.ToString().IndexOf(" ")));
                    sqlconn.Open();
                    query.ExecuteNonQuery();
                    sqlconn.Close();
                    this.Close();
                }
            }
            else if (edit && textBox1.Text.Trim() != "" && Int32.TryParse(textBox2.Text, out ammount))
            {
                {

                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand(String.Format("UPDATE Sellings SET idProduct = @idProduct, Date = @Date, Price=@Price, Time = @Time, Ammount = @Ammount, Employee = @Employee WHERE idSell = {0}", idsell.ToString()), sqlconn);
                    query.Parameters.Add("@idProduct", SqlDbType.Int).Value = Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@Date", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                    query.Parameters.Add("@Time", SqlDbType.NVarChar).Value = textBox1.Text;
                    query.Parameters.Add("@Ammount", SqlDbType.Int).Value = Convert.ToInt32(textBox2.Text);
                    query.Parameters.Add("@Employee", SqlDbType.Int).Value = Convert.ToInt32(comboBox2.SelectedItem.ToString().Substring(0, comboBox2.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@Price", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox3.Text);
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

        private void SellingEdit_Load(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT idProduct, Name FROM Product", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int temp = 0;
            foreach (DataRow x in dt.Rows)
            {
                comboBox1.Items.Add(x[0].ToString() + " " + x[1].ToString());
                if (Convert.ToInt32(x[0].ToString()) == idsell)
                    temp = comboBox1.Items.Count - 1;
            }
            comboBox1.SelectedIndex = temp;
            sda = new SqlDataAdapter("SELECT idEmp, FullName FROM Employee", sqlconn);
            dt = new DataTable();
            sda.Fill(dt);
            temp = 0;
            foreach (DataRow x in dt.Rows)
            {
                comboBox2.Items.Add(x[0].ToString() + " " + x[1].ToString());
                if (Convert.ToInt32(x[0].ToString()) == employee)
                    temp = comboBox2.Items.Count - 1;
            }
            comboBox2.SelectedIndex = temp;
        }
    }
}
