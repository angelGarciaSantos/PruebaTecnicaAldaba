using Newtonsoft.Json;
using System;

namespace PruebaTecnicaAldaba.Models
{
    public class MovieDTO
    {
        public MovieDTO() { }

        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("original_title")]
        public string? OriginalTitle { get; set; }

        [JsonProperty("overview")]
        public string? Overview { get; set; }

        [JsonProperty("vote_average")]
        public float? VoteAverage { get; set; }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }
    }
}
