using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.MovieCinema
{
    public class InsertMovieCinemaRequestHandler : IRequestHandler<InsertMovieCinemaRequest, int>
    {
        private readonly MySqlConnection _connection;

        public InsertMovieCinemaRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(InsertMovieCinemaRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.InsertMovieCinemaQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@MovieId", request.MovieId);
            command.Parameters.AddWithValue("@CinemaId", request.CinemaId);

            var affectedRows = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return affectedRows; 
        }
    }
}
