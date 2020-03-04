using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movies>
            {
                new Movies {Title = "The Dark Knight", Rating= 8.9f, Year= 2008},
                new Movies {Title = "The King's Speech", Rating= 8.0f, Year= 2010},
                new Movies {Title = "Casablanca", Rating= 8.5f, Year= 1999},
                new Movies {Title = "Star Wars V", Rating= 8.7f, Year= 1980},
            };
           // var query = Enumerable.Empty<Movies>();

            // Ectension Method
          //var query = movies.Where(m => m.Year > 2000)
          //      .OrderByDescending(m=> m.Rating);

            // Query Method 
            var query = from movie in movies
                        where movie.Year > 2000
                        orderby movie.Rating descending
                        select movies;
            // Define a query

            //foreach (var movie in query)  // Execute query
            //{
            //    Console.WriteLine(movie.Title);
            //}
            //query = query.Take(1);
            // Console.WriteLine(query.Count());
            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);            }
        }
    }
}
