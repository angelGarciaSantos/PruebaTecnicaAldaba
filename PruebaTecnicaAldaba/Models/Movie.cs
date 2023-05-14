using System;

namespace PruebaTecnicaAldaba.Models
{
    public class Movie
    {
        public string? Title { get; set; }
        public string? OriginalTitle { get; set; }
        public float? AVGRating { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Overview { get; set; }
        public string? SimilarMovies { get; set; }
    }
}
