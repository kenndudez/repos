﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public class Movies
    {
        public string Title { get; set; }
        public float Rating { get; set; }

        int _year;
        public int Year { get
            {
               // throw new Exception("Error!");

                Console.WriteLine($"Returning {_year} for {Title}");
                return _year;
            }
               set {

                _year = value;

            } }
    }
}
