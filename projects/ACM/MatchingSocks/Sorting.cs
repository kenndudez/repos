using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchingSocks
{
    public class Sorting
    {
        public static int get_order(int[] input1)
        {
            var sorterArray = input1.OrderBy(o => o).ToArray();
            var unsortedArray = input1;
            int temp1;
            int swap = 0;
            
            int arrayLength = sorterArray.Length;
            for (int i = 0; i < arrayLength; i++)
            {
                if (sorterArray[i] != unsortedArray[i])
                {
                    temp1 = unsortedArray[i];
                    unsortedArray[i] = sorterArray[i];
                   // unsortedArray[j] = sorterArray[i];
                    unsortedArray[i] = temp1;

                    for (int j = i + 1; j < arrayLength; j++)
                    {
                        if (unsortedArray[j] == sorterArray[i])
                        {
                            unsortedArray[j] = temp1;
                            swap++;
                            break;
                        }
                    }
                }
            }

            return swap;
        }


        static int MinimumSwaps(int[] arr)
        {
            int result = 0;
            int temp;
            int counter = 0;
            for (int i = 0; i < arr.Length; ++i)
            {
                if (arr[i] - 1 == i)
                {
                    //once all sorted then
                    if (counter == arr.Length) break;
                    counter++;
                    continue;
                }
                temp = arr[arr[i] - 1];
                arr[arr[i] - 1] = arr[i];
                arr[i] = temp;
                result++;//swapped
                i = 0;//needs to start from the beginning after every swap
                counter = 0;//clearing the sorted array counter
            }
            return result;
        }
    }
}
