using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Cinema
{
    public class DeleteCinemaByIdRequestHandler : IRequestHandler<DeleteCinemaByIdRequest>
    {
        private readonly MySqlConnection _connection;

        public DeleteCinemaByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(DeleteCinemaByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CinemaQueries.DeleteCinemaByIdQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@CinemaId", request.CinemaId);

            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
