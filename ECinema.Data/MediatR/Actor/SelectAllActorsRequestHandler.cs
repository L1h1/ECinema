using ECinema.Data.Queries;
using ECinema.Data.Entities;
using MediatR;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Actor
{
    public class SelectAllActorsRequestHandler : IRequestHandler<SelectAllActorsRequest, List<Entities.Actor>>
    {
        private readonly MySqlConnection _connection;

        public SelectAllActorsRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Actor>> Handle(SelectAllActorsRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ActorQueries.SelectAllActorsQuery;

            using var command = new MySqlCommand(query, _connection);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var actors = new List<Entities.Actor>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var actor = new Entities.Actor
                {
                    ActorId = (int)reader["ActorId"],
                    ActorName = (string)reader["ActorName"],
                    DateOfBirth = reader["DateOfBirth"] as DateTime?,
                    Bio = reader["Bio"] as string
                };
                actors.Add(actor);
            }

            await _connection.CloseAsync();

            return actors;
        }
    }
}
