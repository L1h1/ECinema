using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Cinema
{
    public class UpdateCinemaNameRequestHandler : IRequestHandler<UpdateCinemaNameRequest>
    {
        private readonly MySqlConnection _connection;

        public UpdateCinemaNameRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(UpdateCinemaNameRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CinemaQueries.UpdateCinemaNameQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@CinemaId", request.CinemaId);
            command.Parameters.AddWithValue("@CinemaName", request.CinemaName);

            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
