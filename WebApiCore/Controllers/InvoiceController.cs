using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DemoDbContext _context;
        public InvoiceController(DemoDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllHeader")]
        public IEnumerable<TblSalesHeader> GetAllHeader()
        {
            return _context.TblSalesHeader.ToList();
        }

        [HttpGet("GetAllHeaderbyCode")]
        public TblSalesHeader GetAllHeaderbyCode(string invoiceno)
        {
            return _context.TblSalesHeader.FirstOrDefault(o => o.InvoiceNo == invoiceno);
        }

        [HttpGet("GetAllDetailbyCode")]
        public IEnumerable<TblSalesHeader> GetAllDetailbyCode(string invoiceno)
        {
            return _context.TblSalesHeader.Where(o => o.InvoiceNo == invoiceno);
        }

        [HttpPost("Save")]
        public APIResponse Save([FromBody] InvoiceInput invoiceEntity)
        {
            string result = string.Empty;
            try
            {
                var _inv = _context.TblSalesHeader.FirstOrDefault(o => o.InvoiceNo == invoiceEntity.InvoiceNo);
                if (_inv != null)
                {
                    //_emp.Role = value.Role;
                    //_emp.Email = value.Email;
                    //_emp.Username = value.Username;
                    //_emp.Isactive = value.Isactive;
                    _context.SaveChanges();
                    result = "pass";
                }
                else
                {
                    TblSalesHeader tblHeader = new TblSalesHeader()
                    {
                        InvoiceNo = invoiceEntity.InvoiceNo,
                        CustomerId = invoiceEntity.CustomerId,
                        CustomerName = invoiceEntity.CustomerName,
                        DeliveryAddress = invoiceEntity.DeliveryAddress,
                        Remarks = invoiceEntity.DeliveryAddress,
                        Total = invoiceEntity.Total,
                        Tax = invoiceEntity.Tax,
                        NetTotal = invoiceEntity.NetTotal
                    };
                    _context.TblSalesHeader.Add(tblHeader);
                    _context.SaveChanges();

                    if(invoiceEntity.details.Count() > 0)
                    {
                        for (int i=0;i<= invoiceEntity.details.Count();i++)
                        {
                            TblSalesProductInfo product = new TblSalesProductInfo()
                            {
                                InvoiceNo = invoiceEntity.InvoiceNo,
                                ProductCode = invoiceEntity.details[i].ProductCode,
                                ProductName = invoiceEntity.details[i].ProductName,
                                Qty = invoiceEntity.details[i].Qty,
                                SalesPrice = invoiceEntity.details[i].SalesPrice,
                                Total = invoiceEntity.details[i].Total
                            };
                            _context.TblSalesProductInfo.Add(product);
                            _context.SaveChanges();
                        }
                    }
                    result = "pass";
                }

            }
            catch (Exception ex)
            {
                result = string.Empty;
            }
            return new APIResponse { keycode = string.Empty, result = result };
        }


        [HttpDelete("Remove")]
        public APIResponse Remove(string InvoiceNo)
        {
            string result = string.Empty;
            var _inv = _context.TblSalesHeader.FirstOrDefault(o => o.InvoiceNo == InvoiceNo);
            if (_inv != null)
            {
                _context.TblSalesHeader.Remove(_inv);
                _context.SaveChanges();
                result = "pass";
            }
            return new APIResponse { keycode = string.Empty, result = result };
        }

        //[HttpGet("generatepdf")]
        //public IActionResult GeneratePDF(string InvoiceNo)
        //{
        //    var document = new PdfDocument();
        //    string imgeurl = "data:image/png;base64, " + Getbase64string() + "";

        //    string[] copies = { "Customer copy", "Comapny Copy" };
        //    for (int i = 0; i < copies.Length; i++)
        //    {
        //        InvoiceHeader header = await this._container.GetAllInvoiceHeaderbyCode(InvoiceNo);
        //        List<InvoiceDetail> detail = await this._container.GetAllInvoiceDetailbyCode(InvoiceNo);
        //        string htmlcontent = "<div style='width:100%; text-align:center'>";
        //        htmlcontent += "<img style='width:80px;height:80%' src='" + imgeurl + "'   />";
        //        htmlcontent += "<h2>" + copies[i] + "</h2>";
        //        htmlcontent += "<h2>Welcome to Nihira Techiees</h2>";



        //        if (header != null)
        //        {
        //            htmlcontent += "<h2> Invoice No:" + header.InvoiceNo + " & Invoice Date:" + header.InvoiceDate + "</h2>";
        //            htmlcontent += "<h3> Customer : " + header.CustomerName + "</h3>";
        //            htmlcontent += "<p>" + header.DeliveryAddress + "</p>";
        //            htmlcontent += "<h3> Contact : 9898989898 & Email :ts@in.com </h3>";
        //            htmlcontent += "<div>";
        //        }



        //        htmlcontent += "<table style ='width:100%; border: 1px solid #000'>";
        //        htmlcontent += "<thead style='font-weight:bold'>";
        //        htmlcontent += "<tr>";
        //        htmlcontent += "<td style='border:1px solid #000'> Product Code </td>";
        //        htmlcontent += "<td style='border:1px solid #000'> Description </td>";
        //        htmlcontent += "<td style='border:1px solid #000'>Qty</td>";
        //        htmlcontent += "<td style='border:1px solid #000'>Price</td >";
        //        htmlcontent += "<td style='border:1px solid #000'>Total</td>";
        //        htmlcontent += "</tr>";
        //        htmlcontent += "</thead >";

        //        htmlcontent += "<tbody>";
        //        if (detail != null && detail.Count > 0)
        //        {
        //            detail.ForEach(item =>
        //            {
        //                htmlcontent += "<tr>";
        //                htmlcontent += "<td>" + item.ProductCode + "</td>";
        //                htmlcontent += "<td>" + item.ProductName + "</td>";
        //                htmlcontent += "<td>" + item.Qty + "</td >";
        //                htmlcontent += "<td>" + item.SalesPrice + "</td>";
        //                htmlcontent += "<td> " + item.Total + "</td >";
        //                htmlcontent += "</tr>";
        //            });
        //        }
        //        htmlcontent += "</tbody>";

        //        htmlcontent += "</table>";
        //        htmlcontent += "</div>";

        //        htmlcontent += "<div style='text-align:right'>";
        //        htmlcontent += "<h1> Summary Info </h1>";
        //        htmlcontent += "<table style='border:1px solid #000;float:right' >";
        //        htmlcontent += "<tr>";
        //        htmlcontent += "<td style='border:1px solid #000'> Summary Total </td>";
        //        htmlcontent += "<td style='border:1px solid #000'> Summary Tax </td>";
        //        htmlcontent += "<td style='border:1px solid #000'> Summary NetTotal </td>";
        //        htmlcontent += "</tr>";
        //        if (header != null)
        //        {
        //            htmlcontent += "<tr>";
        //            htmlcontent += "<td style='border: 1px solid #000'> " + header.Total + " </td>";
        //            htmlcontent += "<td style='border: 1px solid #000'>" + header.Tax + "</td>";
        //            htmlcontent += "<td style='border: 1px solid #000'> " + header.NetTotal + "</td>";
        //            htmlcontent += "</tr>";
        //        }
        //        htmlcontent += "</table>";
        //        htmlcontent += "</div>";

        //        htmlcontent += "</div>";

        //        PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);
        //    }
        //    byte[]? response = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        document.Save(ms);
        //        response = ms.ToArray();
        //    }
        //    string Filename = "Invoice_" + InvoiceNo + ".pdf";
        //    return File(response, "application/pdf", Filename);
        //}




        // // GET: api/<InvoiceController>
        // [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<InvoiceController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<InvoiceController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<InvoiceController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<InvoiceController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
