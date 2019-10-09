using CSVGenerators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVGenerators.Interface
{
   public interface ICsvExportService
    {
        string ReturnData();

        string[] GetValues(Product product);

        string[] GetColumnNames();
    }
}
