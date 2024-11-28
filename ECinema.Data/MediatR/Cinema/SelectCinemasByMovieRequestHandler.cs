using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Cinema
{
    internal class SelectCinemasByMovieRequestHandler : IRequestHandler<SelectCinemasByMovieRequest, List<Entities.Cinema>>
    {
        private readonly MySqlConnection _connection;

        public SelectCinemasByMovieRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<Entities.Cinema>> Handle(SelectCinemasByMovieRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CinemaQueries.SelectCinemasByMovieQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue(@"MovieId", request.MovieId);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var cinemas = new List<Entities.Cinema>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var cinema = new Entities.Cinema
                {
                    CinemaId = (int)reader["CinemaId"],
                    CinemaName = (string)reader["CinemaName"],
                    WebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? (string)reader["WebsiteUrl"] : null
                };
                cinemas.Add(cinema);
            }

            await _connection.CloseAsync();

            return cinemas;
        }
    }
}
