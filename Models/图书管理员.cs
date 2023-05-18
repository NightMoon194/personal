using System.Text;

namespace BookManage.Models
{
    public class 图书管理员 : baseEntity
    {
        [Key]
        [StringLength(Length = 20)]
        public string 管理员账号 { get; set; }

        [StringLength(Length = 50)]
        public string 密码 { get; set; }
        public override string GetTableSql()
        {
            StringBuilder s_sql = new StringBuilder();
            s_sql.AppendLine("CREATE TABLE " + GetTableName() + "(");
            s_sql.AppendLine("管理员账号 nvarchar(20) not null primary key,");
            s_sql.AppendLine("密码 nvarchar(50) not null");
            s_sql.AppendLine(")");

            return s_sql.ToString();
        }
    }
}
