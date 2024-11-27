using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.MovieCinema
{
    public class SelectMoviesByCinemaRequestHandler : IRequestHandler<SelectMoviesByCinemaRequest, List<Entities.Movie>>
    {
        private readonly MySqlConnection _connection;

        public SelectMoviesByCinemaRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Movie>> Handle(SelectMoviesByCinemaRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.SelectMoviesByCinemaQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@MovieId", request.CinemaId);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var movies = new List<Entities.Movie>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var movie = new Entities.Movie
                {
                    MovieId = (int)reader["MovieId"],
                    Title = (string)reader["Title"],
                    Description = reader["Description"] as string, 
                    ReleaseYear = reader["ReleaseYear"] as int? ?? 0, 
                    DurationMinutes = reader["DurationMinutes"] as int? ?? 0, 
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };

                movies.Add(movie);
            }

            await _connection.CloseAsync();

            return movies; 
        }
    }
}
