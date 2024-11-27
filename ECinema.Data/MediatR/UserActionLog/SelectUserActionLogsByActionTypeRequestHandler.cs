using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;

namespace ECinema.Data.MediatR.UserActionLog
{
    public class SelectUserActionLogsByActionTypeRequestHandler : IRequestHandler<SelectUserActionLogsByActionTypeRequest, List<Entities.UserActionLog>>
    {
        private readonly MySqlConnection _connection;

        public SelectUserActionLogsByActionTypeRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.UserActionLog>> Handle(SelectUserActionLogsByActionTypeRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserActionLogQueries.SelectUserActionLogsByActionTypeQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ActionType", request.ActionType);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var userActionLogs = new List<Entities.UserActionLog>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var userActionLog = new Entities.UserActionLog
                {
                    LogId = (int)reader["LogId"],
                    UserId = (int)reader["UserId"],
                    ActionType = (string)reader["ActionType"],
                    ActionDetails = (string)reader["ActionDetails"],
                    ActionTime = (DateTime)reader["ActionTime"]
                };
                userActionLogs.Add(userActionLog);
            }

            await _connection.CloseAsync();

            return userActionLogs;
        }
    }
}
