using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Studio
{
    public class AddStudioRequestHandler : IRequestHandler<AddStudioRequest, int>
    {
        private readonly MySqlConnection _connection;

        public AddStudioRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(AddStudioRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = StudioQueries.InsertStudioQuery; 

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@StudioName", request.StudioName);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            if (result > 0)
            {
                var lastInsertedIdQuery = "SELECT LAST_INSERT_ID();";
                using var idCommand = new MySqlCommand(lastInsertedIdQuery, _connection);
                var lastInsertedId = await idCommand.ExecuteScalarAsync(cancellationToken);

                await _connection.CloseAsync();

                return Convert.ToInt32(lastInsertedId);  
            }

            await _connection.CloseAsync();

            throw new Exception("Failed to insert genre.");
        }
    }

}
