﻿using System;
using System.Collections.Generic;

namespace WebApiCore.Models
{
    public partial class TblRefreshtoken
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TokenId { get; set; }
        public string RefreshToken { get; set; }
        public bool? IsActive { get; set; }
    }
}
