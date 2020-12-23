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
    public partial class SellingForm : Form
    {
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public SellingForm()
        {
            InitializeComponent();
        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "zooshopDataSet.Selling". При необходимости она может быть перемещена или удалена.
            
            
            Selling();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void Selling()
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * From Sellings", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label1.Text = "Продажи";
            dataGridView1.DataSource = dt;
            button2.Text = "Сортировать по дате";
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Продажи"):
                    {
                        var ep = new SellingEdit();
                        ep.ShowDialog();
                        Selling();
                       zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Продажи"):
                    {
                        DataGridViewRow temp = dataGridView1.SelectedRows[0];
                        var ep = new SellingEdit(Convert.ToInt32(temp.Cells[0].Value),
                            Convert.ToInt32(temp.Cells[1].Value),
                            Convert.ToDateTime(temp.Cells[2].Value),
                            Convert.ToString(temp.Cells[3].Value),
                            Convert.ToInt32(temp.Cells[4].Value),
                            Convert.ToDecimal(temp.Cells[5].Value),
                            Convert.ToInt32(temp.Cells[6].Value));
                        ep.ShowDialog();
                        Selling();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Продажи"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        sqlconn.Open();
                        SqlCommand query = new SqlCommand("Delete From Sellings Where idSell =" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), sqlconn);
                        query.ExecuteNonQuery();
                        sqlconn.Close();
                        Selling();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Продажи"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Sellings ORDER BY Date;", sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    }
            }
        }

       
    }
}
