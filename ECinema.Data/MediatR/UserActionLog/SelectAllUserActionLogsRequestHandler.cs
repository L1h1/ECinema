using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.UserActionsLog
{
    public class SelectAllUserActionLogsRequestHandler : IRequestHandler<SelectAllUserActionLogsRequest, List<Entities.UserActionLog>>
    {
        private readonly MySqlConnection _connection;

        public SelectAllUserActionLogsRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.UserActionLog>> Handle(SelectAllUserActionLogsRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserActionLogQueries.SelectAllUserActionLogsQuery;

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var userActionLogs = new List<Entities.UserActionLog>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var userActionLog = new Entities.UserActionLog
                {
                    LogId = (int)reader["LogId"],
                    UserId = (int)reader["UserId"],
                    ActionType = (string)reader["ActionType"],
                    ActionDetails = reader["ActionDetails"] as string,
                    ActionTime = (DateTime)reader["ActionTime"]
                };

                userActionLogs.Add(userActionLog);
            }

            await _connection.CloseAsync();

            return userActionLogs;
        }
    }
}
