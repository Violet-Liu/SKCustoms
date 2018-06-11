using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class GridPager
    {
        public int Pg_Size { get; set; }//每页行数
        public int Pg_Index { get; set; }//当前页是第几页
        public bool Order { get; set; }//排序方式
        public string Sort { get; set; }//排序列
        public int TotalRows { get; set; }//总行数
        public int TotalPages //总页数
        {
            get
            {
                return (int)Math.Ceiling((float)TotalRows / (float)Pg_Size);
            }
        }
        public string FilterRules { get; set; }
    }


    public class GridRows<T>
    {
        public List<T> Rows { get; set; }
        public int Total { get; set; }
    }
}
