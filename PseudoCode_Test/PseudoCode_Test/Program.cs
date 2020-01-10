using System;

namespace PseudoCode_Test
{
    class Program
    {
       
    static void Main(string[] args)
        {
            int numBase = 2;
            int exp = 16;
            int result = 1;



            while (exp > 0)
            {
                result = result * numBase;
                exp--;
            }

            Console.WriteLine($"Result is {result}");
            Console.WriteLine("Hello World!");


            // int redius = 34;
            // int height = 10;
            // int volume =1;
            // int exp = 2;
            // int pie = 3;
            // int redi = 1;

            //if (redius > 0)
            // {
            //     redi = redius * exp;
            //     exp--;
            // }

            // while (exp > 0)
            // {
            //     volume = pie * redius * height;
            // }

            // Console.WriteLine($"Volume is {volume}");
            Console.ReadKey();

        }
    }
}
