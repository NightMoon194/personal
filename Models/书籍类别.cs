using System.Text;

namespace BookManage.Models
{
    public class 书籍类别:baseEntity
    {
        [Key]
        [StringLength(Length = 20)]
        public string 类别编码 { get; set; }
        [StringLength(Length = 50)]
        public string 类别名称 { get; set; }
        public override string GetTableSql()
        {
            StringBuilder s_sql = new StringBuilder();
            s_sql.AppendLine("CREATE TABLE " + GetTableName() + "(");
            s_sql.AppendLine("类别编码 nvarchar(20) not null primary key,");
            s_sql.AppendLine("类别名称 nvarchar(50) not null");
            s_sql.AppendLine(")");
            return s_sql.ToString();
        }
    }
}
