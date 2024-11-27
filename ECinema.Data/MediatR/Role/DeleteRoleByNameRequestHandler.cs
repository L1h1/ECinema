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
    public class DeleteRoleByNameRequestHandler : IRequestHandler<DeleteRoleByNameRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public DeleteRoleByNameRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(DeleteRoleByNameRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = RoleQueries.DeleteRoleByNameQuery;  
            query = query.Replace("@RoleName",$"'{request.RoleName}'");  

            using var command = new MySqlCommand(query, _connection);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result > 0;
        }
    }

}
