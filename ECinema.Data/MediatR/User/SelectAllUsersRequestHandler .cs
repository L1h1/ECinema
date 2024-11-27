using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.User
{
    public class SelectAllUsersRequestHandler : IRequestHandler<SelectAllUsersQueryRequest, List<Entities.User>>
    {
        private readonly MySqlConnection _connection;

        public SelectAllUsersRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.User>> Handle(SelectAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.SelectAllUsersQuery;

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var users = new List<Entities.User>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var user = new Entities.User
                {
                    UserId = (int)reader["UserId"],
                    Username = reader["Username"] != DBNull.Value ? (string)reader["Username"] : null,
                    Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                    RoleId = (int)reader["RoleId"],
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };
                users.Add(user);
            }

            await _connection.CloseAsync();

            return users;
        }
    }
}
