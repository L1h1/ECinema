using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Cinema
{
    public class InsertCinemaRequestHandler : IRequestHandler<InsertCinemaRequest>
    {
        private readonly MySqlConnection _connection;

        public InsertCinemaRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(InsertCinemaRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CinemaQueries.InsertCinemaQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@CinemaName", request.CinemaName);
            command.Parameters.AddWithValue("@WebsiteUrl", request.WebsiteUrl);

            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
