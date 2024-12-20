using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Studio
{
    internal class SelectStudioByIdRequestHandler : IRequestHandler<SelectStudioByIdRequest, Entities.Studio>
    {
        private readonly MySqlConnection _connection;
        public SelectStudioByIdRequestHandler(MySqlConnection _connection)
        {
            this._connection = _connection;
        }

        public async Task<Entities.Studio> Handle(SelectStudioByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = StudioQueries.SelectStudioByIdQuery;
            query = query.Replace("@StudioId", request.StudioId.ToString());

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Studio studio = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                studio = new Entities.Studio
                {
                    StudioId = (int)reader["StudioId"],
                    StudioName = (string)reader["StudioName"]
                };
            }

            await _connection.CloseAsync();

            return studio;
        }
    }
}
