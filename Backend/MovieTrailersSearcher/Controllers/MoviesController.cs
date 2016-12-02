using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Journalist;
using MovieTrailersSearcher.Abstract;
using MovieTrailersSearcher.Models;

namespace MovieTrailersSearcher.Controllers
{
    public class MoviesController : ApiController
    {
        public MoviesController(IMovieInformationProvider movieInformationProvider)
        {
            Require.NotNull(movieInformationProvider, nameof(movieInformationProvider));

            _movieInformationProvider = movieInformationProvider;
        }

        [HttpGet]
        [Route("movies/search")]
        public async Task<IEnumerable<MovieInfo>> FindMoviesByTitle(string movieTitle)
        {
            Require.NotEmpty(movieTitle, nameof(movieTitle));

            var movies = await _movieInformationProvider.FindMoviesAsync(movieTitle);

            return movies;
        }

        [HttpGet]
        [Route("movies/{id}")]
        public async Task<HttpResponseMessage> GetMovieById(int id)
        {
            Require.Positive(id, nameof(id));

            var movie = await _movieInformationProvider.GetMovieWithTrailersAsync(id);
            return movie.Match(
                Request.CreateResponse,
                () => Request.CreateErrorResponse(HttpStatusCode.NotFound, "Movie was not found"));
        }

        private readonly IMovieInformationProvider _movieInformationProvider;
    }
}
