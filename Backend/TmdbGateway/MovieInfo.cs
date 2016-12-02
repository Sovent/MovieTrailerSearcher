using Journalist;
using Journalist.Options;

namespace TmdbGateway
{
    public class MovieInfo
    {
        public MovieInfo(int tmdbMovieId, string name, Option<int> year, string description)
        {
            Require.Positive(tmdbMovieId, nameof(tmdbMovieId));
            Require.NotEmpty(name, nameof(name));
            Require.NotNull(description, nameof(description));

            TmdbMovieId = tmdbMovieId;
            Name = name;
            Year = year;
            Description = description;
        }

        public int TmdbMovieId { get; private set; }

        public string Name { get; private set; }

        public Option<int> Year { get; private set; }

        public string Description { get; private set; }
    }
}
