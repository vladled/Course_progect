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
    public partial class PetsEdit : Form
    {
        int idPets;
        bool edit;
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public PetsEdit()

        {
            InitializeComponent();
            edit = false;
        }
        public PetsEdit(int idPets, string Pets)
        {
            InitializeComponent();
            textBox1.Text = Pets;
            this.idPets = idPets;
            edit = true;
        }

       private void PetsEdit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!edit && textBox1.Text.Trim() != "")
            {
                {
                    SqlConnection sqlconn = new SqlConnection(conn);
                    sqlconn.Open();
                    SqlCommand query = new SqlCommand("Insert Into Pets (Pets) " + "Values (@Pets)", sqlconn);

                    query.Parameters.Add("@Pets", SqlDbType.NVarChar).Value = textBox1.Text;
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
                    string quer = String.Format("Update Pets Set Pets = '{0}' WHERE IdPets = {1}",
                        textBox1.Text, idPets.ToString());
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