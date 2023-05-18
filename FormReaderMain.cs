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
    public partial class FormReaderMain : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        DateTime dtSysTime;
        public FormReaderMain()
        {
            InitializeComponent();
            dtSysTime = Sql.ExecuteScalar("select getdate()").AsDateTime();
            tssl_time.Text = dtSysTime.ToString("yyyy年MM月dd日HH:mm:ss");
            tssl_lognUser.Text = GlobalVal.登录用户_姓名;
            tssl_type.Text = GlobalVal.登录用户_类别;
            timer1.Enabled = true;
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


        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormChangePwd().ShowDialog();
        }

        private void 图书信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = new FormBook();
            CloseAll();
            fm.Text = "图书信息查询";
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }

        private void 个人借阅查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var fm = new FormBack();
            CloseAll();
            fm.Text = "个人借阅查询";
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }

        private void 个人罚单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var fm = new FormBill();
            CloseAll();
            fm.Text = "个人罚单查询";
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }

        private void 个人资料查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var fm = new FormReader();
            CloseAll();
            fm.Text = "个人资料查询";
            fm.MdiParent = this;
            fm.Show();
            FullScreen(fm);
        }


        private void CloseAll()
        {
            foreach (Form fm in this.MdiChildren)
            {
                fm.Close();
            }
        }

        void FullScreen(Form fm)
        {
            fm.Dock = DockStyle.Fill;
        }

    }
}
