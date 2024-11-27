using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.MovieCinema
{
    public class DeleteMovieCinemaRequestHandler : IRequestHandler<DeleteMovieCinemaRequest, int>
    {
        private readonly MySqlConnection _connection;

        public DeleteMovieCinemaRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(DeleteMovieCinemaRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.DeleteMovieCinemaQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@MovieId", request.MovieId);
            command.Parameters.AddWithValue("@CinemaId", request.CinemaId);

            var affectedRows = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return affectedRows; // Returns the number of rows deleted
        }
    }
}
