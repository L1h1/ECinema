using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;

namespace ECinema.Data.MediatR.WatchHistory
{
    public class InsertWatchHistoryRequestHandler : IRequestHandler<InsertWatchHistoryRequest, int>
    {
        private readonly MySqlConnection _connection;

        public InsertWatchHistoryRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(InsertWatchHistoryRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = WatchHistoryQueries.InsertWatchHistoryQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", request.UserId);
            command.Parameters.AddWithValue("@MovieId", request.MovieId);
            command.Parameters.AddWithValue("@WatchedAt", request.WatchedAt);


            var insertedId = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));

            await _connection.CloseAsync();

            return insertedId;
        }
    }
}
