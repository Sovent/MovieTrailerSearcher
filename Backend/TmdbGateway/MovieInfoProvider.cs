using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journalist;
using Journalist.Options;
using TMDbLib.Client;

namespace TmdbGateway
{
    public class MovieInfoProvider : IMovieInfoProvider
    {
        public MovieInfoProvider(string apiKey, int maxSearchCount)
        {
            Require.NotEmpty(apiKey, nameof(apiKey));
            Require.Positive(maxSearchCount, nameof(maxSearchCount));

            _apiKey = apiKey;
            _maxSearchCount = maxSearchCount;
        }

        public async Task<IEnumerable<MovieInfo>> SearchMoviesByTitleAsync(string title)
        {
            Require.NotEmpty(title, nameof(title));

            try
            {
                var searchResult = await Client.SearchMovieAsync(title);

                var result = searchResult.Results
                    .Take(_maxSearchCount)
                    .Select(movie => new MovieInfo(
                        movie.Id, 
                        movie.OriginalTitle,
                        movie.ReleaseDate.ToOption(date => date.Year),
                        movie.Overview));
                return result;
            }
            catch (Exception exception)
            {
                throw new TmdbException(exception.Message, exception);
            }
        }

        public async Task<Option<MovieInfo>> GetMovieByIdAsync(int movieId)
        {
            Require.ZeroOrGreater(movieId, nameof(movieId));

            try
            {
                var movie = await Client.GetMovieAsync(movieId);

                var movieInfo = movie.MayBe()
                    .Select(m => new MovieInfo(
                        m.Id,
                        m.OriginalTitle,
                        m.ReleaseDate.ToOption(date => date.Year),
                        m.Overview));

                return movieInfo;
            }
            catch (Exception exception)
            {
                throw new TmdbException(exception.Message, exception);
            }
        }

        private TMDbClient Client => new TMDbClient(_apiKey);
        private readonly string _apiKey;
        private readonly int _maxSearchCount;
    }
}
