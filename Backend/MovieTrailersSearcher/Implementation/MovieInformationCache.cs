using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Journalist.Options;
using MovieTrailersSearcher.Abstract;
using MovieTrailersSearcher.Models;

namespace MovieTrailersSearcher.Implementation
{
    public class MovieInformationCache : IMovieInformationCache
    {
        public MovieInformationCache(long cacheExpirationTimeInSeconds)
        {
            _memoryCache = new MemoryCache(nameof(MovieInformationCache));
            _expirationTimeSpan = TimeSpan.FromSeconds(cacheExpirationTimeInSeconds);
        }

        public Option<IEnumerable<MovieInfo>> TryGetCachedMovieInfos(string query)
        {
            return _memoryCache
                .Get(BuildKeyForMovieInfos(query))
                .MayBe()
                .Select(item => item as IEnumerable<MovieInfo>);
        }

        public Option<MovieWithTrailers> TryGetCachedMovieWithTrailers(int movieId)
        {
            return _memoryCache
                .Get(BuildKeyForMovieWithTrailers(movieId))
                .MayBe()
                .Select(item => item as MovieWithTrailers);
        }

        public void CacheMovieInfos(string query, IEnumerable<MovieInfo> movieInfos)
        {
            _memoryCache.Set(BuildKeyForMovieInfos(query), movieInfos, GetValueExpirationDate());
        }

        public void CacheMovieWithTrailers(int movieId, MovieWithTrailers movie)
        {
            _memoryCache.Set(BuildKeyForMovieWithTrailers(movieId), movie, GetValueExpirationDate());
        }

        private DateTimeOffset GetValueExpirationDate()
        {
            return DateTimeOffset.Now.Add(_expirationTimeSpan);    
        }

        private static string BuildKeyForMovieInfos(string query)
        {
            return $"MovieInfos:{query}";
        }

        private static string BuildKeyForMovieWithTrailers(int movieId)
        {
            return $"MovieWithTrailers:{movieId}";
        }

        private readonly MemoryCache _memoryCache;
        private readonly TimeSpan _expirationTimeSpan;
    }
}