using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookManage.Utils;
namespace BookManage
{
    public partial class FormDelay : Form
    {
        public decimal 罚单合计 { get; private set; }
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormDelay(int days)
        {
            InitializeComponent();

            textBox1.Text = days.ToString();
            textBox1.Enabled = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("罚单合计不能小于等于0");
                numericUpDown1.Focus();
                return;
            }
            罚单合计 = numericUpDown1.Value;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
