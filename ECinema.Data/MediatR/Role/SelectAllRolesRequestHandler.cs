using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Role
{
    public class SelectAllRolesRequestHandler : IRequestHandler<SelectAllRolesRequest, List<Entities.Role>>
    {
        private readonly MySqlConnection _connection;

        public SelectAllRolesRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Role>> Handle(SelectAllRolesRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = RoleQueries.SelectAllRolesQuery;

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var roles = new List<Entities.Role>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var role = new Entities.Role
                {
                    RoleId = (int)reader["RoleId"],
                    RoleName = (string)reader["RoleName"]
                };

                roles.Add(role);
            }

            await _connection.CloseAsync();

            return roles;
        }
    }
}
