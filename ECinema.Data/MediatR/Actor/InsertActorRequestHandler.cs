using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Actor
{
    public class InsertActorRequestHandler : IRequestHandler<InsertActorRequest, int>
    {
        private readonly MySqlConnection _connection;

        public InsertActorRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(InsertActorRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ActorQueries.InsertActorQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ActorName", request.ActorName);
            command.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth.HasValue ? (object)request.DateOfBirth.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Bio", request.Bio);


            await command.ExecuteNonQueryAsync(cancellationToken);


            command.CommandText = "SELECT LAST_INSERT_ID();";
            var result = await command.ExecuteScalarAsync(cancellationToken);

            await _connection.CloseAsync();

            return Convert.ToInt32(result);  //Id нового актера
        }
    }
}


