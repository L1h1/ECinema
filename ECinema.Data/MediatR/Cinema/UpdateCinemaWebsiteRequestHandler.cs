using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Cinema
{
    public class UpdateCinemaWebsiteRequestHandler : IRequestHandler<UpdateCinemaWebsiteRequest>
    {
        private readonly MySqlConnection _connection;

        public UpdateCinemaWebsiteRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(UpdateCinemaWebsiteRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CinemaQueries.UpdateCinemaWebsiteQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@CinemaId", request.CinemaId);
            command.Parameters.AddWithValue("@WebsiteUrl", request.WebsiteUrl);

            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
