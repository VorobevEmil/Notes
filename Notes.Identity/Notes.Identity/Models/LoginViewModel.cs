﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Identity.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!; 
    }
}
