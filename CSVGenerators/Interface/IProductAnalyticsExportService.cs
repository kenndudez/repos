using CSVGenerators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVGenerators.Interface
{
    interface IProductAnalyticsExportService
    {
        string Export(IList<Product> products);
    }
}
