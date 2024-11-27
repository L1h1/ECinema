using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;

namespace ECinema.Data.MediatR.WatchHistory
{
    public class DeleteWatchHistoryRequestHandler : IRequestHandler<DeleteWatchHistoryRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public DeleteWatchHistoryRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(DeleteWatchHistoryRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = WatchHistoryQueries.DeleteWatchHistoryQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@WatchId", request.WatchId);

            var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return rowsAffected > 0;
        }
    }
}
