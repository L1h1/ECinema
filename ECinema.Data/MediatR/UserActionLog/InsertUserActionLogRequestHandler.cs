using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.UserActionLog
{
    public class InsertUserActionLogRequestHandler : IRequestHandler<InsertUserActionLogRequest, int>
    {
        private readonly MySqlConnection _connection;

        public InsertUserActionLogRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(InsertUserActionLogRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserActionLogQueries.InsertUserActionLogQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", request.UserId);
            command.Parameters.AddWithValue("@ActionType", request.ActionType);
            command.Parameters.AddWithValue("@ActionDetails", request.ActionDetails);
            command.Parameters.AddWithValue("@ActionTime", request.ActionTime);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result;
        }
    }
}
