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
    public partial class FormChangePwd : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormChangePwd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox3.Text != GlobalVal.登录用户_密码)
            {
                MessageBox.Show("原密码输入有误");
                textBox3.Focus();
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("新密码不能为空");
                textBox1.Focus();
                return;
            }
            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("两次密码输入不一致");
                textBox2.Focus();
                return;
            }


            if (GlobalVal.登录用户_类别 == "管理员")
            {
                var data = Sql.Query<Models.图书管理员>("管理员账号=@管理员账号", new IDbDataParameter[] {
                    Sql.CreateParameter("@管理员账号",GlobalVal.登录用户_账号)
                }).FirstOrDefault();
                data.密码 = textBox1.Text;
                Sql.Edit(data);
            }
            else
            {
                var data = Sql.Query<Models.读者>("读者号=@读者号", new IDbDataParameter[] {
                    Sql.CreateParameter("@读者号",GlobalVal.登录用户_账号)
                }).FirstOrDefault();
                data.密码 = textBox1.Text;
                Sql.Edit(data);
            }
            GlobalVal.登录用户_密码 = textBox1.Text;
            MessageBox.Show("密码修改成功");
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
