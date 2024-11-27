using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.MovieGenre
{
    public class DeleteMovieGenreRequestHandler : IRequestHandler<DeleteMovieGenreRequest>
    {
        private readonly MySqlConnection _connection;

        public DeleteMovieGenreRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(DeleteMovieGenreRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.DeleteMovieGenreQuery
                .Replace("@MovieId", request.MovieId.ToString())
                .Replace("@GenreId", request.GenreId.ToString());

            using var command = new MySqlCommand(query, _connection);
            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
