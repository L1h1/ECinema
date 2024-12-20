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
    internal class UpdateStudioNameRequestHandler : IRequestHandler<UpdateStudioNameRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public UpdateStudioNameRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<bool> Handle(UpdateStudioNameRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);
            var query = StudioQueries.UpdateStudioNameQuery;
            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@StudioId", request.StudioId);
            command.Parameters.AddWithValue("@StudioName",request.StudioName);

            var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return rowsAffected > 0;
        }
    }
}
