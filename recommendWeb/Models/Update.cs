using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace recommendWeb.Models
{
    public class Update
    {
        [Required]
        [MaxLength(140)]
        public string Status { get; set;}
        public DateTime Date { get; set; }
    }
}