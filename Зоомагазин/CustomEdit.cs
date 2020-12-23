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
    public partial class CustomEdit : Form
    {

        int idcustom;
        int idsuppliers;
        bool edit;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";



        public CustomEdit()
        {
            InitializeComponent();
            edit = false;
        }
        public CustomEdit(int idcustom, string name, int idsuppliers, string address, DateTime data, string time)
        {
            InitializeComponent();
            edit = true;
            this.idcustom = idcustom;
            this.idsuppliers = idsuppliers;
            textBox1.Text = name;
            textBox2.Text = address;
            textBox3.Text = time;
            dateTimePicker1.Value = data;
        }

        private void CustomEdit_Load(object sender, EventArgs e)
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT idSuppliers, Name FROM Suppliers", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int temp = 0;
            foreach (DataRow x in dt.Rows)
            {
                listBox1.Items.Add(x[0].ToString() + " " + x[1].ToString());
                if (Convert.ToInt32(x[0].ToString()) == idsuppliers)
                    temp = listBox1.Items.Count - 1;
            }
            listBox1.SelectedIndex = temp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int temp = 0;
            if (!edit && textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "")
            {

                {
                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand("Insert Into Custom (NameCust,idSuppliers,Address,Data,Time)" +
                        "Values (@NameCust,@idSuppliers,@Address,@Data,@Time)", sqlconn);

                    query.Parameters.Add("@idSuppliers", SqlDbType.Int).Value = Convert.ToInt32(listBox1.SelectedItem.ToString().Substring(0, listBox1.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@NameCust", SqlDbType.NVarChar).Value = textBox1.Text;
                    query.Parameters.Add("@Address", SqlDbType.NVarChar).Value = textBox2.Text;
                    query.Parameters.Add("@Time", SqlDbType.NVarChar).Value = textBox3.Text;
                    query.Parameters.Add("@Data", SqlDbType.DateTime).Value = dateTimePicker1.Value;

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
                    SqlCommand query = new SqlCommand(String.Format("UPDATE Custom SET idSuppliers = @idSuppliers, NameCust = @NameCust, Address= @Address, Data = @Data, Time = @Time WHERE idCustom = {0}", idcustom.ToString()), sqlconn);

                    query.Parameters.Add("@idSuppliers", SqlDbType.Int).Value = Convert.ToInt32(listBox1.SelectedItem.ToString().Substring(0, listBox1.SelectedItem.ToString().IndexOf(" ")));
                    query.Parameters.Add("@NameCust", SqlDbType.NVarChar).Value = textBox1.Text;
                    query.Parameters.Add("@Address", SqlDbType.NVarChar).Value = textBox2.Text;
                    query.Parameters.Add("@Time", SqlDbType.NVarChar).Value = textBox3.Text;
                    query.Parameters.Add("@Data", SqlDbType.DateTime).Value = dateTimePicker1.Value;

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
