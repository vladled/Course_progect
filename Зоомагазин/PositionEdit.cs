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
    public partial class PositionEdit : Form
    {
        int idPos;
        bool edit;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";
        public PositionEdit()
        {
            InitializeComponent();
            edit = false;
        }

        private void PositionEdit_Load(object sender, EventArgs e)
        {

        }
        public PositionEdit(int idPos, string Position)
        {
            InitializeComponent();
            textBox1.Text = Position;
            this.idPos = idPos;
            edit = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!edit && textBox1.Text.Trim() != "")
            {
                {
                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand("Insert Into Position (Position) " + "Values (@Position)", sqlconn);

                    query.Parameters.Add("@Position", SqlDbType.NVarChar).Value = textBox1.Text;
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
                    string quer = String.Format("Update Position Set Position = '{0}' WHERE idPos = {1}",
                        textBox1.Text, idPos.ToString());
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

