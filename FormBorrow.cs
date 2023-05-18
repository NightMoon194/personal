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
    public partial class FormBorrow : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormBorrow()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            button1_Click(null, null);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var dr = new FormBorrowAdd().ShowDialog();
            if(dr == DialogResult.OK)
            {
                MessageBox.Show("借书成功");
                button1_Click(null, null);
            }
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

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Sql.Query<Models.借阅信息>(s_whereClause.ToString(), parameters.ToArray());
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的借阅信息");
                return;
            }
            int id = dataGridView1.SelectedRows[0].Cells[0].Value.AsInt();

            if (MessageBox.Show("是否删除借阅信息", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            string 书号 = dataGridView1.SelectedRows[0].Cells[3].Value.AsString();
            var 图书 = Sql.Query<Models.图书>("书号=@书号", new IDbDataParameter[] {
                    Sql.CreateParameter("@书号",书号)
                }).FirstOrDefault();
            try
            {
                Sql.BeginTransaction();
                图书.书籍状态 = "在库";
                Sql.Edit(图书);
                Sql.Delete(new Models.借阅信息
                {
                    借阅编号 = id
                });

                Sql.Commit();
            }
            catch (Exception)
            {
                Sql.Rollback();
                throw;
            }
            MessageBox.Show("删除借阅信息信息成功");
            button1_Click(null, null);
        }
    }
}
