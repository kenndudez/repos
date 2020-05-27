using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.BLTest
{
    [TestClass]
    public class Socks
    {
        [TestMethod]
        public void MatchungColors()
        {

            int n = 7;
            int[] Arr = new int[] { 1, 2, 1, 2, 1, 3, 2 };

            int ans = 0;

            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)

                    // finding the index with same 
                    // value but different index. 
                    if (Arr[i] == Arr[j])
                        ans++;

            //return ans;

        }
    }
}

