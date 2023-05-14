using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PruebaTecnicaAldaba.Models;
using PruebaTecnicaAldaba.Services;
using System.Threading.Tasks;

namespace PruebaTecnicaAldaba.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, IMovieService MovieService)
        {
            _logger = logger;
            _movieService = MovieService;
        }

        /// <summary>
        /// Obtains info about a movie
        /// </summary>
        /// <param name="query">Keywords used to search the movie</param>
        /// <returns>The movie info</returns>
        [HttpGet]
        public async Task<Movie> GetAsync(string query)
        {
            var result = await _movieService.GetMovieByQuery(query);

            return result;
        }
    }
}
