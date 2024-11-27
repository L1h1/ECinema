using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Cinema
{
    public class SelectAllCinemasRequestHandler : IRequestHandler<SelectAllCinemasRequest, List<Entities.Cinema>>
    {
        private readonly MySqlConnection _connection;

        public SelectAllCinemasRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Cinema>> Handle(SelectAllCinemasRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CinemaQueries.SelectAllCinemasQuery;

            using var command = new MySqlCommand(query, _connection);
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
