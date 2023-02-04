using System;
using System.Collections.Generic;

namespace WebApiCore.Models
{
    public partial class TblUserMaster
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool? Isactive { get; set; }
    }
}
