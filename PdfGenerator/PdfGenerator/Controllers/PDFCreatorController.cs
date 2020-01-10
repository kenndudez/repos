using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Model;
using PdfGenerator.Service;

namespace PdfGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PDFCreatorController : Controller
    {

        [HttpPost]
        public (JsonResult, IActionResult) CreatePDF([FromBody] ReportViewModel model)
        {
            try
            {
                return (Json(new { isError = false }), GetDocument());
            }

            catch (System.Exception ex)
            {
                Serilog.Log.Error($"ReportController.CreatePDF() - {ex}");
                return (Json(new { isError = true, errorMessage = ex.Message }), null);
            }
        }

        public FileContentResult GetDocument()
        {
            var fc = new MyPdfCreator();
            var document = fc.GetDocumentStream();
            return File(document.ToArray(), "application/pdf", "File Name.pdf");
        }
    }
}