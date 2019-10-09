using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVGenerators.Model
{
    public class Product
    {
      
        public string Name { get; set; }
        public string  Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string ProducerName{ get; set; }
        public string QuantityAvailable { get; set; }
        public string QuantitySoldLastMonth{ get; set; }
        public string Weight { get; set; }
        public string Price { get; set; }
        public string LastOrderDate { get; set; }
}

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ExportAttributes : Attribute
    {
        public string ExportName { get; set; }

        public string Format { get; set; }

        public int Order { get; set; }
    }

    public class ProductAnalyticsAttribute : ExportAttributes
    {
    }
}
