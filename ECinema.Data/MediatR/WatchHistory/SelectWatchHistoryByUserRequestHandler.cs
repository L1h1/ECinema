using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;

namespace ECinema.Data.MediatR.WatchHistory
{
    public class SelectWatchHistoryByUserRequestHandler : IRequestHandler<SelectWatchHistoryByUserRequest, List<Entities.WatchHistory>>
    {
        private readonly MySqlConnection _connection;

        public SelectWatchHistoryByUserRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.WatchHistory>> Handle(SelectWatchHistoryByUserRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = WatchHistoryQueries.SelectWatchHistoryByUserQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", request.UserId);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var watchHistoryList = new List<Entities.WatchHistory>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var watchHistory = new Entities.WatchHistory
                {
                    WatchId = (int)reader["WatchId"],
                    UserId = (int)reader["UserId"],
                    MovieId = (int)reader["MovieId"],
                    WatchedAt = (DateTime)reader["WatchedAt"],
                    Title = (string)reader["Title"],
                };

                watchHistoryList.Add(watchHistory);
            }

            await _connection.CloseAsync();

            return watchHistoryList;
        }
    }
}
