using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.User
{
    public class DeleteUserByIdRequestHandler : IRequestHandler<DeleteUserByIdRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public DeleteUserByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.DeleteUserByIdQuery;  
            query = query.Replace("@UserId", request.UserId.ToString());  

            using var command = new MySqlCommand(query, _connection);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result > 0;
        }
    }

}
