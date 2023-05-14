using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PruebaTecnicaAldaba.DataAccess;
using PruebaTecnicaAldaba.Services;
using System.IO;
using Xunit;

namespace PruebaTecnicaAldaba.Test
{
    public class MovieServiceTest
    {
        public IMovieService _movieService;
        public IMovieDAO _movieDAO;

        public MovieServiceTest()
        {
            // Creating instances of services, DAOs, etc.
            using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
                .SetMinimumLevel(LogLevel.Trace)
                .AddConsole());
            ILogger<MovieService> _loggerService = loggerFactory.CreateLogger<MovieService>();
            ILogger<MovieDAO> _loggerDAO = loggerFactory.CreateLogger<MovieDAO>();
            IConfigurationRoot _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            _movieDAO = new MovieDAO(_loggerDAO, _configuration);
            _movieService = new MovieService(_loggerService, _movieDAO);
        }

        [Fact]
        public void GetMovieByQueryException()
        {
            var result = _movieService.GetMovieByQuery(null);
            Assert.True(result.IsFaulted);
            Assert.Equal("One or more errors occurred. (A query must be specified.)", result.Exception.Message);
        }

        [Fact]
        public void GetMovieByQueryEmpty()
        {
            var result = _movieService.GetMovieByQuery("5856758");
            Assert.False(result.IsFaulted);
            Assert.Null( result.Result);
        }

        [Fact]
        public void GetMovieByQueryOk()
        {
            var result = _movieService.GetMovieByQuery("Titanic");
            Assert.False(result.IsFaulted);
            Assert.Equal( "Titanic", result.Result.Title);
        }
    }
}
