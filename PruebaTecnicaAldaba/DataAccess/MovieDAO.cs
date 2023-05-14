using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PruebaTecnicaAldaba.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PruebaTecnicaAldaba.DataAccess
{
    public class MovieDAO : IMovieDAO
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly ILogger<MovieDAO> _logger;

        public MovieDAO(ILogger<MovieDAO> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<ResultsDTO> GetMovieResultsByQuery(string query)
        {
            string APIKey = _configuration.GetValue<string>("APIKey");
            HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key={APIKey}&query={query}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Couldn't get movies results for query: {query}.");
                throw new Exception($"Couldn't get movies results for query: {query}.");
            }
            var result = await ReadJsonResult<ResultsDTO>(response);

            return result;
        }

        public async Task<MovieDTO> GetMovieDetailsById(long movieId)
        {
            string APIKey = _configuration.GetValue<string>("APIKey");
            HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key={APIKey}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Couldn't get movie details for movie: {movieId}.");
                throw new Exception($"Couldn't get movie details for movie: {movieId}.");
            }
            var result = await ReadJsonResult<MovieDTO>(response);

            return result;
        }

        public async Task<ResultsDTO> GetSimilarMoviesById(long movieId)
        {
            string APIKey = _configuration.GetValue<string>("APIKey");
            HttpResponseMessage response = await client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/similar?api_key={APIKey}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Couldn't get similar movies for movie: {movieId}.");
                throw new Exception($"Couldn't get similar movies for movie: {movieId}.");
            }
            var result = await ReadJsonResult<ResultsDTO>(response);

            return result;
        }


        private async Task<T> ReadJsonResult<T>(HttpResponseMessage response)
        {
            using var sr = new StreamReader(await response.Content.ReadAsStreamAsync());
            using var jtr = new JsonTextReader(sr);
            T result = new JsonSerializer().Deserialize<T>(jtr);

            return result;
        }
    }
}
