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
    public class DeleteGenreByIdRequestHandler : IRequestHandler<DeleteGenreByIdRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public DeleteGenreByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(DeleteGenreByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = GenreQueries.DeleteGenreByIdQuery; 
            query = query.Replace("@GenreId", request.GenreId.ToString());  

            using var command = new MySqlCommand(query, _connection);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result > 0;
        }
    }

}
