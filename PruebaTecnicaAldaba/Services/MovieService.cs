using Microsoft.Extensions.Logging;
using PruebaTecnicaAldaba.DataAccess;
using PruebaTecnicaAldaba.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PruebaTecnicaAldaba.Services
{
    public class MovieService : IMovieService
    {
        private readonly ILogger<MovieService> _logger;
        private static readonly HttpClient _client = new HttpClient();
        private readonly IMovieDAO _movieDAO;

        public MovieService(ILogger<MovieService> logger, IMovieDAO MovieDAO)
        {
            _logger = logger;
            _movieDAO = MovieDAO;
        }

        public async Task<Movie> GetMovieByQuery(string? query)
        {
            Movie movie = null;
            if (query == null)
            {
                _logger.LogError("A query must be specified.");
                throw new Exception("A query must be specified.");
            }
            try {
                var movieQueryResults = await _movieDAO.GetMovieResultsByQuery(query);
                if (movieQueryResults.Results.Count() > 0)
                {
                    var movieId = movieQueryResults.Results.First().Id;
                    var movieDetails = await _movieDAO.GetMovieDetailsById(movieId);
                    var similarMoviesResults = await _movieDAO.GetSimilarMoviesById(movieId);
                    var similarMovies = ProcessSimilarMovies(similarMoviesResults);

                    movie = new Movie
                    {
                        Title = movieDetails.Title,
                        OriginalTitle = movieDetails.OriginalTitle,
                        AVGRating = movieDetails.VoteAverage,
                        ReleaseDate = movieDetails.ReleaseDate,
                        Overview = movieDetails.Overview,
                        SimilarMovies = similarMovies
                    };
                }
                         
            }
            catch (Exception)
            {
                _logger.LogError("Error trying to get movie by query.");
                throw;
            }

            return movie;
        }

        private string ProcessSimilarMovies(ResultsDTO results)
        {
            string similarMovies = "";
            // Getting only the first 5 results
            var resultsSelected = results.Results.Take(5).ToArray();
            foreach (MovieDTO result in resultsSelected)
            {
                similarMovies += $"{result.Title} ({result.ReleaseDate.Value.Year}), ";
            }

            return similarMovies.Count() > 0 ? similarMovies[0..^2] : similarMovies;
        }
    }
}
