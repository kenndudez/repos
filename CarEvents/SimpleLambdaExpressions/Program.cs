using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLambdaExpressions
{
    class Program
    {
        private static int value;

        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Lambdas *****\n");
            TraditionalDelegateSyntax();
            AnonymousMethodSyntax();
            addingvalue();
            Console.ReadLine();
        }



        static void TraditionalDelegateSyntax()
        {
            // Make a list of integers.
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });
            // Call FindAll() using traditional delegate syntax.
            Predicate<int> callback = IsEvenNumber;
            List<int> evenNumbers = list.FindAll(callback);
            Console.WriteLine("Here are your even numbers:");
            foreach (int evenNumber in evenNumbers)
            {
                Console.Write("{0}\t", evenNumber);
            }
            Console.WriteLine();
        }

        static void AnonymousMethodSyntax()
        {
            // Make a list of integers.
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });
            // Now, use an anonymous method.
            List<int> evenNumbers = list.FindAll(delegate (int i)
            { return (i % 2) == 0; });
            Console.WriteLine("Here are your even numbers:");
            foreach (int evenNumber in evenNumbers)
            {
                Console.Write("{0}\t", evenNumber);
            }
            Console.WriteLine();
        }

        static void LambdaExpressionSyntax()
        {
            // Make a list of integers.
            List<int> list = new List<int>();
            list.AddRange(new int[] { 20, 1, 4, 8, 9, 44 });
            // Now, use a C# lambda expression.
            List<int> evenNumbers = list.FindAll(i => (i % 2) == 0);
            Console.WriteLine("Here are your even numbers:");
            foreach (int evenNumber in evenNumbers)
            {
                Console.Write("{0}\t", evenNumber);
            }
            Console.WriteLine();
        }



        static void addingvalue()
        {
            int i;
             
            int[] digits = new int[400];
            Console.WriteLine("Solution:");
            for (i=0; i<400; i++ )
            {
                digits[i] = value;
                
            }
            Console.WriteLine();
        }

        static void addtion()
        {
            
            List<int> termlist = new List<int>();
            for (int i = 0; i < 400; i++)
            {
                termlist.Add(i);
                Console.Write(termlist);
            }
           
            Console.WriteLine();
        }
        static bool IsEvenNumber(int i)
        {
            // Is it an even number?
            return (i % 2) == 0;
        }
    }
}

