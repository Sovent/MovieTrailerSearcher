using System.Collections.Generic;
using System.Threading.Tasks;
using Journalist.Options;
using MovieTrailersSearcher.Models;

namespace MovieTrailersSearcher.Abstract
{
    public interface IMovieInformationProvider
    {
        Task<IEnumerable<MovieInfo>> FindMoviesAsync(string query);

        Task<Option<MovieWithTrailers>> GetMovieWithTrailersAsync(int movieId);
    }
}