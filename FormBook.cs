using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BookManage.Models;
using BookManage.Utils;

namespace BookManage
{
    public partial class FormBook : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        private List<书籍类别> 书籍类别集合;

        public FormBook()
        {
            InitializeComponent();

            if (GlobalVal.登录用户_类别 != "管理员")
            {
                toolStrip1.Visible = false;
            }

            LoadBookType();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            button1_Click(null, null);
        }

        void LoadBookType()
        {
            书籍类别集合= Sql.Query<Models.书籍类别>("");
            comboBox1.DataSource = null;
            var dataList = 书籍类别集合;
            dataList.Insert(0, new Models.书籍类别
            {
                类别名称 = "全部",
                类别编码 = ""
            });
            comboBox1.DataSource = dataList;
            comboBox1.DisplayMember = "类别名称";
            comboBox1.ValueMember = "类别编码";
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var dr = new FormBookAdd().ShowDialog();
            if(dr == DialogResult.OK)
            {
                MessageBox.Show("新增图书信息成功");
                button1_Click(null, null);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要修改的图书");
                return;
            }
            string code = dataGridView1.SelectedRows[0].Cells[0].Value.AsString();

            var dr = new FormBookAdd(code).ShowDialog();
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("修改图书信息成功");
                button1_Click(null, null);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的图书信息");
                return;
            }
            string code = dataGridView1.SelectedRows[0].Cells[0].Value.AsString();
            int count = Sql.Query<Models.借阅信息>("书号=@书号", new IDbDataParameter[] {
                Sql.CreateParameter("@书号",code)
            }).Count;
            if (count > 0)
            {
                MessageBox.Show("此图书已经被借出过，不允许删除");
                return;
            }

            if (MessageBox.Show("是否删除图书", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            Sql.Delete(new Models.图书
            {
                书号 = code
            });
            MessageBox.Show("删除图书信息成功");
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder s_whereClause = new StringBuilder();
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
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
            if (!string.IsNullOrEmpty(comboBox1.SelectedValue.AsString()))
            {
                s_whereClause.AppendAnd("书籍类别=@书籍类别");
                parameters.Add(Sql.CreateParameter("@书籍类别", comboBox1.SelectedValue.AsString()));
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Sql.Query<Models.图书>(s_whereClause.ToString(), parameters.ToArray());
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var data = 书籍类别集合.Find(a => a.类别编码 == e.Value.AsString());
                if (data != null)
                {
                    e.Value = data.类别名称;
                }
            }
        }
    }
}
