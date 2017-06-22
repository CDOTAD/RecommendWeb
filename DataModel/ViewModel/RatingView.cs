using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ViewModel
{
    public class RatingView
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime TimeStamp { get; set; }
        public float Rating { get; set; }
    }
}
