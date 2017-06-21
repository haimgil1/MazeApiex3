using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AP2ex3.Models
{
    public class User
    {
        [Required]
        [Key]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

    }
}