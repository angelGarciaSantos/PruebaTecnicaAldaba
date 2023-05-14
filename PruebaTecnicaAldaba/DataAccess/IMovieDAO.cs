using PruebaTecnicaAldaba.Models;
using System.Threading.Tasks;

namespace PruebaTecnicaAldaba.DataAccess
{
    public interface IMovieDAO
    {
        /// <summary>
        /// Obtains a list of movie results
        /// </summary>
        /// <param name="query">Keywords used to search the movie</param>
        /// <returns>A list of movie results</returns>
        public Task<ResultsDTO> GetMovieResultsByQuery(string query);

        /// <summary>
        /// Obtains the details of a specific movie
        /// </summary>
        /// <param name="movieId">The Id of the movie</param>
        /// <returns>The details of the movie</returns>
        public Task<MovieDTO> GetMovieDetailsById(long movieId);

        /// <summary>
        /// Obtains similar movies given a specific movie
        /// </summary>
        /// <param name="movieId">The Id of the movie</param>
        /// <returns>Similar movies given a specific movie</returns>
        public Task<ResultsDTO> GetSimilarMoviesById(long movieId);
    }
}
