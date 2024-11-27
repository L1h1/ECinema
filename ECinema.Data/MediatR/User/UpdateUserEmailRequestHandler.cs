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
    public class UpdateUserEmailRequestHandler : IRequestHandler<UpdateUserEmailRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public UpdateUserEmailRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(UpdateUserEmailRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.UpdateUserEmailQuery; 


            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", request.UserId.ToString());
            command.Parameters.AddWithValue("@Email", request.NewEmail);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result > 0;
        }
    }

}
