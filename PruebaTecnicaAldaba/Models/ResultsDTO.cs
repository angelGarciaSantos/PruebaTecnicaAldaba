using Newtonsoft.Json;
using System.Collections.Generic;

namespace PruebaTecnicaAldaba.Models
{
    public class ResultsDTO
    {
        public ResultsDTO() { }

        [JsonProperty("page")]
        public long Page { get; set; }
        
        [JsonProperty("results")]
        public IEnumerable<MovieDTO> Results { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }
    }
}
