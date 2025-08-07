using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Invoice")]
    public class InvoiceController : ControllerBase
    {
        [HttpPost("download")]
        public IActionResult DownloadInvoice([FromBody] InvoiceRequest request)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Default font (safe and supported by PdfSharpCore)
            XFont titleFont = new XFont("Helvetica", 20, XFontStyle.Bold);
            XFont regularFont = new XFont("Helvetica", 12, XFontStyle.Regular);

            // Draw Title
            gfx.DrawString("Turf Booking Invoice", titleFont, XBrushes.Black,
                new XRect(0, 40, page.Width, 40),
                XStringFormats.TopCenter);

            // Draw Details
            int y = 100;
            int lineHeight = 30;

            gfx.DrawString($"Booking ID: {request.BookingId}", regularFont, XBrushes.Black, 50, y); y += lineHeight;
            gfx.DrawString($"Turf ID: {request.TurfId}", regularFont, XBrushes.Black, 50, y); y += lineHeight;
            gfx.DrawString($"Payment Method: {request.PaymentMethod}", regularFont, XBrushes.Black, 50, y); y += lineHeight;
            gfx.DrawString($"Amount Paid: ₹{request.AmountPaid:F2}", regularFont, XBrushes.Black, 50, y); y += lineHeight;
            gfx.DrawString($"Date: {DateTime.Now:dd-MM-yyyy}", regularFont, XBrushes.Black, 50, y);

            // Save the document into a stream
            using var stream = new MemoryStream();
            document.Save(stream, false);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", $"Invoice_{request.BookingId}.pdf");
        }
    }
}
