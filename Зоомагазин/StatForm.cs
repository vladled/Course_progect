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
    public partial class StatForm : Form
    {
        string conn = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True";
        public StatForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //label1.Text = button1.Text;
            string query = "SELECT Employee.FullName,Count(Sellings.IdSell) AS Number FROM Sellings, Employee WHERE Employee.idEmp=Sellings.Employee GROUP BY Employee.idEmp,Employee.FullName";
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            using (StreamWriter x = new StreamWriter(Directory.GetCurrentDirectory() + @"\temp.txt", false))
            {
                x.WriteLine("Сотрудники, которые продали больше всего товаров");
                x.WriteLine();
                foreach (DataRow y in dt.Rows)
                {
                    x.WriteLine("ФИО продавца: " + y[0].ToString());
                    x.WriteLine("Количество продаж: " + y[1].ToString());
                    x.WriteLine();
                }
            }
        }

        private void StatForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT Suppliers.Name, Count(Custom.idCustom) AS Number FROM Custom, Suppliers WHERE Custom.idSuppliers=Suppliers.idSuppliers  GROUP BY Suppliers.idSuppliers, Suppliers.Name";
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            using (StreamWriter x = new StreamWriter(Directory.GetCurrentDirectory() + @"\temp.txt", false))
            {
                x.WriteLine("Количество поставок");
                x.WriteLine();
                foreach (DataRow y in dt.Rows)
                {
                    x.WriteLine("Поставщик: " + y[0].ToString());
                    x.WriteLine("Количество поставок: " + y[1].ToString());
                    x.WriteLine();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT Product.Name, Count(Sellings.Ammount) AS Number FROM Product, Sellings WHERE Sellings.idProduct = Product.idProduct GROUP BY Product.Name ORDER BY  Count(Sellings.Ammount) DESC";
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            using (StreamWriter x = new StreamWriter(Directory.GetCurrentDirectory() + @"\temp.txt", false))
            {
                x.WriteLine("Статистика продажи товаров");
                x.WriteLine();
                foreach (DataRow y in dt.Rows)
                {
                    x.WriteLine("Название товара: " + y[0].ToString());
                    x.WriteLine("Всего продано: " + y[1].ToString());
                    x.WriteLine();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + @"\temp.txt");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sqlconn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Zooshop.mdf;Integrated Security = True");
                sqlconn.Open();

                SqlDataAdapter oda = new SqlDataAdapter(TestInput.Text, sqlconn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlconn.Close();
            }
            catch (Exception ex) { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TestInput.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = "SELECT Product.Name, Sellings.Date,Sellings.Time, Sellings.Ammount, Sellings.Price, Sellings.Ammount*Sellings.Price AS TotalPrice From Sellings, Product Where Product.idProduct = Sellings.idProduct Group by Product.Name, Sellings.Date,Sellings.Time, Sellings.Ammount, Sellings.Price";
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            using (StreamWriter x = new StreamWriter(Directory.GetCurrentDirectory() + @"\temp.txt", false))
            {
                x.WriteLine("Чек");
                x.WriteLine();
                foreach (DataRow y in dt.Rows)
                {
                    x.WriteLine("Название товара: " + y[0].ToString());
                    x.WriteLine("Дата: " + y[1].ToString());
                    x.WriteLine("Время: " + y[2].ToString());
                    x.WriteLine("Кол-во: " + y[3].ToString());
                    x.WriteLine("Цена: " + y[4].ToString());
                    x.WriteLine("Общая цена: " + y[5].ToString());
                    x.WriteLine();
                }
            }
        }
    }
        }
    
    





