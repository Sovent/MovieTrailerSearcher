using System.Collections.Generic;
using System.Threading.Tasks;
using Journalist.Options;

namespace TmdbGateway
{
    public interface IMovieInfoProvider
    {
        Task<IEnumerable<MovieInfo>> SearchMoviesByTitleAsync(string title);

        Task<Option<MovieInfo>> GetMovieByIdAsync(int movieId);
    }
}
