using ECinema.Data.Entities;
using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Movie
{
    public class SelectMovieByIdRequestHandler : IRequestHandler<SelectMovieByIdRequest, Entities.Movie>
    {
        private readonly MySqlConnection _connection;

        public SelectMovieByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Movie> Handle(SelectMovieByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.SelectMovieByIdQuery;
            query = query.Replace("@MovieId", request.MovieId.ToString());

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Movie movie = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                movie = new Entities.Movie
                {
                    MovieId = (int)reader["MovieId"],
                    Title = (string)reader["Title"],
                    Description = reader["Description"] as string,
                    ReleaseYear = (int)reader["ReleaseYear"],
                    DurationMinutes = (int)reader["DurationMinutes"],
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };
            }

            await _connection.CloseAsync();

            return movie;
        }
    }
}
