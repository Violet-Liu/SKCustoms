using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ints = new List<int> { 1, 2, 3, 45, 5 };
            var list = new List<Student>
            {
                new Student { Name = "张三" },
                new Student { Name = "李四" }
            };

            var jsonresult = list.ToJson();

            var dese = jsonresult.ToObject<List<Student>>();

            Console.ReadLine();
        }
    }

    class Student
    {
       public string  Name{ get; set; }
    }
}
