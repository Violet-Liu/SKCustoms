using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repostories.BulkExtensions
{
    internal class MySqlBulkOperationProvider
    {
        private DbContext _context;
        private string _connectionString;

        public MySqlBulkOperationProvider(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException("context");
            _connectionString = context.Database.Connection.ConnectionString;
        }


        public int Insert<T>(IEnumerable<T> entities)
        {
            var insertCount = 0;
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {

                    try
                    {
                        insertCount = Insert(entities, transaction);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        if (transaction.Connection != null)
                        {
                            transaction.Rollback();
                        }
                        throw;
                    }
                }
            }
            
            return insertCount;
        }

        private int Insert<T>(IEnumerable<T> entities, MySqlTransaction transaction)
        {
            TableMapping tableMapping = DbMapper.GetDbMapping(_context)[typeof(T).Name];
            using (DataTable dataTable = CreateDataTable(tableMapping, entities))
            {
                if (string.IsNullOrEmpty(dataTable.TableName)) throw new Exception("请给DataTable的TableName属性附上表名称");
                if (dataTable.Rows.Count == 0) return 0;
                int insertCount = 0;
                string tmpPath = Path.GetTempFileName();
                string csv = DataTableToCsv(dataTable);
                File.WriteAllText(tmpPath, csv);
                MySqlBulkLoader bulk = new MySqlBulkLoader(transaction.Connection)
                {
                    FieldTerminator = ",",
                    FieldQuotationCharacter = '"',
                    EscapeCharacter = '"',
                    LineTerminator = "\r\n",
                    FileName = tmpPath,
                    NumberOfLinesToSkip = 0,
                    TableName = dataTable.TableName,
                };
                //bulk.Columns.AddRange(table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToArray());  
                insertCount = bulk.Load();
                File.Delete(tmpPath);
                return insertCount;
            }
        }


        private string DataTableToCsv(DataTable table)
        {
            //以半角逗号（即,）作分隔符，列为空也要表达其存在。  
            //列内容如存在半角逗号（即,）则用半角引号（即""）将该字段值包含起来。  
            //列内容如存在半角引号（即"）则应替换成半角双引号（""）转义，并用半角引号（即""）将该字段值包含起来。  
            StringBuilder sb = new StringBuilder();
            DataColumn colum;
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + colum.ColumnName.ToUpper() == "ID" ? "0" : row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(colum.ColumnName.ToUpper() == "ID" ? "0" : row[colum].ToString());
                }
                sb.AppendLine();
            }


            return sb.ToString();
        }

        private static DataTable CreateDataTable<T>(TableMapping tableMapping, IEnumerable<T> entities)
        {
            var dataTable = BuildDataTable<T>(tableMapping);

            foreach (var entity in entities)
            {
                DataRow row = dataTable.NewRow();

                foreach (var columnMapping in tableMapping.Columns)
                {
                    var @value = entity.GetPropertyValue(columnMapping.PropertyName);

                    if (columnMapping.IsIdentity) continue;

                    if (@value == null)
                    {
                        row[columnMapping.ColumnName] = DBNull.Value;
                    }
                    else
                    {
                        row[columnMapping.ColumnName] = @value;
                    }
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        private static DataTable BuildDataTable<T>(TableMapping tableMapping)
        {
            var entityType = typeof(T);
            string tableName = tableMapping.TableName;

            var dataTable = new DataTable(tableName);
            var primaryKeys = new List<DataColumn>();

            foreach (var columnMapping in tableMapping.Columns)
            {
                var propertyInfo = entityType.GetProperty(columnMapping.PropertyName, '.');
                columnMapping.Type = propertyInfo.PropertyType;

                var dataColumn = new DataColumn(columnMapping.ColumnName);

                if (propertyInfo.PropertyType.IsNullable(out Type dataType))
                {
                    dataColumn.DataType = dataType;
                    dataColumn.AllowDBNull = true;
                }
                else
                {
                    dataColumn.DataType = propertyInfo.PropertyType;
                    dataColumn.AllowDBNull = columnMapping.Nullable;
                }

                if (columnMapping.IsIdentity)
                {
                    dataColumn.Unique = true;
                    if (propertyInfo.PropertyType == typeof(int)
                      || propertyInfo.PropertyType == typeof(long))
                    {
                        dataColumn.AutoIncrement = true;
                    }
                    else continue;
                }
                else
                {
                    dataColumn.DefaultValue = columnMapping.DefaultValue;
                }

                if (propertyInfo.PropertyType == typeof(string))
                {
                    dataColumn.MaxLength = columnMapping.MaxLength;
                }

                if (columnMapping.IsPk)
                {
                    primaryKeys.Add(dataColumn);
                }

                dataTable.Columns.Add(dataColumn);
            }

            dataTable.PrimaryKey = primaryKeys.ToArray();

            return dataTable;
        }
    }
}
