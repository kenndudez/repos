using CSVGenerators.Interface;
using CSVGenerators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVGenerators.Service
{
    public class ProductAnalyticsExportService : GenericExportService, IProductAnalyticsExportService
    {

        public string Export(IList<Product> products)
        {
            var result = Export<ProductAnalyticsAttribute>(products);

            return result;
        }
    }
}
