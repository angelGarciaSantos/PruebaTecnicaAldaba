using PruebaTecnicaAldaba.Models;
using System.Threading.Tasks;

namespace PruebaTecnicaAldaba.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// Obtains info about a movie
        /// </summary>
        /// <param name="query">Keywords used to search the movie</param>
        /// <returns>The movie info</returns>
        public Task<Movie> GetMovieByQuery(string query);
    }
}
