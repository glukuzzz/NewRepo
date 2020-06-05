using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.StyledXmlParser.Css.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ViachappaFactAuth.Controllers
{
    public class PDFController : Controller
    {
        DB.AAModel db = new DB.AAModel();
        // GET: PDF
        public ActionResult TORG12View(Guid id)
        {
            return View(db.Orders.First(x=>x.Id == id));
        }

        public FileResult TORG12(Guid id)
        {
            var client = new WebClient();
            var t = db.Orders.First(x => x.Id == id);
            client.Encoding = Encoding.UTF8;
            var p = MvcHelpers.RenderViewToString(this.ControllerContext, "TORG12View",t);

            var workStream = new MemoryStream();
            using (var writer = new PdfWriter(workStream))
            {
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetTagged();
                PageSize pageSize = PageSize.A4;
                pdf.SetDefaultPageSize(pageSize);
                ConverterProperties properties = new ConverterProperties();
                writer.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(p, pdf, properties)) { }
            }
            workStream.Position = 0;

            var doc_guid = Guid.NewGuid();

            t.OrderTransferDoc = new DB.OrderTransferDoc
            {
                User_Id = db.AspNetUsers.First(x=>x.UserName == User.Identity.Name).Id,
                OperDate = DateTime.Now,
                DocDate= t.DocDate,
                DocNumber = t.DocNumber.ToString(),
                Id = doc_guid,
              
            };

            db.SaveChanges();
            
            foreach(var pos in t.OrderPositions)
            {
                db.WarehouseItems.Add(new DB.WarehouseItem
                {
                    Count = -pos.Count,
                    Id = Guid.NewGuid(),
                    Material_Id = pos.Material_Id,
                    VATRate_Id = (int)pos.VATRate_Id,
                    Price = pos.Price,
                    OrderTransferDoc_Id = doc_guid,
                    OrderInvoiceDate =DateTime.Today,
                    
                });
                db.SaveChanges();
            }
            return File(workStream, "application/pdf");
        }



        public ActionResult FactSchetView(Guid id)
        {
            return View(db.Orders.First(x => x.Id == id));
        }

        public FileResult FactSchet(Guid id)
        {
            var client = new WebClient();

            client.Encoding = Encoding.UTF8;
            var p = MvcHelpers.RenderViewToString(this.ControllerContext, "FactSchetView", db.Orders.First(x => x.Id == id));

            var workStream = new MemoryStream();
            using (var writer = new PdfWriter(workStream))
            {
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetTagged();
                PageSize pageSize = PageSize.A4.Rotate();
                pdf.SetDefaultPageSize(pageSize);
                ConverterProperties properties = new ConverterProperties();
                writer.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(p, pdf, properties)) { }
            }
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }



        public ActionResult UPDView(Guid id)
        {
            return View(db.Orders.First(x => x.Id == id));
        }

        public FileResult UPD(Guid id)
        {
            var client = new WebClient();

            client.Encoding = Encoding.UTF8;
            var p = MvcHelpers.RenderViewToString(this.ControllerContext, "UPDView", db.Orders.First(x => x.Id == id));

            var workStream = new MemoryStream();
            using (var writer = new PdfWriter(workStream))
            {
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetTagged();
                PageSize pageSize = PageSize.A4.Rotate();
                pageSize.IncreaseHeight(4);
                pdf.SetDefaultPageSize(pageSize);
                ConverterProperties properties = new ConverterProperties();
                properties.SetMediaDeviceDescription(new MediaDeviceDescription(MediaType.PRINT));
                writer.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(p, pdf, properties)) { }
            }
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }


        public ActionResult SFView(Guid id)
        {
            return View(db.Orders.First(x => x.Id == id));
        }

        public FileResult SF(Guid id)
        {
            var client = new WebClient();

            client.Encoding = Encoding.UTF8;
            var p = MvcHelpers.RenderViewToString(this.ControllerContext, "SFView", db.Orders.First(x => x.Id == id));

            var workStream = new MemoryStream();
            using (var writer = new PdfWriter(workStream))
            {
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetTagged();
                PageSize pageSize = PageSize.A4.Rotate();
                pageSize.IncreaseHeight(4);
                pdf.SetDefaultPageSize(pageSize);
                ConverterProperties properties = new ConverterProperties();
                properties.SetMediaDeviceDescription(new MediaDeviceDescription(MediaType.PRINT));
                writer.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(p, pdf, properties)) { }
            }
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }


        public ActionResult OplSchetView(Guid id)
        {
            return View(db.Orders.First(x => x.Id == id));
        }

        public FileResult OplSchet(Guid id)
        {
            var client = new WebClient();

            client.Encoding = Encoding.UTF8;
            var p = MvcHelpers.RenderViewToString(this.ControllerContext, "OplSchetView", db.Orders.First(x => x.Id == id));


            var workStream = new MemoryStream();
            using (var pdfWriter = new PdfWriter(workStream))
            {
                pdfWriter.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(p, pdfWriter)) { }
            }
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }

        public ActionResult AktView()
        {
            Guid id = Guid.Parse("435A000D-AC32-4C50-ACDD-2BB31CCA76F0");
            return View(db.Orders.First(x => x.Id == id));
        }

        public FileResult Akt()
        {
            var client = new WebClient();
            Guid id = Guid.Parse("435A000D-AC32-4C50-ACDD-2BB31CCA76F0");
            //client.Encoding = Encoding.UTF8;
            //var p = client.DownloadString(Request.Url.ToString().Replace("Akt", "AktView"));

            var p = MvcHelpers.RenderViewToString(this.ControllerContext, "AktView", db.Orders.First(x=>x.Id == id));

            var workStream = new MemoryStream();
            using (var pdfWriter = new PdfWriter(workStream))
            {

                pdfWriter.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(p, pdfWriter)) { }
            }
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }
        public ActionResult OplataCountView(Guid id)
        {
            
            return View(db.Orders.First(x => x.Id == id));
        }

        public FileResult OplataCount(Guid id)
        {
            var client = new WebClient();

            client.Encoding = Encoding.UTF8;
            var p = MvcHelpers.RenderViewToString(this.ControllerContext, "OplataCountView", db.Orders.First(x => x.Id == id));

            var workStream = new MemoryStream();
            using (var pdfWriter = new PdfWriter(workStream))
            {

                pdfWriter.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(p, pdfWriter)) { }
            }
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }

        public ActionResult M15View()
        {
            Guid id = Guid.Parse("435A000D-AC32-4C50-ACDD-2BB31CCA76F0");
            return View(db.Orders.First());
        }

        public FileResult M15()
        {
            var client = new WebClient();

            client.Encoding = Encoding.UTF8;
            var p = MvcHelpers.RenderViewToString(this.ControllerContext, "M15View", db.Orders.First());

            var workStream = new MemoryStream();
            using (var writer = new PdfWriter(workStream))
            {
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetTagged();
                PageSize pageSize = PageSize.A4.Rotate();
                pageSize.IncreaseHeight(4);
                pdf.SetDefaultPageSize(pageSize);
                ConverterProperties properties = new ConverterProperties();
                properties.SetMediaDeviceDescription(new MediaDeviceDescription(MediaType.PRINT));
                writer.SetCloseStream(false);
                using (var document = HtmlConverter.ConvertToDocument(p, pdf, properties)) { }
            }
            workStream.Position = 0;
            return File(workStream, "application/pdf");
        }
    }
}