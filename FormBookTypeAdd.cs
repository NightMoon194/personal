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
    public partial class FormBookTypeAdd : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormBookTypeAdd()
        {
            InitializeComponent();
        }
        public FormBookTypeAdd(string 类别编码)
        {
            InitializeComponent();
            var data = Sql.Query<Models.书籍类别>("类别编码=@类别编码", new IDbDataParameter[]
            {
                Sql.CreateParameter("@类别编码", 类别编码)
            }).FirstOrDefault();
            if (data == null)
            {
                MessageBox.Show("书籍类别不存在");
                this.Close();
                return;
            }

            textBox1.Text = data.类别编码;
            textBox2.Text = data.类别名称;
            textBox1.Enabled = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("类别编号不能为空");
                textBox1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("类别名称不能为空");
                textBox2.Focus();
                return;
            }

            Models.书籍类别 书籍类别 = new Models.书籍类别
            {
                类别编码 = textBox1.Text,
                类别名称 = textBox2.Text,
            };
            if (textBox1.Enabled)
            {
                Sql.Insert(书籍类别);
            }
            else
            {
                Sql.Edit(书籍类别);
            }

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
