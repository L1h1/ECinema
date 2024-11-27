using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.MovieActor
{
    public class InsertMovieActorRequestHandler : IRequestHandler<InsertMovieActorRequest, int>
    {
        private readonly MySqlConnection _connection;

        public InsertMovieActorRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(InsertMovieActorRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.InsertMovieActorQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@MovieId", request.MovieId);
            command.Parameters.AddWithValue("@ActorId", request.ActorId);

            var result = await command.ExecuteNonQueryAsync(cancellationToken); 

            await _connection.CloseAsync();

            return result;
        }
    }
}
