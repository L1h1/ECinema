using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.MovieActor
{
    public class SelectMoviesByActorRequestHandler : IRequestHandler<SelectMoviesByActorRequest, List<Entities.Movie>>
    {
        private readonly MySqlConnection _connection;

        public SelectMoviesByActorRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Movie>> Handle(SelectMoviesByActorRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.SelectMoviesByActorQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ActorId", request.ActorId);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var movies = new List<Entities.Movie>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var movie = new Entities.Movie
                {
                    MovieId = (int)reader["MovieId"],
                    Title = (string)reader["Title"],
                    Description = reader["Description"] as string,
                    ReleaseYear = reader["ReleaseYear"] as int? ?? default,
                    DurationMinutes = reader["DurationMinutes"] as int? ?? default,
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
