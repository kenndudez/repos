using CSVGenerators.Model;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CSVGenerators.Data
{
    public class ExportProperty
    {
        public PropertyInfo PropertyInfo { get; set; }

        public ExportAttributes ExportAttribute { get; set; }
    }
}
