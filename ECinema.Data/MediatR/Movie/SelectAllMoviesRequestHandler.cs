using ECinema.Data.MediatR.Movie;
using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
namespace ECinema.Data.MediatR.Movie
{

    public class SelectAllMoviesRequestHandler : IRequestHandler<SelectAllMoviesRequest, List<Entities.Movie>>
    {
        private readonly MySqlConnection connection;

        public SelectAllMoviesRequestHandler(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public async Task<List<Entities.Movie>> Handle(SelectAllMoviesRequest request, CancellationToken cancellationToken)
        {
            var movies = new List<Entities.Movie>();

            await connection.OpenAsync(cancellationToken);

            using (var command = new MySqlCommand(MovieQueries.SelectAllMoviesQuery, connection))
            using (var reader = await command.ExecuteReaderAsync(cancellationToken))
            {
                while (await reader.ReadAsync(cancellationToken))
                {
                    movies.Add(new Entities.Movie
                    {
                        MovieId = (int)reader["MovieId"],
                        Title = (string)reader["Title"],
                        Description = (string)reader["Description"],
                        ReleaseYear = (int)reader["ReleaseYear"],
                        DurationMinutes = (int)reader["DurationMinutes"],
                        CreatedAt = (DateTime)reader["CreatedAt"],
                        TrailerUrl = (string)reader["TrailerUrl"],
                        StudioId = (int)reader["StudioId"]
                    });
                }
            }

            await connection.CloseAsync();

            return movies;
        }
    }

}