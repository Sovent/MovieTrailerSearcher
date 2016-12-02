using System.Collections.Generic;
using System.Linq;
using MovieTrailersSearcher.Models;
using YoutubeGateway;

namespace MovieTrailersSearcher.Implementation
{
    public static class EntityMapper
    {
        public static MovieInfo ToModel(this TmdbGateway.MovieInfo movieInfo)
        {
            return new MovieInfo(
                movieInfo.TmdbMovieId,
                movieInfo.Name,
                movieInfo.Description,
                movieInfo.Year.Match(year => (int?)year, () => null));
        }

        public static MovieWithTrailers ToModel(
            this TmdbGateway.MovieInfo movieInfo, 
            IEnumerable<YouTubeVideoPreview> youTubeVideoPreviews)
        {
            var youTubeVideos = youTubeVideoPreviews.Select(preview => new YoutubeVideo(
                preview.Id, preview.Name, preview.PreviewImage));
            return new MovieWithTrailers(movieInfo.ToModel(), youTubeVideos);
        }
    }
}