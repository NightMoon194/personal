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
    public partial class FormBookAdd : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        Models.图书 待修改的图书信息;
        public FormBookAdd()
        {
            InitializeComponent();
            LoadBookType();
        }

        public FormBookAdd(string 书号)
        {
            InitializeComponent();

            待修改的图书信息 = Sql.Query<Models.图书>("书号=@书号", new IDbDataParameter[] {
                Sql.CreateParameter("@书号",书号)
            }).FirstOrDefault();

            if (待修改的图书信息 == null)
            {
                MessageBox.Show("图书信息不存在");
                this.Close();
                return;
            }
            LoadBookType();

            textBox1.Text = 待修改的图书信息.书号;
            textBox2.Text = 待修改的图书信息.名称;
            comboBox1.SelectedValue = 待修改的图书信息.书籍类别;
            textBox3.Text = 待修改的图书信息.作者;
            textBox4.Text = 待修改的图书信息.出版社;
            dateTimePicker1.Value = 待修改的图书信息.出版日期;
            dateTimePicker2.Value = 待修改的图书信息.登记日期;
            numericUpDown1.Value = 待修改的图书信息.价格;

            textBox1.Enabled = false;
        }

        void LoadBookType()
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = Sql.Query<Models.书籍类别>("");
            comboBox1.DisplayMember = "类别名称";
            comboBox1.ValueMember = "类别编码";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("书籍编号不能为空");
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("书籍名称不能为空");
                textBox2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("书籍类别不能为空");
                comboBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("作者姓名不能为空");
                textBox3.Focus();
                return;
            }


            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("出版社不能为空");
                textBox4.Focus();
                return;
            }
            

            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("书籍价格不能小于等于0");
                numericUpDown1.Focus();
                return;
            }

            Models.图书 图书 = new Models.图书
            {
                书号 = textBox1.Text,
                名称 = textBox2.Text,
                书籍类别 = comboBox1.SelectedValue.ToString(),
                作者 = textBox3.Text,
                出版社 = textBox4.Text,
                出版日期 = dateTimePicker1.Value.Date,
                登记日期 = dateTimePicker2.Value.Date,
                价格 = numericUpDown1.Value,
                书籍状态 = "在库"
            };

            if (待修改的图书信息 != null)
            {
                图书.书籍状态 = 待修改的图书信息.书籍状态;
            }

            if (textBox1.Enabled)
            {
                Sql.Insert(图书);
            }
            else
            {
                Sql.Edit(图书);
            }

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
