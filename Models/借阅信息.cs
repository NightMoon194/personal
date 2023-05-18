using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookManage.Models
{
    public class 借阅信息:baseEntity
    {
        [Key(Identity =true)]
        public int 借阅编号 { get; set; }

        [StringLength(Length = 20)]
        public string 书号 { get; set; }
        [StringLength(Length = 100)]
        public string 名称 { get; set; }
        [StringLength(Length =20)]
        public string 读者号 { get; set; }

        [StringLength(Length = 20)]
        public string 读者姓名 { get; set; }
        
        public DateTime 借阅日期 { get; set; }
        public int 借阅天数 { get; set; }

        public DateTime? 归还日期 { get; set; }

        public override string GetTableSql()
        {
            StringBuilder s_sql = new StringBuilder();
            s_sql.AppendLine("CREATE TABLE " + GetTableName() + "(");
            s_sql.AppendLine("借阅编号 int identity(1,1) not null primary key,");
            s_sql.AppendLine("书号 nvarchar(20) not null,");
            s_sql.AppendLine("名称 nvarchar(100) not null,");
            s_sql.AppendLine("读者号 nvarchar(20) not null,");
            s_sql.AppendLine("读者姓名 nvarchar(20) not null,");
            s_sql.AppendLine("借阅日期 DateTime not null,");
            s_sql.AppendLine("借阅天数 int not null,");
            s_sql.AppendLine("归还日期 DateTime null");
            s_sql.AppendLine(")");

            return s_sql.ToString(); 
        }
    }
}
