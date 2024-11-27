using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Movie
{
    public class DeleteMovieByIdRequestHandler : IRequestHandler<DeleteMovieByIdRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public DeleteMovieByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(DeleteMovieByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.DeleteMovieByIdQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@MovieId", request.MovieId);

            var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return rowsAffected > 0;
        }
    }
}
