using Bogus;
using CSVGenerators.Interface;
using CSVGenerators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVGenerators
{
    public class GenerateProductsService : IGenerateProducts
    {
       public List<Product>GenerateProduct(int count)
        {
            var productGenerator = new Faker<Product>()
            .RuleFor(p => p.Id, v => "!@#-3433")
            .RuleFor(p => p.Name, v => v.Commerce.ProductName())
            .RuleFor(p => p.ReferenceNumber, v => v.IndexGlobal.ToString())
            .RuleFor(p => p.ProducerName, v => v.Company.CompanyName())
            .RuleFor(p => p.QuantityAvailable, v => "")
            .RuleFor(p => p.QuantitySoldLastMonth, v =>"")
            .RuleFor(p => p.Weight, v =>"")
            .RuleFor(p => p.Price, v => "")
            .RuleFor(p => p.LastOrderDate, v => "");

            return productGenerator.Generate(count);
        }
    }
}
