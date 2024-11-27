using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Genre
{
    public class SelectAllGenresRequestHandler : IRequestHandler<SelectAllGenresRequest, List<Entities.Genre>>
    {
        private readonly MySqlConnection _connection;

        public SelectAllGenresRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Genre>> Handle(SelectAllGenresRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = GenreQueries.SelectAllGenresQuery; 

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var genres = new List<Entities.Genre>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var genre = new Entities.Genre
                {
                    GenreId = (int)reader["GenreId"],
                    GenreName = reader["GenreName"] != DBNull.Value ? (string)reader["GenreName"] : null
                };
                genres.Add(genre);
            }

            await _connection.CloseAsync();

            return genres;
        }
    }
}
