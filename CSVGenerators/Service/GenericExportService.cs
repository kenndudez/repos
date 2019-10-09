using CSVGenerators.Data;
using CSVGenerators.Model;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSVGenerators.Service
{
    public abstract class GenericExportService
    {
        private const string CsvDelimeter = ";";

        protected string Export<TAttribute>(IEnumerable<Product> products)
            where TAttribute : ExportAttributes
        {
            using (var exportStream = (MemoryStream)GetStream<TAttribute>(products))
            {
                var encoding = new UTF8Encoding(false);
                return encoding.GetString(exportStream.ToArray());
            }
        }

        private Stream GetStream<TAttribute>(IEnumerable<Product> objectList)
            where TAttribute : ExportAttributes
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, new UTF8Encoding(false));

            var columns = GetColumns<TAttribute>().OrderBy(o => o.ExportAttribute.Order);
            var columnNames = columns.Select(c => c.ExportAttribute.ExportName != null
                ? c.ExportAttribute.ExportName
                : c.PropertyInfo.Name);
            streamWriter.WriteLine(string.Join(CsvDelimeter, columnNames));

            foreach (var item in objectList)
            {
                var values = GetProductValues<TAttribute>(item, columns);
                streamWriter.WriteLine(string.Join(CsvDelimeter, values));
            }

            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private IEnumerable<ExportProperty> GetColumns<TAttribute>()
            where TAttribute : ExportAttributes
        {
            return typeof(Product).GetProperties().Select(
                property => {
                    var exportAttribute = ((TAttribute)property.GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault());
                    return exportAttribute == null
                        ? null
                        : new ExportProperty { PropertyInfo = property, ExportAttribute = exportAttribute };
                }).Where(p => p != null);
        }

        private List<string> GetProductValues<TAttribute>(Product product, IEnumerable<ExportProperty> columns)
            where TAttribute : ExportAttributes
        {
            var propertyValues = new List<string>();
            foreach (var column in columns)
            {
                propertyValues.Add(GetAttributeValue(product, column.PropertyInfo, column.ExportAttribute));
            }

            return propertyValues;
        }

        private string GetAttributeValue<TAttribute>(Product product, PropertyInfo propertyInfo, TAttribute attribute)
            where TAttribute : ExportAttributes
        {
            object value = propertyInfo.GetValue(product);

            if (value == null || attribute == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(attribute.Format) && value is IFormattable)
            {
                return (value as IFormattable).ToString(attribute.Format, CultureInfo.CurrentCulture);
            }

            if (!string.IsNullOrWhiteSpace(attribute.Format))
            {
                return string.Format(attribute.Format, value);
            }

            return propertyInfo.GetValue(product).ToString();
        }
    }
}
