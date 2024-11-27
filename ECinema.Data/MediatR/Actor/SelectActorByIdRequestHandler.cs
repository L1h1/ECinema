using ECinema.Data.Queries;
using ECinema.Data.Entities;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Actor
{
    public class SelectActorByIdRequestHandler : IRequestHandler<SelectActorByIdRequest, Entities.Actor>
    {
        private readonly MySqlConnection _connection;

        public SelectActorByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Actor> Handle(SelectActorByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ActorQueries.SelectActorByIdQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ActorId", request.ActorId);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Actor actor = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                actor = new Entities.Actor
                {
                    ActorId = (int)reader["ActorId"],
                    ActorName = (string)reader["ActorName"],
                    DateOfBirth = reader["DateOfBirth"] as DateTime?,
                    Bio = reader["Bio"] as string
                };
            }

            await _connection.CloseAsync();

            return actor;
        }
    }
}
