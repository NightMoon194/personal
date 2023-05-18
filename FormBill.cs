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
    public partial class FormBill : Form
    {
        SqlFactoryUtil Sql = new SqlFactoryUtil();
        public FormBill()
        {
            InitializeComponent();



            if (GlobalVal.登录用户_类别 != "管理员")
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;

                textBox1.Text = GlobalVal.登录用户_账号;
                textBox2.Text = GlobalVal.登录用户_姓名;
            }

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                s_whereClause.AppendAnd("读者姓名 like @读者姓名");
                parameters.Add(Sql.CreateParameter("@读者姓名", "%" + textBox2.Text + "%"));
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Sql.Query<Models.罚单>(s_whereClause.ToString(), parameters.ToArray());
        }
    }
}
