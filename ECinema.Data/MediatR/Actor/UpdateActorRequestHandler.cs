using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Actor
{
    public class UpdateActorRequestHandler : IRequestHandler<UpdateActorRequest>
    {
        private readonly MySqlConnection _connection;

        public UpdateActorRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(UpdateActorRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ActorQueries.UpdateActorQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ActorId", request.ActorId);
            command.Parameters.AddWithValue("@ActorName", request.ActorName);
            command.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth.HasValue ? (object)request.DateOfBirth.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Bio", request.Bio);

            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
