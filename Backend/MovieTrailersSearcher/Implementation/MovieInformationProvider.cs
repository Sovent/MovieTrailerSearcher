using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journalist;
using Journalist.Options;
using MovieTrailersSearcher.Abstract;
using MovieTrailersSearcher.Models;
using TmdbGateway;
using YoutubeGateway;
using MovieInfo = MovieTrailersSearcher.Models.MovieInfo;

namespace MovieTrailersSearcher.Implementation
{
    public class MovieInformationProvider : IMovieInformationProvider
    {
        public MovieInformationProvider(
            IMovieInformationCache cache, 
            IMovieInfoProvider movieInfoProvider,
            IYoutubeVideosSearcher youtubeVideosSearcher)
        {
            Require.NotNull(cache, nameof(cache));
            Require.NotNull(movieInfoProvider, nameof(movieInfoProvider));
            Require.NotNull(youtubeVideosSearcher, nameof(youtubeVideosSearcher));

            _cache = cache;
            _movieInfoProvider = movieInfoProvider;
            _youtubeVideosSearcher = youtubeVideosSearcher;
        }

        public async Task<IEnumerable<MovieInfo>> FindMoviesAsync(string query)
        {
            Require.NotEmpty(query, nameof(query));

            var cachedValue = _cache.TryGetCachedMovieInfos(query);
            if (cachedValue.IsSome)
            {
                return cachedValue.GetOrDefault(Enumerable.Empty<MovieInfo>);
            }

            var movieInfos = await _movieInfoProvider.SearchMoviesByTitleAsync(query);
            var movieInfosModels = movieInfos.Select(info => info.ToModel());

            _cache.CacheMovieInfos(query, movieInfosModels);
            return movieInfosModels;
        }

        public async Task<Option<MovieWithTrailers>> GetMovieWithTrailersAsync(int movieId)
        {
            Require.Positive(movieId, nameof(movieId));

            var cachedValue = _cache.TryGetCachedMovieWithTrailers(movieId);
            if (cachedValue.IsSome)
            {
                return cachedValue;
            }

            var findMovieResult = await _movieInfoProvider.GetMovieByIdAsync(movieId);
            if (findMovieResult.IsNone)
            {
                return Option.None();
            }

            var movie = findMovieResult.GetOrDefault(() => null);
            var query = BuildTrailersSearchQuery(movie.Name);
            var videos = await _youtubeVideosSearcher.FindVideosByQueryAsync(query);
            var movieWithTrailers = movie.ToModel(videos);

            _cache.CacheMovieWithTrailers(movieId, movieWithTrailers);

            return movieWithTrailers.MayBe();
        }

        private static string BuildTrailersSearchQuery(string movieTitle)
        {
            var query = $"{movieTitle} official trailer";
            return query;
        }

        private readonly IMovieInformationCache _cache;
        private readonly IMovieInfoProvider _movieInfoProvider;
        private readonly IYoutubeVideosSearcher _youtubeVideosSearcher;
    }
}