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
    public class SelectUserByIdRequestHandler : IRequestHandler<SelectUserByIdRequest, Entities.User>
    {
        private readonly MySqlConnection _connection;

        public SelectUserByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.User> Handle(SelectUserByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.SelectUserByIdQuery; 
            query = query.Replace("@UserId", request.UserId.ToString());  

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.User? user = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                user = new Entities.User
                {
                    UserId = (int)reader["UserId"],
                    Username = (string)reader["Username"],
                    Email = (string)reader["Email"],
                    RoleId = (int)reader["RoleId"],
                    CreatedAt = (DateTime)reader["CreatedAt"]        
                };
            }

            await _connection.CloseAsync();

            return user;
        }
    }

}
