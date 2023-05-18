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
    public partial class FormBorrowAdd : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        private List<书籍类别> 书籍类别集合;

        public FormBorrowAdd()
        {
            InitializeComponent();
            LoadBookType();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            button1_Click(null, null);
            button2_Click(null, null);
        }

        void LoadBookType()
        {
            comboBox1.DataSource = null;
            书籍类别集合 = Sql.Query<Models.书籍类别>("");
            书籍类别集合.Insert(0, new Models.书籍类别
            {
                类别名称 = "全部",
                类别编码 = ""
            });
            comboBox1.DataSource = 书籍类别集合;
            comboBox1.DisplayMember = "类别名称";
            comboBox1.ValueMember = "类别编码";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择读者");
                return;
            }

            if (dataGridView2.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择图书");
                return;
            }

            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("借书天数不能小于等于0");
                numericUpDown1.Focus();
                return;
            }

            try
            {
                string 书号 = dataGridView2.SelectedRows[0].Cells[0].Value.AsString();
                var 图书 = Sql.Query<Models.图书>("书号=@书号", new IDbDataParameter[] {
                    Sql.CreateParameter("@书号",书号)
                }).FirstOrDefault();

                //开启事务
                Sql.BeginTransaction();
                //插入借阅信息
                Sql.Insert(new Models.借阅信息
                {
                    书号 = dataGridView2.SelectedRows[0].Cells[0].Value.AsString(),
                    名称 = dataGridView2.SelectedRows[0].Cells[1].Value.AsString(),
                    读者号 = dataGridView1.SelectedRows[0].Cells[0].Value.AsString(),
                    读者姓名 = dataGridView1.SelectedRows[0].Cells[1].Value.AsString(),
                    借阅日期 = DateTime.Now,
                    借阅天数 = (int)numericUpDown1.Value,
                });
                //更新图书状态为借出
                图书.书籍状态 = "借出";
                Sql.Edit(图书);
                Sql.Commit();
            }
            catch (Exception)
            {
                Sql.Rollback();
                throw;
            }

            DialogResult = DialogResult.OK;
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

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder s_whereClause = new StringBuilder();
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                s_whereClause.AppendAnd("书号=@书号");
                parameters.Add(Sql.CreateParameter("@书号", textBox4.Text));
            }
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                s_whereClause.AppendAnd("名称 like @名称");
                parameters.Add(Sql.CreateParameter("@名称", "%" + textBox3.Text + "%"));
            }
            if (!string.IsNullOrEmpty(comboBox1.SelectedValue.AsString()))
            {
                s_whereClause.AppendAnd("书籍类别=@书籍类别");
                parameters.Add(Sql.CreateParameter("@书籍类别", comboBox1.SelectedValue.AsString()));
            }


            s_whereClause.AppendAnd("书籍状态=@书籍状态");
            parameters.Add(Sql.CreateParameter("@书籍状态", "在库"));

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = Sql.Query<Models.图书>(s_whereClause.ToString(), parameters.ToArray());
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
