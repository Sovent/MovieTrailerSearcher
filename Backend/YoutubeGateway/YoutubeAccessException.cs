using System;
using System.Runtime.Serialization;

namespace YoutubeGateway
{
    [Serializable]
    public class YoutubeAccessException : Exception
    {
        public YoutubeAccessException()
        {
        }

        public YoutubeAccessException(string message) : base(message)
        {
        }

        public YoutubeAccessException(string message, Exception inner) : base(message, inner)
        {
        }

        protected YoutubeAccessException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}