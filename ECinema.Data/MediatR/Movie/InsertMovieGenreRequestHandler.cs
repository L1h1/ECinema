using ECinema.Data.MediatR.Movie;
using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.MovieGenre
{
    public class InsertMovieGenreRequestHandler : IRequestHandler<InsertMovieGenreRequest, int>
    {
        private readonly MySqlConnection _connection;

        public InsertMovieGenreRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(InsertMovieGenreRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.InsertMovieGenreQuery
                .Replace("@MovieId", request.MovieId.ToString())
                .Replace("@GenreId", request.GenreId.ToString());

            using var command = new MySqlCommand(query, _connection);
            await command.ExecuteNonQueryAsync(cancellationToken);

            var insertedId = (int)command.LastInsertedId;

            await _connection.CloseAsync();

            return insertedId;
        }
    }
}
