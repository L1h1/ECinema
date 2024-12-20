using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Movie
{
    public class UpdateMovieDetailsRequestHandler : IRequestHandler<UpdateMovieDetailsRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public UpdateMovieDetailsRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(UpdateMovieDetailsRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.UpdateMovieDetailsQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Description", request.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@ReleaseYear", request.ReleaseYear);
            command.Parameters.AddWithValue("@DurationMinutes", request.DurationMinutes);
            command.Parameters.AddWithValue("@MovieId", request.MovieId);
            command.Parameters.AddWithValue("@TrailerUrl", request.TrailerUrl);
            command.Parameters.AddWithValue("@StudioId", request.StudioId);

            var rowsAffected = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return rowsAffected > 0;
        }
    }
}
