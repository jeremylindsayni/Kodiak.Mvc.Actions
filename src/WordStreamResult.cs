using System;
using System.IO;
using System.Web.Mvc;

namespace Kodiak.Mvc.Actions
{
    public class WordStreamResult : ActionResult
    {
        private MemoryStream WordDocumentStream { get; set; }

        private string FileName { get; set; }

        public WordStreamResult(MemoryStream wordDocumentStream, string fileName)
        {
            if (wordDocumentStream is null || wordDocumentStream.Length == 0)
            {
                throw new ArgumentNullException("Word Document Stream is null or empty");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("File name is null or empty");
            }

            WordDocumentStream = wordDocumentStream;
            FileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-length", WordDocumentStream.Length.ToString());
            context.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            context.HttpContext.Response.AddHeader("content-disposition", $"attachment; filename={this.FileName}");
            context.HttpContext.Response.BinaryWrite(WordDocumentStream.ToArray());
            WordDocumentStream.Dispose();
            WordDocumentStream = null;
        }
    }
}
