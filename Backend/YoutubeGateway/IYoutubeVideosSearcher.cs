using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeGateway
{
    public interface IYoutubeVideosSearcher
    {
        Task<IEnumerable<YouTubeVideoPreview>> FindVideosByQueryAsync(string query);
    }
}