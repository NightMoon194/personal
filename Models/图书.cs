using System;
using System.Text;

namespace BookManage.Models
{
    public class 图书 : baseEntity
    {
        [Key]
        [StringLength(Length = 20)]
        public string 书号 { get; set; }
        [StringLength(Length = 100)]
        public string 名称 { get; set; }
        [StringLength(Length = 200)]
        public string 出版社 { get; set; }

        public decimal 价格 { get; set; }

        [StringLength(Length = 50)]
        public string 作者 { get; set; }

        public DateTime 出版日期 { get; set; }
        public DateTime 登记日期 { get; set; }
        /// <summary>
        /// 在库、借出
        /// </summary>
        [StringLength(Length = 20)]
        public string 书籍状态 { get; set; }
        [StringLength(Length = 20)]
        public string 书籍类别 { get; set; }
        public override string GetTableSql()
        {
            StringBuilder s_sql = new StringBuilder();
            s_sql.AppendLine("CREATE TABLE " + GetTableName() + "(");
            s_sql.AppendLine("书号 nvarchar(20) not null primary key,");
            s_sql.AppendLine("名称 nvarchar(100) not null,");
            s_sql.AppendLine("出版社 nvarchar(200) not null,");
            s_sql.AppendLine("价格 numeric(12,2) not null,");
            s_sql.AppendLine("作者 nvarchar(50) not null,");
            s_sql.AppendLine("出版日期 datetime not null,");
            s_sql.AppendLine("登记日期 datetime not null,");
            s_sql.AppendLine("书籍状态 nvarchar(20) not null,");
            s_sql.AppendLine("书籍类别 nvarchar(20) not null");
            s_sql.AppendLine(")");

            return s_sql.ToString();
        }
    }
}
