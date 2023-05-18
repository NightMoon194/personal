using System;
using System.Text;

namespace BookManage.Models
{
    public class 罚单 : baseEntity
    {
        [Key(Identity = true)]
        public int 罚单号 { get; set; }
        [StringLength(Length = 20)]
        public string 读者号 { get; set; }
        [StringLength(Length = 20)]
        public string 读者姓名 { get; set; }

        public decimal 罚款合计 { get; set; }

        public DateTime 办理日期 { get; set; }

        public int 借阅编号 { get; set; }
        public override string GetTableSql()
        {
            StringBuilder s_sql = new StringBuilder();
            s_sql.AppendLine("CREATE TABLE " + GetTableName() + "(");
            s_sql.AppendLine("罚单号 int identity(1,1) not null primary key,");
            s_sql.AppendLine("读者号 nvarchar(20) not null,");
            s_sql.AppendLine("读者姓名 nvarchar(20) not null,");
            s_sql.AppendLine("罚款合计 numeric(12,2) not null,");
            s_sql.AppendLine("办理日期 datetime not null,");
            s_sql.AppendLine("借阅编号 int not null");
            s_sql.AppendLine(")");

            return s_sql.ToString();
        }
    }
}
