using System;
using System.Collections.Generic;

namespace WebApiCore.Models
{
    public partial class TblUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
