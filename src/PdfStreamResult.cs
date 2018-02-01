using System;
using System.IO;
using System.Web.Mvc;

namespace Kodiak.Actions
{
    public class PdfStreamResult : ActionResult
    {
        private MemoryStream PdfStream { get; set; }

        private string FileName { get; set; }

        public PdfStreamResult(MemoryStream pdfStream, string fileName)
        {
            if (pdfStream is null || pdfStream.Length == 0)
            {
                throw new ArgumentNullException("Pdf Stream is null or empty");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("File name is null or empty");
            }

            PdfStream = pdfStream;
            FileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-length", PdfStream.Length.ToString());
            context.HttpContext.Response.ContentType = "application/pdf";
            context.HttpContext.Response.AddHeader("content-disposition", $"attachment; filename={this.FileName}");
            context.HttpContext.Response.BinaryWrite(PdfStream.ToArray());
        }
    }
}