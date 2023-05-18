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
    public partial class FormReader : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormReader()
        {
            InitializeComponent();

            if (GlobalVal.登录用户_类别 != "管理员")
            {
                toolStrip1.Visible = false;

                textBox1.Enabled = false;
                textBox2.Enabled = false;

                textBox1.Text = GlobalVal.登录用户_账号;
                textBox2.Text = GlobalVal.登录用户_姓名;
            }
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            button1_Click(null, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var dr = new FormReaderAdd().ShowDialog();
            if(dr == DialogResult.OK)
            {
                MessageBox.Show("新增读者信息成功");
                button1_Click(null, null);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要修改的读者");
                return;
            }
            string code = dataGridView1.SelectedRows[0].Cells[0].Value.AsString();

            var dr = new FormReaderAdd(code).ShowDialog();
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("修改读者信息成功");
                button1_Click(null, null);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的读者");
                return;
            }

            string code = dataGridView1.SelectedRows[0].Cells[0].Value.AsString();
            int count = Sql.Query<Models.借阅信息>("读者号=@读者号", new IDbDataParameter[] {
                Sql.CreateParameter("@读者号",code)
            }).Count;
            if (count > 0)
            {
                MessageBox.Show("此读者已经借过书籍，不允许删除");
                return;
            }

            if (MessageBox.Show("是否删除读者", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            Sql.Delete(new Models.读者
            {
                读者号 = code
            });
            MessageBox.Show("删除读者信息成功");
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder s_whereClause = new StringBuilder();
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                s_whereClause.AppendAnd("读者号=@读者号");
                parameters.Add(Sql.CreateParameter("@读者号", textBox1.Text));
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                s_whereClause.AppendAnd("姓名 like @姓名");
                parameters.Add(Sql.CreateParameter("@姓名", "%" + textBox2.Text + "%"));
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Sql.Query<Models.读者>(s_whereClause.ToString(), parameters.ToArray());
        }
    }
}
