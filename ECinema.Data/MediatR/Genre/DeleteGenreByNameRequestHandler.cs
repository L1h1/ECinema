using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Genre
{
    public class DeleteGenreByNameRequestHandler : IRequestHandler<DeleteGenreByNameRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public DeleteGenreByNameRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(DeleteGenreByNameRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = GenreQueries.DeleteGenreByNameQuery; 

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@GenreName", request.GenreName);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result > 0;
        }
    }

}
