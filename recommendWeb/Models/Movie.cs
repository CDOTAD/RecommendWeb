

namespace recommendWeb.Models
{
    /// <summary>
    /// Movie model
    /// 对应database 中 movie table
    /// </summary>
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }
    }
}