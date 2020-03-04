using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) => x + y;
            Func<int, int, int> added = (x, y) => { 
int temp = x + y;
                return temp;
            };

            Action<int> write = x => Console.WriteLine(x);
            write(square(add(3, 5)));
            Console.WriteLine(square(added(3, 5)));
            IEnumerable<Employee> developers = new Employee[]
        {
            new Employee {Id = 1, Name = "Scoutt"},
            new Employee {Id = 2, Name = "Chris"}
        };

            var sales = new List<Employee>()
        {
            new Employee {Id = 3 , Name = "Alex"}
        };
            foreach (var employee in developers.Where( 
                e  => e.Name.StartsWith("S")
           ))
                {
                Console.WriteLine(employee.Name);
            }
            //Linq - Method syntax 
            var query = developers.Where(x => x.Name.Length == 5).OrderBy(e => e.Name);

            //Linq - Query syntax 
            var query2 = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;

            query2.Count();
            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);
            }
            Console.WriteLine(developers.Count());
            var enumerator = developers.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }

        }

        private static bool NameStartWithS(Employee employee)
        {
            return employee.Name.StartsWith("S");
        }
    }
}
