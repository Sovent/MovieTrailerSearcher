namespace MovieTrailersSearcher.Models
{
    public class MovieInfo
    {
        public MovieInfo(int movieId, string title, string description, int? year)
        {
            MovieId = movieId;
            Title = title;
            Description = description;
            Year = year;
        }

        public int MovieId { get; }

        public string Title { get; }

        public string Description { get; }

        public int? Year { get; }
    }
}