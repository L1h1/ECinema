using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Movie
{
    public class InsertMovieRequestHandler : IRequestHandler<InsertMovieRequest, int>
    {
        private readonly MySqlConnection _connection;

        public InsertMovieRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(InsertMovieRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.InsertMovieQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Title", request.Title);
            command.Parameters.AddWithValue("@Description", request.Description);
            command.Parameters.AddWithValue("@ReleaseYear", request.ReleaseYear);
            command.Parameters.AddWithValue("@DurationMinutes", request.DurationMinutes);

            await command.ExecuteNonQueryAsync(cancellationToken);
            var insertedId = (int)command.LastInsertedId;

            await _connection.CloseAsync();

            return insertedId;
        }
    }
}
