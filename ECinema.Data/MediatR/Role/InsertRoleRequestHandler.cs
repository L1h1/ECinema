using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Role
{
    public class InsertRoleRequestHandler : IRequestHandler<InsertRoleRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public InsertRoleRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(InsertRoleRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = RoleQueries.InsertRoleQuery; 
            query = query.Replace("@RoleName", $"'{request.RoleName}'"); 

            using var command = new MySqlCommand(query, _connection);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result > 0;
        }
    }

}
