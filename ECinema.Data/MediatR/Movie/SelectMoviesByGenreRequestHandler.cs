using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Movie
{
    public class SelectMoviesByGenreRequestHandler : IRequestHandler<SelectMoviesByGenreRequest, List<Entities.Movie>>
    {
        private readonly MySqlConnection _connection;

        public SelectMoviesByGenreRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Movie>> Handle(SelectMoviesByGenreRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.SelectMoviesByGenreQuery
                .Replace("@GenreId", request.GenreId.ToString());

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var movies = new List<Entities.Movie>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var movie = new Entities.Movie
                {
                    MovieId = (int)reader["MovieId"],
                    Title = (string)reader["Title"],
                    Description = reader["Description"] as string,
                    ReleaseYear = (int)reader["ReleaseYear"],
                    DurationMinutes = (int)reader["DurationMinutes"],
                    CreatedAt = (DateTime)reader["CreatedAt"],
                    TrailerUrl = (string)reader["TrailerUrl"]
                };

                movies.Add(movie);
            }

            await _connection.CloseAsync();

            return movies;
        }
    }
}
