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
    public partial class EmployeeForm : Form
    {
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";

        public EmployeeForm()
        {

            InitializeComponent();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "zooshopDataSet.Position". При необходимости она может быть перемещена или удалена.
           this.positionTableAdapter.Fill(this.zooshopDataSet.Position);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "zooshopDataSet.Employee". При необходимости она может быть перемещена или удалена.
          this.employeeTableAdapter.Fill(this.zooshopDataSet.Employee);
           dataGridView1.AutoGenerateColumns = true;
            Position();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee();
        }

        private void должностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Position();
        }

        public void Employee()
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Employee", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label1.Text = "Сотрудники";
            dataGridView1.DataSource = dt;
            searchText.Enabled = true;
            button1.Enabled = true;
            button1.Text = "Искать по Фамилии";
            button2.Text = "Сортировать по зарплате";
            button2.Visible = true;
        }
        public void Position()
        {
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Position", sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            label1.Text = "Должность";
            dataGridView1.DataSource = dt;
            searchText.Enabled = true;
            button1.Enabled = true;
            button1.Text = "Искать по названию";
            button2.Visible = false;

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Сотрудники"):
                    {
                        var ep = new EmployeeEdit();
                        ep.ShowDialog();
                        Employee();
                      zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Должность"):
                    {
                        var ep = new PositionEdit();
                        ep.ShowDialog();
                        Position();
                       zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Сотрудники"):
                    {
                        DataGridViewRow temp = dataGridView1.SelectedRows[0];
                        var ep = new EmployeeEdit(Convert.ToInt32(temp.Cells[0].Value),
                            Convert.ToInt32(temp.Cells[1].Value),
                            Convert.ToString(temp.Cells[2].Value),
                            Convert.ToString(temp.Cells[3].Value),
                            Convert.ToString(temp.Cells[4].Value),
                            Convert.ToString(temp.Cells[5].Value),
                            Convert.ToDecimal(temp.Cells[6].Value));
                        ep.ShowDialog();
                        Employee();
                       zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Должность"):
                    {
                        DataGridViewRow temp = dataGridView1.SelectedRows[0];
                        var ep = new PositionEdit(Convert.ToInt32(temp.Cells[0].Value),
                            Convert.ToString(temp.Cells[1].Value));
                        ep.ShowDialog();
                        Position();
                      zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Сотрудники"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        sqlconn.Open();
                        SqlCommand query = new SqlCommand("Delete From Employee Where idEmp =" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), sqlconn);
                        query.ExecuteNonQuery();
                        sqlconn.Close();
                        Employee();
                        zooshopDataSet.AcceptChanges();
                        break;
                    }
                case ("Должность"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        sqlconn.Open();
                        SqlCommand query = new SqlCommand("Delete From Position Where idPos =" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), sqlconn);
                        query.ExecuteNonQuery();
                        sqlconn.Close();
                        Position();
                       zooshopDataSet.AcceptChanges();
                        break;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Сотрудники"):
                    {
                        try
                        {
                            string selectString =
                            "FullName Like '" + searchText.Text.Trim() + "%'";

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
                case ("Должность"):
                    {
                        try
                        {

                            string selectString =
                            "Position Like '" + searchText.Text.Trim() + "%'";

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
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (label1.Text)
            {
                case ("Сотрудники"):
                    {
                        SqlConnection sqlconn = new SqlConnection(conn);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Employee ORDER BY Salary;", sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                        break;
                    }
            }
        }
    }
}