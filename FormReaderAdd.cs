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
    public partial class FormReaderAdd : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormReaderAdd()
        {
            InitializeComponent();
        }

        public FormReaderAdd(string 读者号)
        {
            InitializeComponent();
            var data = Sql.Query<Models.读者>("读者号=@读者号", new IDbDataParameter[] {
                Sql.CreateParameter("@读者号",读者号)
            }).FirstOrDefault();

            if (data == null)
            {
                MessageBox.Show("读者信息不存在");
                this.Close();
                return;
            }
            textBox1.Text = data.读者号;
            textBox2.Text = data.密码;
            textBox3.Text = data.姓名;
            radioButton1.Checked = data.性别 == "男" ? true : false;
            radioButton2.Checked = data.性别 == "女" ? true : false;
            dateTimePicker1.Value = data.登记日期;

            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("借书证编号不能为空");
                textBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("密码不能为空");
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("读者姓名不能为空");
                textBox2.Focus();
                return;
            }

            Models.读者 读者 = new Models.读者
            {
                读者号 = textBox1.Text,
                密码 = textBox2.Text,
                姓名 = textBox3.Text,
                性别 = radioButton1.Checked ? "男" : "女",
                登记日期 = dateTimePicker1.Value.Date,
            };

            if (textBox1.Enabled)
            {
                Sql.Insert(读者);
            }
            else
            {
                Sql.Edit(读者);
            }

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
