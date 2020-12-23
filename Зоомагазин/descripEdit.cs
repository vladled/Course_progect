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
    public partial class descripEdit : Form
    {
        int iddescr;
        bool edit;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public descripEdit()
        {
            InitializeComponent();
            edit = false;
        }

        public descripEdit(int iddescr, string description)
        {
            InitializeComponent();
            textBox1.Text = description;
            this.iddescr = iddescr;
            edit = true;
        }

        private void descripEdit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!edit && textBox1.Text.Trim() != "")
            {
                {
                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand("Insert Into Description (Description) " + "Values (@Description)", sqlconn);

                    query.Parameters.Add("@Description", SqlDbType.NVarChar).Value = textBox1.Text;
                    query.ExecuteNonQuery();
                    sqlconn.Close();
                    this.Close();
                }
            }
            else if (edit && textBox1.Text.Trim() != "")
            {
                {

                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand(String.Format("UPDATE Description SET Description = @Description WHERE idDescr = {0}", iddescr.ToString()), sqlconn);

                    query.Parameters.Add("@Description", SqlDbType.NVarChar).Value = textBox1.Text;
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
