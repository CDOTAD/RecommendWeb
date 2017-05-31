using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recommendWeb.Models
{
    public class Rating
    {
        /// <summary>
        /// rating model
        /// refer to rating table in database
        /// </summary>
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public float Rate { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}