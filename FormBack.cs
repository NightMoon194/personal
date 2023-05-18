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
    public partial class FormBack : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormBack()
        {
            InitializeComponent();


            if (GlobalVal.登录用户_类别 != "管理员")
            {
                toolStrip1.Visible = false;
                textBox4.Enabled = false;
                textBox3.Enabled = false;

                textBox4.Text = GlobalVal.登录用户_账号;
                textBox3.Text = GlobalVal.登录用户_姓名;
            }

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            button1_Click(null, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要归还的记录");
                return;
            }

            if (MessageBox.Show("是否归还", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            int id = dataGridView1.SelectedRows[0].Cells[0].Value.AsInt();
            string 书号 = dataGridView1.SelectedRows[0].Cells[3].Value.AsString();
            var 图书 = Sql.Query<Models.图书>("书号=@书号", new IDbDataParameter[] {
                    Sql.CreateParameter("@书号",书号)
                }).FirstOrDefault();
            var 借阅信息 = Sql.Query<Models.借阅信息>("借阅编号=@借阅编号", new IDbDataParameter[] {
                    Sql.CreateParameter("@借阅编号",id)
                }).FirstOrDefault();

            if (借阅信息.归还日期 != null)
            {
                MessageBox.Show("图书已归还不允许重复归还，请刷新界面查看");
                return;
            }
            DateTime dtNow = DateTime.Now;
            Models.罚单 罚单 = null; 
            借阅信息.归还日期 = dtNow;
            图书.书籍状态 = "在库";
            DateTime dtNew = 借阅信息.借阅日期.AddDays(借阅信息.借阅天数);
            if (dtNew < dtNow)
            {
                int days = new TimeSpan(dtNow.Ticks).Subtract(new TimeSpan(dtNew.Ticks)).Days;

                FormDelay form = new FormDelay(days);
                if(form.ShowDialog()!= DialogResult.OK)
                {
                    return;
                }
                罚单 = new Models.罚单
                {
                    借阅编号 = 借阅信息.借阅编号,
                    办理日期 = dtNow,
                    读者号 = 借阅信息.读者号,
                    读者姓名 = 借阅信息.读者姓名,
                };
                罚单.罚款合计 = form.罚单合计;
            }


            try
            {
                Sql.BeginTransaction();
                Sql.Edit(借阅信息);
                Sql.Edit(图书);
                if (罚单 != null)
                    Sql.Insert(罚单);
                Sql.Commit();
            }
            catch (Exception)
            {
                Sql.Rollback();
                throw;
            }

            MessageBox.Show("还书成功");
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder s_whereClause = new StringBuilder();
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                s_whereClause.AppendAnd("读者号=@读者号");
                parameters.Add(Sql.CreateParameter("@读者号", textBox4.Text));
            }
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                s_whereClause.AppendAnd("读者姓名 like @读者姓名");
                parameters.Add(Sql.CreateParameter("@读者姓名", "%" + textBox3.Text + "%"));
            }

            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                s_whereClause.AppendAnd("书号=@书号");
                parameters.Add(Sql.CreateParameter("@书号", textBox1.Text));
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                s_whereClause.AppendAnd("名称 like @名称");
                parameters.Add(Sql.CreateParameter("@名称", "%" + textBox2.Text + "%"));

            }

            if (checkBox1.Checked)
            {
                s_whereClause.AppendAnd("归还日期 is not null");
            }
            else
            {
                s_whereClause.AppendAnd("归还日期 is null");
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Sql.Query<Models.借阅信息>(s_whereClause.ToString(), parameters.ToArray());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要取消归还的记录");
                return;
            }

            if (MessageBox.Show("是否取消归还", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            int id = dataGridView1.SelectedRows[0].Cells[0].Value.AsInt();
            string 书号 = dataGridView1.SelectedRows[0].Cells[3].Value.AsString();
            var 图书 = Sql.Query<Models.图书>("书号=@书号", new IDbDataParameter[] {
                    Sql.CreateParameter("@书号",书号)
                }).FirstOrDefault();

            if (图书.书籍状态 != "在库")
            {
                MessageBox.Show("图书已被借出，无法取消归还");
                return;
            }

            var 借阅信息 = Sql.Query<Models.借阅信息>("借阅编号=@借阅编号", new IDbDataParameter[] {
                    Sql.CreateParameter("@借阅编号",id)
                }).FirstOrDefault();

            if (借阅信息.归还日期 == null)
            {
                MessageBox.Show("图书还未归还，无需取消");
                return;
            }
            借阅信息.归还日期 = null;
            图书.书籍状态 = "借出";
            Models.罚单 罚单 = Sql.Query<Models.罚单>("借阅编号=@借阅编号", new IDbDataParameter[] {
                Sql.CreateParameter("@借阅编号",id)
            }).FirstOrDefault();


            try
            {

                Sql.BeginTransaction();
                Sql.Edit(借阅信息);
                Sql.Edit(图书);
                if (罚单 != null)
                    Sql.Delete(罚单);
                Sql.Commit();
            }
            catch (Exception)
            {
                Sql.Rollback();
                throw;
            }

            MessageBox.Show("取消还书成功");
            button1_Click(null, null);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}
