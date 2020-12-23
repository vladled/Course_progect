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
using System.IO;

namespace Зоомагазин
{
    public partial class CustomForm : Form
    {
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public CustomForm()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CustomForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "zooshopDataSet.Custom". При необходимости она может быть перемещена или удалена.
            this.customTableAdapter.Fill(this.zooshopDataSet.Custom);
            dataGridView1.AutoGenerateColumns = true;
            Suppliers();
        }
        public void Custom()
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Custom", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label1.Text = "Заказы";
            dataGridView1.DataSource = dt;
            searchText.Enabled = true;
            button1.Enabled = true;
            button1.Text = "Искать по названию";
            button2.Text = "Сортировать по дате";
        }
        public void Suppliers()
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Suppliers", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label1.Text = "Поставщики";
            dataGridView1.DataSource = dt;
            searchText.Enabled = true;
            button1.Enabled = true;
            button1.Text = "Искать по названию";
            button2.Text = "Сортировать по названию";
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Заказы"):
                    {
                        var ep = new CustomEdit();
                        ep.ShowDialog();
                        Custom();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Поставщики"):
                    {
                        var ep = new SuppEdit();
                        ep.ShowDialog();
                        Suppliers();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }
        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Custom();
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Suppliers();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Заказы"):
                    {
                        DataGridViewRow temp = dataGridView1.SelectedRows[0];
                        var ep = new CustomEdit(Convert.ToInt32(temp.Cells[0].Value),
                            Convert.ToString(temp.Cells[1].Value),
                            Convert.ToInt32(temp.Cells[2].Value),
                            Convert.ToString(temp.Cells[3].Value),
                            Convert.ToDateTime(temp.Cells[4].Value),
                            Convert.ToString(temp.Cells[5].Value));
                        ep.ShowDialog();
                        Custom();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Поставщики"):
                    {
                        DataGridViewRow temp = dataGridView1.SelectedRows[0];
                        var ep = new SuppEdit(Convert.ToInt32(temp.Cells[0].Value),
                            Convert.ToString(temp.Cells[1].Value),
                            Convert.ToString(temp.Cells[2].Value),
                            Convert.ToString(temp.Cells[3].Value));
                        ep.ShowDialog();
                        Suppliers();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Заказы"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        sqlconn.Open();
                        SqlCommand query = new SqlCommand("Delete From Custom Where idCustom =" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), sqlconn);
                        query.ExecuteNonQuery();
                        sqlconn.Close();
                        Custom();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Поставщики"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        sqlconn.Open();
                        SqlCommand query = new SqlCommand("Delete From Suppliers Where idSuppliers =" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), sqlconn);
                        query.ExecuteNonQuery();
                        sqlconn.Close();
                        Suppliers();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Заказы"):
                    {
                        try
                        {
                            string selectString =
                            "NameCust Like '" + searchText.Text.Trim() + "%'";

                            DataRowCollection allRows =
                            ((DataTable)dataGridView1.DataSource).Rows;

                            DataRow[] searchedRows =
                            ((DataTable)dataGridView1.DataSource).
                            Select(selectString);

                            int rowIndex = allRows.IndexOf(searchedRows[0]);

                            dataGridView1.CurrentCell =
                            dataGridView1[0, rowIndex];
                        }
                        catch (IndexOutOfRangeException t)
                        {
                            MessageBox.Show("Такой названия не найден", "Предупреждение", MessageBoxButtons.OK);
                        }
                        break;
                    }
                case ("Поставщики"):
                    {
                        try
                        {

                            string selectString =
                            "Name Like '" + searchText.Text.Trim() + "%'";

                            DataRowCollection allRows =
                            ((DataTable)dataGridView1.DataSource).Rows;

                            DataRow[] searchedRows =
                            ((DataTable)dataGridView1.DataSource).
                            Select(selectString);

                            int rowIndex = allRows.IndexOf(searchedRows[0]);

                            dataGridView1.CurrentCell =
                            dataGridView1[0, rowIndex];
                        }
                        catch (IndexOutOfRangeException t)
                        {
                            MessageBox.Show("Такого поставщика не найдено", "Предупреждение", MessageBoxButtons.OK);
                        }
                        break;
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Заказы"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Custom ORDER BY Data;", sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    }
                case ("Поставщики"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Suppliers ORDER BY Name;", sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    }
            }
        }

        private void загрузитьПоставщиковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog od = new OpenFileDialog();
                if (od.ShowDialog() != DialogResult.Cancel)
                {
                    using (StreamReader sr = new StreamReader(od.FileName, Encoding.GetEncoding("windows-1251")))
                    {
                        while (!sr.EndOfStream)
                        {
                            string[] temp = sr.ReadLine().Split();
                            try
                            {
                                SqlConnection sqlconn = new SqlConnection(conn);
                                sqlconn.Open();
                                SqlCommand query = new SqlCommand("Insert Into Suppliers (Name, Address, Phone) " + "Values (@Name, @Address, @Phone)", sqlconn);

                                query.Parameters.Add("@Name", SqlDbType.NVarChar).Value = temp[0];
                                query.Parameters.Add("@Address", SqlDbType.NVarChar).Value = temp[1];
                                query.Parameters.Add("@Phone", SqlDbType.Int).Value = Convert.ToInt32(temp[2]);
                                query.ExecuteNonQuery();
                                sqlconn.Close();
                            }
                            catch (Exception ex) { }
                        }
                    }
                    zooshopDataSet.AcceptChanges();
                    Suppliers();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}