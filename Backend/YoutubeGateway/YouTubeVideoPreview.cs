using System;
using Journalist;

namespace YoutubeGateway
{
    public class YouTubeVideoPreview
    {
        public YouTubeVideoPreview(string id, string name, Uri previewImage)
        {
            Require.NotEmpty(id, nameof(id));
            Require.NotEmpty(name, nameof(name));
            Require.NotNull(previewImage, nameof(previewImage));

            Id = id;
            Name = name;
            PreviewImage = previewImage;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public Uri PreviewImage { get; private set; }
    }
}