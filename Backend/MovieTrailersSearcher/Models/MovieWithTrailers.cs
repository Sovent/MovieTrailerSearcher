using System.Collections.Generic;

namespace MovieTrailersSearcher.Models
{
    public class MovieWithTrailers
    {
        public MovieWithTrailers(MovieInfo movieInfo, IEnumerable<YoutubeVideo> youtubeVideos)
        {
            MovieInfo = movieInfo;
            YoutubeVideos = youtubeVideos;
        }

        public MovieInfo MovieInfo { get; private set; }

        public IEnumerable<YoutubeVideo> YoutubeVideos { get; private set; }
    }
}