using System;
using System.Collections.Generic;

namespace WebApiCore.Models
{
    public partial class TblCrudNetCore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
