using System;
using System.Collections.Generic;

namespace WebApiCore.Models
{
    public partial class TblProduct
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Category { get; set; }
    }
}
