using System;

namespace MovieTrailersSearcher.Models
{
    public class YoutubeVideo
    {
        public YoutubeVideo(string id, string title, Uri previewImage)
        {
            Id = id;
            Title = title;
            PreviewImage = previewImage;
        }

        public string Id { get; }

        public string Title { get; }

        public Uri PreviewImage { get; }
    }
}