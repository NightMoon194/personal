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
    public partial class FormBookType : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormBookType()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var dr = new FormBookTypeAdd().ShowDialog();
            if(dr == DialogResult.OK)
            {
                MessageBox.Show("新增书籍类别成功");
                button1_Click(null, null);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要修改的书籍类别");
                return;
            }
            string code = dataGridView1.SelectedRows[0].Cells[0].Value.AsString() ;

            var dr = new FormBookTypeAdd(code).ShowDialog();
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("修改书籍类别成功");
                button1_Click(null, null);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的书籍类别");
                return;
            }

            string code = dataGridView1.SelectedRows[0].Cells[0].Value.AsString();

            int count = Sql.Query<Models.图书>("书籍类别=@书籍类别", new IDbDataParameter[] {
                Sql.CreateParameter("@书籍类别",code)
            }).Count;
            if (count > 0)
            {
                MessageBox.Show("已有书籍使用了此类别，不允许删除");
                return;
            }
            if (MessageBox.Show("是否删除书籍类别", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            Sql.Delete(new Models.书籍类别
            {
                类别编码 = code
            });
            MessageBox.Show("删除书籍类别成功");
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder s_whereClause = new StringBuilder();
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                s_whereClause.AppendAnd("类别编码=@类别编码");
                parameters.Add(Sql.CreateParameter("@类别编码", textBox1.Text));
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                s_whereClause.AppendAnd("类别名称 like @类别名称");
                parameters.Add(Sql.CreateParameter("@类别名称", "%" + textBox2.Text + "%"));
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Sql.Query<Models.书籍类别>(s_whereClause.ToString(), parameters.ToArray());
        }

        private void FormBookType_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }
    }
}
