using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingSample
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Employee> developers = new Employee[]
       {
            new Employee {Id = 1, Name = "Scoutt"},
            new Employee {Id = 2, Name = "Chris"}
       };

            IEnumerable<Employee> sales = new List<Employee>()
        {
            new Employee {Id = 3 , Name = "Alex"}
        };
            IEnumerator<Employee> enumerator = developers.GetEnumerator();
           // while (enumerator.)
            {

            }
        }
      

      
}
    }

