using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Зоомагазин
{
    public partial class MainForn : Form
    {
        public MainForn()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var s = new SellingForm();
            s.Show();
        }

        private void MainForn_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = new ProductForm();
            s.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var s = new EmployeeForm();
            s.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var s = new CustomForm();
            s.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var s = new StatForm();
            s.Show();
        }

      
    }     
        }

