using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Actor
{
    public class DeleteActorRequestHandler : IRequestHandler<DeleteActorRequest>
    {
        private readonly MySqlConnection _connection;

        public DeleteActorRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(DeleteActorRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ActorQueries.DeleteActorQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ActorId", request.ActorId);

            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
