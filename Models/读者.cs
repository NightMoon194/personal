using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookManage.Models
{
    public class 读者:baseEntity
    {
        [Key]
        [StringLength(Length = 20)]
        public string 读者号 { get; set; }
        [StringLength(Length =50)]
        public string 密码 { get; set; }
        [StringLength(Length = 20)]
        public string 姓名 { get; set; }
        [StringLength(Length = 10)]
        public string 性别 { get; set; }
        public DateTime 登记日期 { get; set; }
        
        public override string GetTableSql()
        {
            StringBuilder s_sql = new StringBuilder();
            s_sql.AppendLine("CREATE TABLE " + GetTableName() + "(");
            s_sql.AppendLine("读者号 nvarchar(20) not null primary key,");
            s_sql.AppendLine("密码 nvarchar(50) not null,");
            s_sql.AppendLine("姓名 nvarchar(20) not null,");
            s_sql.AppendLine("性别 nvarchar(10) not null,");
            s_sql.AppendLine("登记日期 DateTime not null");
            s_sql.AppendLine(")");

            return s_sql.ToString();
        }
    }
}
