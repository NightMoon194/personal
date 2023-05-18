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
    public partial class FormMain : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        DateTime dtSysTime;
        public FormMain()
        {
            InitializeComponent();
            dtSysTime = Sql.ExecuteScalar("select getdate()").AsDateTime();
            tssl_time.Text = dtSysTime.ToString("yyyy年MM月dd日HH:mm:ss");
            tssl_lognUser.Text = GlobalVal.登录用户_账号;
            tssl_type.Text = GlobalVal.登录用户_类别;
            timer1.Enabled = true;
        }

        private void 书籍库存信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = new FormBook();
            CloseAll();
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }

        private void 书籍类别信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = new FormBookType();
            CloseAll();
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);

        }

        private void 读者信息维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = new FormReader();
            CloseAll();
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }

        private void 借书信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = new FormBorrow();
            CloseAll();
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }

        private void 还书信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = new FormBack();
            CloseAll();
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dtSysTime = dtSysTime.AddSeconds(1);
            tssl_time.Text = dtSysTime.ToString("yyyy年MM月dd日HH:mm:ss");
        }

        private void 罚单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var fm = new FormBill();
            CloseAll();
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }

        private void CloseAll()
        {
            foreach(Form fm in this.MdiChildren)
            {
                fm.Close();
            }
        }

        void FullScreen(Form fm)
        {
            fm.Dock = DockStyle.Fill;
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormChangePwd().ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
