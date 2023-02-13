using System;
using System.Collections.Generic;

namespace WebApiCore.Models
{
    public partial class TblSalesProductInfo
    {
        public string InvoiceNo { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int? Qty { get; set; }
        public decimal? SalesPrice { get; set; }
        public decimal? Total { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ModifyUser { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
