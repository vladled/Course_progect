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
    public partial class ProductEdit : Form
    {
        int idproduct;
        int pets = 0;
        int description = 0;
        int idcust = 0;
        bool edit;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public ProductEdit()
        {
            InitializeComponent();
            edit = false;
        }
        public ProductEdit(int idproduct, int pets, int description, string name, string unit, decimal price, decimal purchasePrice, int ammount, DateTime expirationDate , int idcust)
        {
            InitializeComponent();
            edit = true;
            this.idproduct = idproduct;
            this.pets = pets;
            this.description = description;
            this.idcust = idcust;
            textBox1.Text = name;
            textBox2.Text = unit;
            textBox3.Text = price.ToString();
            textBox5.Text = ammount.ToString();
            textBox4.Text = purchasePrice.ToString();
            dateTimePicker1.Value = expirationDate;
        }
        private void ProductEdit_Load(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT idPets, Pets FROM Pets", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int temp = 0;
            foreach (DataRow x in dt.Rows)
            {
                comboBox1.Items.Add(x[0].ToString() + " " + x[1].ToString());
                if (Convert.ToInt32(x[0].ToString()) == pets)
                    temp = comboBox1.Items.Count - 1;
            }
            comboBox1.SelectedIndex = temp;
            sda = new SqlDataAdapter("SELECT idDescr, Description FROM Description", sqlconn);
            dt = new DataTable();
            sda.Fill(dt);
            temp = 0;
            foreach (DataRow x in dt.Rows)
            {
                comboBox2.Items.Add(x[0].ToString() + " " + x[1].ToString());
                if (Convert.ToInt32(x[0].ToString()) == description)
                    temp = comboBox2.Items.Count - 1;
            }
            comboBox2.SelectedIndex = temp;
            sda = new SqlDataAdapter("SELECT idCustom, NameCust FROM Custom", sqlconn);
            dt = new DataTable();
            sda.Fill(dt);
            temp = 0;
            foreach (DataRow x in dt.Rows)
            {
                comboBox3.Items.Add(x[0].ToString() + " " + x[1].ToString());
                if (Convert.ToInt32(x[0].ToString()) == idcust)
                    temp = comboBox3.Items.Count - 1;
            }
            comboBox3.SelectedIndex = temp;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int temp = 0;
            if (!edit && textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && Int32.TryParse(textBox5.Text, out temp))
            {
                {
                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand("Insert Into Product (Pets, Description, Name, Unit, Price, PurchasePrice, Ammount, ExpirationDate, idCust) " + "Values (@Pets, @Description, @Name, @Unit, @Price, @PurchasePrice, @Ammount, @ExpirationDate, @idCust)", sqlconn);

                    query.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBox1.Text;
                    query.Parameters.Add("@Pets", SqlDbType.Int).Value = Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@Description", SqlDbType.Int).Value = Convert.ToInt32(comboBox2.SelectedItem.ToString().Substring(0, comboBox2.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@idCust", SqlDbType.Int).Value = Convert.ToInt32(comboBox3.SelectedItem.ToString().Substring(0, comboBox3.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                    query.Parameters.Add("@Unit", SqlDbType.NVarChar).Value = textBox2.Text;
                    query.Parameters.Add("@Ammount", SqlDbType.Int).Value = Convert.ToInt32(textBox5.Text);
                    query.Parameters.Add("@Price", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox3.Text);
                    query.Parameters.Add("@PurchasePrice", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox4.Text);

                    query.ExecuteNonQuery();
                    sqlconn.Close();
                    this.Close();
                }
            }
            else if (edit && textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && Int32.TryParse(textBox5.Text, out temp))
            {
                {

                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand(String.Format("UPDATE Product SET Name = @Name, Pets = @Pets, Description = @Description, idCust = @idCust, ExpirationDate = ExpirationDate, Unit = @Unit, Ammount = @Ammount, Price = @Price, PurchasePrice = @PurchasePrice WHERE idProduct = {0}", idproduct.ToString()), sqlconn);

                    query.Parameters.Add("@Name", SqlDbType.NVarChar).Value = textBox1.Text;
                    query.Parameters.Add("@Pets", SqlDbType.Int).Value = Convert.ToInt32(comboBox1.SelectedItem.ToString().Substring(0, comboBox1.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@Description", SqlDbType.Int).Value = Convert.ToInt32(comboBox2.SelectedItem.ToString().Substring(0, comboBox2.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@idCust", SqlDbType.Int).Value = Convert.ToInt32(comboBox3.SelectedItem.ToString().Substring(0, comboBox3.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                    query.Parameters.Add("@Unit", SqlDbType.NVarChar).Value = textBox2.Text;
                    query.Parameters.Add("@Ammount", SqlDbType.Int).Value = Convert.ToInt32(textBox5.Text);
                    query.Parameters.Add("@Price", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox3.Text);
                    query.Parameters.Add("@PurchasePrice", SqlDbType.Decimal).Value = Convert.ToDecimal(textBox4.Text);
                    query.ExecuteNonQuery();
                    sqlconn.Close();
                    this.Close();
                }
            }
            else MessageBox.Show("Проверьте данные", "Предупреждение", MessageBoxButtons.OK);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
