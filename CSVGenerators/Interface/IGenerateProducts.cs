using CSVGenerators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVGenerators.Interface
{
   public interface IGenerateProducts 
    {
        List<Product> GenerateProduct(int count);
    }
}
