using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using BookManage.Utils;
namespace BookManage.Models
{
    public abstract class baseEntity
    {
        /// <summary>
        /// 构造函数中创建表
        /// </summary>
        public baseEntity()
        {
            CreateTable(); 
        }
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            string tblName = this.GetType().Name;

            var tblAttr = this.GetType().GetCustomAttributes(false).Where(a => a is TableNameAttribute).FirstOrDefault();
            if (tblAttr != null)
            {
                tblName = (tblAttr as TableNameAttribute).Name;
            }

            return tblName;
        }

        /// <summary>
        /// 获取创建表的语句
        /// </summary>
        /// <returns></returns>
        public virtual string GetTableSql()
        {
            return "";
        }
        /// <summary>
        /// 创建表
        /// </summary>
        public void CreateTable()
        {
            string sql = GetTableSql();
            if (string.IsNullOrEmpty(sql)) return;
            var Sql = new SqlFactoryUtil();
            int count = 0;
            try
            {
                count = Sql.QueryDataSet("select top 1 * from " + GetTableName()).Tables[0].Rows.Count;
            }
            catch
            {
                count = -1;
            }
            if (count < 0)
            {
                Sql.ExecuteSql(sql);
            }

            if (count <= 0 && GetType().Name.ToUpper() == "图书管理员")
            {
                Sql.ExecuteSql("insert into dbo.["+GetTableName()+ "]([管理员账号],[密码])values(@管理员账号,@密码)", new System.Data.IDbDataParameter[] {
                    Sql.CreateParameter("@管理员账号","admin"),
                    Sql.CreateParameter("@密码","123456")
                });
            }
        }
    }
}