using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchingSocks
{
    class Program
    {

static void Main(string[] args)
{
    int[] array = { 10, 5, 10, 2, 2, 3, 4, 5, 5, 6, 7, 8, 9, 11, 12, 12 };

    for (int i = 0; i < array.Length; i++)
    {
        int count = 0;
              

        for (int j = 0; j < array.Length; j++)
        {

                    if (array[i] == array[j])
                        count++;
         
            
            
        }
        Console.WriteLine("\t\n " + array[i] + " occurs " + count + " times");
    }
    Console.ReadKey();
           
}


        static int countPairs(int[] arr, int n)
        {
            int ans = 0;

            // for each index i and j 
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)

                    // finding the index with same 
                    // value but different index. 
                    if (arr[i] == arr[j])
                        ans++;
            return ans;
        }

        static void sockspairing(int[] arr, int n)
        {
            int ans = 0;
             
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)

                    if (arr[i] == arr[j])
                        ans++;

            Console.WriteLine("\t\n " +" occurs " + ans + " times");
            Console.ReadKey();
        }
        
    }
}