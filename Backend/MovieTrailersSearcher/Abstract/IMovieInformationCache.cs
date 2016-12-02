using System.Collections.Generic;
using Journalist.Options;
using MovieTrailersSearcher.Models;

namespace MovieTrailersSearcher.Abstract
{
    public interface IMovieInformationCache
    {
        Option<IEnumerable<MovieInfo>> TryGetCachedMovieInfos(string query);

        Option<MovieWithTrailers> TryGetCachedMovieWithTrailers(int movieId);

        void CacheMovieInfos(string query, IEnumerable<MovieInfo> movieInfos);

        void CacheMovieWithTrailers(int movieId, MovieWithTrailers movie);
    }
}