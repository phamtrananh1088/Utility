using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace GenEntity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Initialize();
            this.Text = this.Text + " Version:" + typeof(Form1).Assembly.GetName().Version.ToString();
        }

        private void Initialize()
        {
            SetSourceComboTable();
        }

        private void SetSourceComboTable()
        {
            DataSet ds = new DataSet();
            DbConnect dbConn = new DbConnect();
            try
            {
                dbConn.Open();
                string sSQL = GetTableSQL();
                SqlCommand cmd = new SqlCommand(sSQL, dbConn.Conn);
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                adapt.Fill(ds);
            }
            catch (Exception)
            {
                dbConn.Close();
                throw;
            }
            cboTable.DataSource = ds.Tables[0];
            cboTable.DisplayMember = "TableName";
            cboTable.ValueMember = "ObjectID";
        }

        private string GetTableSQL()
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine("Select name      AS TableName  ");
            sbSQL.AppendLine("     , object_id AS ObjectID   ");
            sbSQL.AppendLine("  From sys.tables				 ");
            return sbSQL.ToString();
        }

        private string GetColumnSQL()
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine("select @table_name TableName,                                                                                                                     ");
            sbSQL.AppendLine("       col.name ColumnName,                                                                                                                       ");
            sbSQL.AppendLine("       column_id ColumnId,                                                                                                                        ");
            sbSQL.AppendLine("       case typ.name                                                                                                                              ");
            sbSQL.AppendLine("          when 'bigint' then 'long'                                                                                                               ");
            sbSQL.AppendLine("          when 'binary' then 'byte[]'                                                                                                             ");
            sbSQL.AppendLine("          when 'bit' then 'bool'                                                                                                                  ");
            sbSQL.AppendLine("          when 'char' then 'string'                                                                                                               ");
            sbSQL.AppendLine("          when 'date' then 'DateTime'                                                                                                             ");
            sbSQL.AppendLine("          when 'datetime' then 'DateTime'                                                                                                         ");
            sbSQL.AppendLine("          when 'datetime2' then 'DateTime'                                                                                                        ");
            sbSQL.AppendLine("          when 'datetimeoffset' then 'DateTimeOffset'                                                                                             ");
            sbSQL.AppendLine("          when 'decimal' then 'decimal'                                                                                                           ");
            sbSQL.AppendLine("          when 'float' then 'double'                                                                                                              ");
            sbSQL.AppendLine("          when 'image' then 'byte[]'                                                                                                              ");
            sbSQL.AppendLine("          when 'int' then 'int'                                                                                                                   ");
            sbSQL.AppendLine("          when 'money' then 'decimal'                                                                                                             ");
            sbSQL.AppendLine("          when 'nchar' then 'string'                                                                                                              ");
            sbSQL.AppendLine("          when 'ntext' then 'string'                                                                                                              ");
            sbSQL.AppendLine("          when 'numeric' then 'decimal'                                                                                                           ");
            sbSQL.AppendLine("          when 'nvarchar' then 'string'                                                                                                           ");
            sbSQL.AppendLine("          when 'real' then 'float'                                                                                                                ");
            sbSQL.AppendLine("          when 'smalldatetime' then 'DateTime'                                                                                                    ");
            sbSQL.AppendLine("          when 'smallint' then 'short'                                                                                                            ");
            sbSQL.AppendLine("          when 'smallmoney' then 'decimal'                                                                                                        ");
            sbSQL.AppendLine("          when 'sql_variant' then 'object'                                                                                                        ");
            sbSQL.AppendLine("          when 'text' then 'string'                                                                                                               ");
            sbSQL.AppendLine("          when 'time' then 'TimeSpan'                                                                                                             ");
            sbSQL.AppendLine("          when 'timestamp' then 'byte[]'                                                                                                          ");
            sbSQL.AppendLine("          when 'tinyint' then 'byte'                                                                                                              ");
            sbSQL.AppendLine("          when 'uniqueidentifier' then 'Guid'                                                                                                     ");
            sbSQL.AppendLine("          when 'varbinary' then 'byte[]'                                                                                                          ");
            sbSQL.AppendLine("          when 'varchar' then 'string'                                                                                                            ");
            sbSQL.AppendLine("          when 'xml' then 'object'                                                                                                                ");
            sbSQL.AppendLine("          else 'UNKNOWN_' + typ.name                                                                                                              ");
            sbSQL.AppendLine("       end ColumnType,                                                                                                                            ");
            sbSQL.AppendLine("       case                                                                                                                                       ");
            sbSQL.AppendLine("         when col.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2'                                               ");
            sbSQL.AppendLine("                             , 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime'                           ");
            sbSQL.AppendLine("                              , 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier')                                                  ");
            sbSQL.AppendLine("         then '?'                                                                                                                                 ");
            sbSQL.AppendLine("         else ''                                                                                                                                  ");
            sbSQL.AppendLine("       end NullableSign,                                                                                                                          ");
            sbSQL.AppendLine("       case typ.name when 'nvarchar' then col.max_length/2 else col.max_length/1 end ColumnSize,                                                    ");
            sbSQL.AppendLine("       case typ.name                                                                                                                              ");
            sbSQL.AppendLine("             when 'bigint' then 'BigInt'                                                                                                          ");
            sbSQL.AppendLine("             when 'binary' then 'VarBinary'                                                                                                       ");
            sbSQL.AppendLine("             when 'bit' then 'Bit'                                                                                                                ");
            sbSQL.AppendLine("             when 'char' then 'Char'                                                                                                              ");
            sbSQL.AppendLine("             when 'date' then 'Date'                                                                                                              ");
            sbSQL.AppendLine("             when 'datetime' then 'DateTime'                                                                                                      ");
            sbSQL.AppendLine("             when 'datetime2' then 'DateTime2'                                                                                                    ");
            sbSQL.AppendLine("             when 'datetimeoffset' then 'DateTimeOffset'                                                                                          ");
            sbSQL.AppendLine("             when 'decimal' then 'Decimal'                                                                                                        ");
            sbSQL.AppendLine("             when 'float' then 'Float'                                                                                                            ");
            sbSQL.AppendLine("             when 'image' then 'Binary'                                                                                                           ");
            sbSQL.AppendLine("             when 'int' then 'Int'                                                                                                                ");
            sbSQL.AppendLine("             when 'money' then 'Money'                                                                                                            ");
            sbSQL.AppendLine("             when 'nchar' then 'NChar'                                                                                                            ");
            sbSQL.AppendLine("             when 'ntext' then 'NText'                                                                                                            ");
            sbSQL.AppendLine("             when 'numeric' then 'Decimal'                                                                                                        ");
            sbSQL.AppendLine("             when 'nvarchar' then 'NVarChar'                                                                                                      ");
            sbSQL.AppendLine("             when 'real' then 'Real'                                                                                                              ");
            sbSQL.AppendLine("             when 'smalldatetime' then 'DateTime'                                                                                                 ");
            sbSQL.AppendLine("             when 'smallint' then 'SmallInt'                                                                                                      ");
            sbSQL.AppendLine("             when 'smallmoney' then 'SmallMoney'                                                                                                  ");
            sbSQL.AppendLine("             when 'sql_variant' then 'Variant'                                                                                                    ");
            sbSQL.AppendLine("             when 'text' then 'Text'                                                                                                              ");
            sbSQL.AppendLine("             when 'time' then 'Time'                                                                                                              ");
            sbSQL.AppendLine("             when 'timestamp' then 'Timestamp'                                                                                                    ");
            sbSQL.AppendLine("             when 'tinyint' then 'TinyInt'                                                                                                        ");
            sbSQL.AppendLine("             when 'uniqueidentifier' then 'UniqueIdentifier'                                                                                      ");
            sbSQL.AppendLine("             when 'varbinary' then 'VarBinary'                                                                                                    ");
            sbSQL.AppendLine("             when 'varchar' then 'VarChar'                                                                                                        ");
            sbSQL.AppendLine("             when 'xml' then 'Xml'                                                                                                                ");
            sbSQL.AppendLine("             else 'UNKNOWN_' + typ.name                                                                                                           ");
            sbSQL.AppendLine("       end SqlDbType,                                                                                                                             ");
            sbSQL.AppendLine("                                                                                                                                                  ");
            sbSQL.AppendLine("       case typ.name                                                                                                                              ");
            sbSQL.AppendLine("             when 'bigint' then 'GetSqlInt64'                                                                                                     ");
            sbSQL.AppendLine("             when 'binary' then 'GetSqlBinary'                                                                                                    ");
            sbSQL.AppendLine("             when 'bit' then 'GetSqlBoolean'                                                                                                      ");
            sbSQL.AppendLine("             when 'char' then 'GetSqlString'                                                                                                      ");
            sbSQL.AppendLine("             when 'date' then 'GetSqlDateTime'                                                                                                    ");
            sbSQL.AppendLine("             when 'datetime' then 'GetSqlDateTime'                                                                                                ");
            sbSQL.AppendLine("             when 'datetime2' then 'None'                                                                                                         ");
            sbSQL.AppendLine("             when 'datetimeoffset' then 'none'                                                                                                    ");
            sbSQL.AppendLine("             when 'decimal' then 'GetSqlDecimal'                                                                                                  ");
            sbSQL.AppendLine("             when 'float' then 'GetSqlDouble'                                                                                                     ");
            sbSQL.AppendLine("             when 'image' then 'GetSqlBinary'                                                                                                     ");
            sbSQL.AppendLine("             when 'int' then 'GetSqlInt32'                                                                                                        ");
            sbSQL.AppendLine("             when 'money' then 'GetSqlMoney'                                                                                                      ");
            sbSQL.AppendLine("             when 'nchar' then 'GetSqlString'                                                                                                     ");
            sbSQL.AppendLine("             when 'ntext' then 'GetSqlString'                                                                                                     ");
            sbSQL.AppendLine("             when 'numeric' then 'GetSqlDecimal'                                                                                                  ");
            sbSQL.AppendLine("             when 'nvarchar' then 'GetSqlString'                                                                                                  ");
            sbSQL.AppendLine("             when 'real' then 'GetSqlSingle'                                                                                                      ");
            sbSQL.AppendLine("             when 'smalldatetime' then 'GetSqlDateTime'                                                                                           ");
            sbSQL.AppendLine("             when 'smallint' then 'GetSqlInt16'                                                                                                   ");
            sbSQL.AppendLine("             when 'smallmoney' then 'GetSqlMoney'                                                                                                 ");
            sbSQL.AppendLine("             when 'sql_variant' then 'GetSqlValue'                                                                                              ");
            sbSQL.AppendLine("             when 'text' then 'GetSqlString'                                                                                                      ");
            sbSQL.AppendLine("             when 'time' then 'none'                                                                                                              ");
            sbSQL.AppendLine("             when 'timestamp' then 'GetSqlBinary'                                                                                                 ");
            sbSQL.AppendLine("             when 'tinyint' then 'GetSqlByte'                                                                                                     ");
            sbSQL.AppendLine("             when 'uniqueidentifier' then 'GetSqlGuid'                                                                                            ");
            sbSQL.AppendLine("             when 'varbinary' then 'GetSqlBinary'                                                                                                 ");
            sbSQL.AppendLine("             when 'varchar' then 'GetSqlString'                                                                                                   ");
            sbSQL.AppendLine("             when 'xml' then 'GetSqlXml'                                                                                                          ");
            sbSQL.AppendLine("             else 'UNKNOWN_' + typ.name                                                                                                           ");
            sbSQL.AppendLine("       end SqlDataReaderType,                                                                                                                     ");
            sbSQL.AppendLine("       typ.name SqlServerType                                                                                                                     ");
            sbSQL.AppendLine("  from sys.columns col                                                                                                                            ");
            sbSQL.AppendLine("  join sys.types typ on                                                                                                                           ");
            sbSQL.AppendLine("        col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id                                                           ");
            sbSQL.AppendLine("where object_id = @object_id                                                                                                                      ");

            return sbSQL.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cboTable.SelectedIndex < 0)
            {
                cboTable.Focus();
                return;
            }
            GetColumn();
        }

        private void GetColumn()
        {
            DataRowView drv = cboTable.SelectedItem as DataRowView;
            string tableName = drv.Row["TableName"].ToString();
            int objectID = int.Parse(drv.Row["ObjectID"].ToString());
            DataSet ds = new DataSet();
            DbConnect dbConn = new DbConnect();
            try
            {
                dbConn.Open();
                string sSQL = GetColumnSQL();
                SqlCommand cmd = new SqlCommand(sSQL, dbConn.Conn);
                cmd.Parameters.Add("@table_name", SqlDbType.NVarChar).Value = tableName;
                cmd.Parameters.Add("@object_id", SqlDbType.Int).Value = objectID;
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                adapt.Fill(ds);
            }
            catch (Exception)
            {
                dbConn.Close();
                throw;
            }
            //string[] listColumnInBase = new string[] { "入力者コード", "更新者コード", "新規登録日", "更新年月日" };
            StringBuilder sbClass = new StringBuilder();
            sbClass.AppendLine(txtHeader.Text);
            sbClass.AppendLine("{");
            sbClass.AppendLine(string.Format("    public class {0} : 基本クラス1", tableName));
            sbClass.AppendLine("    {");
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                EntityModel en = new EntityModel()
                {
                    TableName = item["TableName"].ToString(),
                    ColumnName = item["ColumnName"].ToString(),
                    ColumnType = item["ColumnType"].ToString(),
                    NullableSign = item["NullableSign"].ToString(),
                    ColumnSize = (int)item["ColumnSize"],
                    SqlDbType = item["SqlDbType"].ToString(),
                    SqlDataReaderType = item["SqlDataReaderType"].ToString(),
                    SqlServerType = item["SqlServerType"].ToString(),
                };
                ////exclude column in base class
                //if (listColumnInBase.Contains(en.ColumnName))
                //{
                //    continue;
                //}
                if (en.MaxLength > 0)
                {
                    sbClass.AppendLine(string.Format("        [MaxLengthAttribute({0})]", en.MaxLength));
                    sbClass.AppendLine(string.Format(@"        [SqlParameterAttribute(""{0}"", SqlDbType.{1},{2})]", en.ColumnName, en.SqlDbType, en.MaxLength));
                }
                else
                {
                    sbClass.AppendLine(string.Format(@"        [SqlParameterAttribute(""{0}"", SqlDbType.{1})]", en.ColumnName, en.SqlDbType));
                }
                sbClass.AppendLine(string.Format("        public {0} {1} {{ get; set; }}", en.ColumnType + en.NullableSign, en.ColumnName));
                sbClass.AppendLine();
            }
            sbClass.AppendLine("    }");
            //GetTable
            sbClass.AppendLine();
            sbClass.AppendLine("    public static partial class TableModelCollection");
            sbClass.AppendLine("    {");
            sbClass.AppendLine(string.Format("        public static DataTable GetTable{0}()", tableName));
            sbClass.AppendLine("        {");
            sbClass.AppendLine("            DataTable tbl = new DataTable();");
            
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                EntityModel en = new EntityModel()
                {
                    TableName = item["TableName"].ToString(),
                    ColumnName = item["ColumnName"].ToString(),
                    ColumnType = item["ColumnType"].ToString(),
                    NullableSign = item["NullableSign"].ToString(),
                    ColumnSize = (int)item["ColumnSize"],
                    SqlDbType = item["SqlDbType"].ToString(),
                    SqlDataReaderType = item["SqlDataReaderType"].ToString(),
                    SqlServerType = item["SqlServerType"].ToString(),
                };
                sbClass.AppendLine(string.Format(@"            tbl.Columns.Add(""{0}"", typeof({1}));", en.ColumnName,en.ColumnType));
            }
            
            sbClass.AppendLine("            return tbl;");
            sbClass.AppendLine("        }");
            sbClass.AppendLine("    }");
            sbClass.AppendLine("}");

            using (FileStream fs = new FileStream(string.Format(@".\{0}.cs", tableName), FileMode.Create))
            {
                Encoding unicode = Encoding.GetEncoding(932);
                byte[] bs = unicode.GetBytes(sbClass.ToString());
                fs.Write(bs, 0, bs.Length);
            }
            System.Diagnostics.Process.Start(string.Format(@".\{0}.cs", tableName));
        }
        private class EntityModel
        {
            /// <summary>
            /// 
            /// </summary>
            public string TableName { get; set; }
            public string ColumnName { get; set; }
            public string ColumnType { get; set; }
            public string NullableSign { get; set; }
            public int ColumnSize { get; set; }
            public string SqlDbType { get; set; }
            public string SqlDataReaderType { get; set; }
            public string SqlServerType { get; set; }
            public int MaxLength {
                get
                {
                    if (SqlServerType== "ntext")
                    {
                        return 0;
                    }
                    else if (ColumnType == "string")
                    {
                        return ColumnSize;
                    }
                    else
                    {
                        return 0;
                    }
                }
                    }
        }
    }
}
