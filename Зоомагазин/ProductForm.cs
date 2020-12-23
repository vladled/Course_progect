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
    public partial class ProductForm : Form
    {

        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "zooshopDataSet.Product". При необходимости она может быть перемещена или удалена.
           
            Description();
        }


        public void Description()
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Description", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label1.Text = "Тип товара";
            dataGridView1.DataSource = dt;
            searchText.Enabled = true;
            button1.Enabled = true;
            button1.Text = "Искать по названию";
            button2.Text = "Сортировать по названию";
            button4.Visible = false;
            button3.Visible = false;
        }
        public void Pets()
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Pets", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label1.Text = "Животное";
            dataGridView1.DataSource = dt;
            searchText.Enabled = true;
            button1.Enabled = true;
            button1.Text = "Искать по названию";
            button2.Text = "Сортировать по названию";
            button4.Visible = false;
            button3.Visible = false;
        }

         public void Product()
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Product", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label1.Text = "Товары";
            dataGridView1.DataSource = dt;
            searchText.Enabled = true;
            button1.Enabled = true;
            button1.Text = "Искать по названию";
            button2.Text = "Сортировать по названию";
            button3.Text = "Сортировать по количеству";
            button4.Text = "Сортировать по цене";
            button4.Visible = true;
            button3.Visible = true;
        }
       
       

       

        private void button1_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Тип товара"):
                    {
                        try
                        {
                            string selectString =
                            "Description Like '" + searchText.Text.Trim() + "%'";

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
                            MessageBox.Show("Такой тип не найден", "Предупреждение", MessageBoxButtons.OK);
                        }
                        break;
                    }
                case ("Животное"):
                    {
                        try
                        {

                            string selectString =
                            "Pets Like '" + searchText.Text.Trim() + "%'";

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
                            MessageBox.Show("Такого животного не найдено", "Предупреждение", MessageBoxButtons.OK);
                        }
                        break;
                    }
                case ("Товары"):
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
                            MessageBox.Show("Такого товара не найдено", "Предупреждение", MessageBoxButtons.OK);
                        }
                        break;
                    }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Тип товара"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Description ORDER BY Description;", sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    }
                case ("Животное"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Pets ORDER BY Pets;", sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    }
                case ("Товары"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Product ORDER BY Name;", sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                switch (label1.Text)
                {
                    case ("Товары"):
                        {
                            SqlConnection sqlconn = new SqlConnection(conn);
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Product ORDER BY Ammount;", sqlconn);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                            break;
                        }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            {
                switch (label1.Text)
                {
                    case ("Продукты"):
                        {
                            SqlConnection sqlconn = new SqlConnection(conn);
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Product ORDER BY Price;", sqlconn);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                            break;
                        }
                }
            }
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void таблицыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

   

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Тип товара"):
                    {
                        var ep = new descripEdit();
                        ep.ShowDialog();
                        Description();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Животное"):
                    {
                        var ep = new PetsEdit();
                        ep.ShowDialog();
                        Pets();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Товары"):
                    {
                        var ep = new ProductEdit();
                        ep.ShowDialog();
                        Product();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }

            }
        }

        private void животныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pets();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Тип товара"):
                    {
                        DataGridViewRow temp = dataGridView1.SelectedRows[0];
                        var ep = new descripEdit(Convert.ToInt32(temp.Cells[0].Value),
                            Convert.ToString(temp.Cells[1].Value));
                        ep.ShowDialog();
                        Description();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Животное"):
                    {
                        DataGridViewRow temp = dataGridView1.SelectedRows[0];
                        var ep = new PetsEdit(Convert.ToInt32(temp.Cells[0].Value),
                            Convert.ToString(temp.Cells[1].Value));
                        ep.ShowDialog();
                        Pets();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Товары"):
                    {
                        DataGridViewRow temp = dataGridView1.SelectedRows[0];
                        var ep = new ProductEdit(Convert.ToInt32(temp.Cells[0].Value),
                            Convert.ToInt32(temp.Cells[1].Value),
                            Convert.ToInt32(temp.Cells[2].Value),
                            Convert.ToString(temp.Cells[3].Value),
                            Convert.ToString(temp.Cells[4].Value),
                            Convert.ToDecimal(temp.Cells[5].Value),
                            Convert.ToDecimal(temp.Cells[6].Value),
                            Convert.ToInt32(temp.Cells[7].Value),
                            Convert.ToDateTime(temp.Cells[8].Value),
                            Convert.ToInt32(temp.Cells[9].Value));

                        ep.ShowDialog();
                        Product();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Тип товара"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        sqlconn.Open();
                        SqlCommand query = new SqlCommand("Delete From Description Where idDescr =" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), sqlconn);
                        query.ExecuteNonQuery();
                        sqlconn.Close();
                        Description();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Животное"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        sqlconn.Open();
                        SqlCommand query = new SqlCommand("Delete From Pets Where IdPets =" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), sqlconn);
                        query.ExecuteNonQuery();
                        sqlconn.Close();
                        Pets();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Товары"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        sqlconn.Open();
                        SqlCommand query = new SqlCommand("Delete From Product Where idProduct =" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), sqlconn);
                        query.ExecuteNonQuery();
                        sqlconn.Close();
                        Product();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void товарыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product();
        }

        private void типТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Description();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var s = new ProductFilter(this);
            s.Show();
        }
    }
    }
