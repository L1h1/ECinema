using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Cinema
{
    public class SelectCinemaByIdRequestHandler : IRequestHandler<SelectCinemaByIdRequest, Entities.Cinema>
    {
        private readonly MySqlConnection _connection;

        public SelectCinemaByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Cinema> Handle(SelectCinemaByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = CinemaQueries.SelectCinemaByIdQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@CinemaId", request.CinemaId);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Cinema cinema = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                cinema = new Entities.Cinema
                {
                    CinemaId = (int)reader["CinemaId"],
                    CinemaName = (string)reader["CinemaName"],
                    WebsiteUrl = reader["WebsiteUrl"] != DBNull.Value ? (string)reader["WebsiteUrl"] : null
                };
            }

            await _connection.CloseAsync();

            return cinema;
        }
    }
}
