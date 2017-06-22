using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ViewModel
{
    public class MovieView
    {
        public int MovieId { set; get; }
        public string Title { set; get; }
        
        public string Genres { get; set; }
    }
}
