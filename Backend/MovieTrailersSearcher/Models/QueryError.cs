namespace MovieTrailersSearcher.Models
{
    public class QueryError
    {
        public QueryError(ErrorCode errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public ErrorCode ErrorCode { get; private set; }

        public string Message { get; private set; }
    }

    public enum ErrorCode
    {
        Unknown = 0,
        YoutubeError = 1100,
        TmdbError = 1200
    }
}