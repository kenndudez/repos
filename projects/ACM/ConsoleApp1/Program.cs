using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int ans = 0;
            int n = 7;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)

                    if (arr[i] == arr[j])
                        ans++;

            Console.WriteLine("\t\n " + " occurs " + ans + " times");
            Console.ReadKey();
        }
    }
}
