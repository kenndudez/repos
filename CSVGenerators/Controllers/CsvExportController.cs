using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVGenerators.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSVGenerators.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvExportController : ControllerBase
    {
        private readonly ICsvExportService _csvExport;

        public CsvExportController(ICsvExportService csvExport)
        {
            _csvExport = csvExport;
        }

        [Route("Products")]
        [HttpGet]
        public IActionResult Products()
        {
            var data = _csvExport.ReturnData();
            var NewTruckFile = Encoding.UTF8.GetBytes(data);
            return File(NewTruckFile, "text/csv", "truck.csv");
        }
    }
}