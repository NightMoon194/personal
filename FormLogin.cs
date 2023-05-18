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
    public partial class FormLogin : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

                var data = Sql.Query<Models.图书管理员>("管理员账号=@管理员账号", new IDbDataParameter[] {
                    Sql.CreateParameter("@管理员账号",textBox1.Text)
                }).FirstOrDefault();
                if (data == null)
                {
                    MessageBox.Show("管理员信息不存在");
                    return;
                }
                if (data.密码 != textBox2.Text)
                {
                    MessageBox.Show("管理员密码输入有误");
                    return;
                }

                GlobalVal.登录用户_账号 = data.管理员账号;
                GlobalVal.登录用户_类别 = radioButton1.Text;
                GlobalVal.登录用户_密码 = data.密码;
                var fm = new FormMain();
                fm.FormClosed += Fm_FormClosed;
                fm.Show();
                this.Visible = false;
            }
            else
            {
                var data = Sql.Query<Models.读者>("读者号=@读者号", new IDbDataParameter[] {
                    Sql.CreateParameter("@读者号",textBox1.Text)
                }).FirstOrDefault();
                if (data == null)
                {
                    MessageBox.Show("读者信息不存在");
                    return;
                }
                if (data.密码 != textBox2.Text)
                {
                    MessageBox.Show("读者密码输入有误");
                    return;
                }


                GlobalVal.登录用户_账号 = data.读者号;
                GlobalVal.登录用户_姓名 = data.姓名;
                GlobalVal.登录用户_类别 = radioButton2.Text;
                GlobalVal.登录用户_密码 = data.密码;
                var fm = new FormReaderMain();
                fm.FormClosed += Fm_FormClosed;
                fm.Show();
                this.Visible = false;
            }

        }

        private void Fm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            new Models.书籍类别();
            new Models.图书();
            new Models.图书管理员();
            new Models.读者();
            new Models.借阅信息();
            new Models.罚单();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
