using ECinema.Data.Entities;
using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Movie
{
    public class SelectMovieByTitleRequestHandler : IRequestHandler<SelectMovieByTitleRequest, List<Entities.Movie>>
    {
        private readonly MySqlConnection _connection;

        public SelectMovieByTitleRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Movie>> Handle(SelectMovieByTitleRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = MovieQueries.SelectMovieByNameQuery;

            query = query.Replace("@Title", request.Title);
            using var command = new MySqlCommand(query, _connection);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            List<Entities.Movie> movies = new();

            while (await reader.ReadAsync(cancellationToken))
            {
                movies.Add(new Entities.Movie
                {
                    MovieId = (int)reader["MovieId"],
                    Title = (string)reader["Title"],
                    Description = reader["Description"] as string,
                    ReleaseYear = (int)reader["ReleaseYear"],
                    DurationMinutes = (int)reader["DurationMinutes"],
                    CreatedAt = (DateTime)reader["CreatedAt"],
                    TrailerUrl = (string)reader["TrailerUrl"],
                    StudioId = (int)reader["StudioId"]
                });
            }


            await _connection.CloseAsync();

            return movies;
        }
    }
}
