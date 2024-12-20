using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Studio
{
    public class SelectAllStudiosRequestHandler : IRequestHandler<SelectAllStudiosRequest, List<Entities.Studio>>
    {
        private readonly MySqlConnection _connection;

        public SelectAllStudiosRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Studio>> Handle(SelectAllStudiosRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = StudioQueries.SelectAllStudiosQuery; 

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var studios = new List<Entities.Studio>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var studio = new Entities.Studio
                {
                    StudioId = (int)reader["StudioId"],
                    StudioName = reader["StudioName"] != DBNull.Value ? (string)reader["StudioName"] : null
                };
                studios.Add(studio);
            }

            await _connection.CloseAsync();

            return studios;
        }
    }
}
