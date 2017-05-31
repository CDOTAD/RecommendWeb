using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recommendWeb.Models
{
    public class RecommendMovie
    {
        /// <summary>
        /// recommendMovie model
        /// 用于推荐
        /// </summary>
        public Movie Movie { get; set; }
        public double Similarity { get; set; }
    }
}