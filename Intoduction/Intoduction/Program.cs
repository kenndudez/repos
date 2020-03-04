using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intoduction
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\Users";
          //  ShowLargeFileWithOutLinq(path);

            Console.WriteLine("***");
            ShowLargeFilesWithLinq(path);
        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            //var query = from file in new DirectoryInfo(path).GetFiles()
            //            orderby file.Length descending
            //            select file;

            var query2 = new DirectoryInfo(path).GetFiles()
                .OrderBy(f => f.Length).Take(5);

            foreach (var file in query2)
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:NO}");

            }
        }

        private static void ShowLargeFileWithOutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());
            for (int i = 0; i < 5; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine($"{file.Name, -20} : {file.Length, 10:NO}");
            }

        }

        public class FileInfoComparer : IComparer<FileInfo>
        {
            public int Compare(FileInfo x, FileInfo y)
            {
                return y.Length.CompareTo(x.Length);
            }
        }
    }
}
