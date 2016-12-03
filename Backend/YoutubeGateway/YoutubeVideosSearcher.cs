using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Journalist;

namespace YoutubeGateway
{
    public class YoutubeVideosSearcher : IYoutubeVideosSearcher
    {
        public YoutubeVideosSearcher(string apiKey, int querySize)
        {
            Require.NotEmpty(apiKey, nameof(apiKey));
            Require.Positive(querySize, nameof(querySize));

            _apiKey = apiKey;
            _querySize = querySize;
        }

        public async Task<IEnumerable<YouTubeVideoPreview>> FindVideosByQueryAsync(string query)
        {
            Require.NotNull(query, nameof(query));

            var client = new YouTubeService(new BaseClientService.Initializer
            {
                ApiKey = _apiKey
            });

            var request = client.Search.List(SearchVideosOnlyAcceptablePart);
            request.Q = query;
            request.MaxResults = _querySize;

            try
            {
                var response = await request.ExecuteAsync();
                var previews = response.Items
                    .Where(item => item.Id.Kind == VideoKind)
                    .Select(item => new YouTubeVideoPreview(
                        item.Id.VideoId, 
                        item.Snippet.Title, 
                        GetThumbnail(item)));

                return previews;
            }
            catch (Exception exception)
            {
                throw new YoutubeAccessException(exception.Message, exception);
            }
        }

        private Uri GetThumbnail(SearchResult item)
        {
            return new Uri(
                item?.Snippet?.Thumbnails?.Maxres?.Url 
                ?? item?.Snippet?.Thumbnails?.High?.Url
                ?? item?.Snippet?.Thumbnails?.Standard?.Url
                ?? item?.Snippet?.Thumbnails?.Default__?.Url);    
        }

        private const string SearchVideosOnlyAcceptablePart = "snippet";
        private const string VideoKind = "youtube#video";
        private readonly string _apiKey;
        private readonly int _querySize;
    }
}