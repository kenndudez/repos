using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using PdfGenerator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace PdfGenerator.Service
{
    public class MyPdfCreator
    {
        public byte[] GetDocumentStream()
        {
            Byte[] bytes = null;
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {

                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {

                        doc.Open();


                        var example_html = @"<p>This <em>is </em><span class=""headline"" style=""text-decoration: underline;"">some</span> <strong>sample <em> text</em></strong><span style=""color: red;"">!!!</span></p>";
                        var example_css = @".headline{font-size:200%}";

                        using (var htmlWorker = new HTMLWorker(doc))
                        {
                            using (var sr = new StringReader(example_html))
                            {
                                htmlWorker.Parse(sr);
                            }
                        }


                        using (var srHtml = new StringReader(example_html))
                        {
                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, srHtml);
                        }


                        using (var msCss = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_css)))
                        {
                            using (var msHtml = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(example_html)))
                            {
                                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msHtml, msCss);
                            }
                        }


                        doc.Close();
                    }
                }
                bytes = ms.ToArray();

            }

            //var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            //return File.WriteAllBytes(testFile,bytes);
            //return testFile;
            return bytes;
        }
    }
}





//     var ms = new MemoryStream();
////var docs = ReturnData(bool state);
//var doc = new Document();
//var writer = PdfWriter.GetInstance(doc, ms);
//writer.CloseStream = false;
//            doc.Open();
//            doc.Add(new Paragraph("Hello World"));
//            doc.Close();
//            writer.Close();
//            return ms;