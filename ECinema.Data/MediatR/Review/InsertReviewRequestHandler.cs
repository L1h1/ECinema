using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Review
{
    public class InsertReviewRequestHandler : IRequestHandler<InsertReviewRequest, int>
    {
        private readonly MySqlConnection _connection;

        public InsertReviewRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Handle(InsertReviewRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ReviewQueries.InsertReviewQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@MovieId", request.MovieId);
            command.Parameters.AddWithValue("@UserId", request.UserId);
            command.Parameters.AddWithValue("@ReviewText", request.ReviewText);
            command.Parameters.AddWithValue("@ReviewRating", request.ReviewRating);
            command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);


            await command.ExecuteNonQueryAsync(cancellationToken);

            var insertIdQuery = "SELECT LAST_INSERT_ID();";
            using var idCommand = new MySqlCommand(insertIdQuery, _connection);
            var lastInsertId = (int)(long)await idCommand.ExecuteScalarAsync(cancellationToken);

            await _connection.CloseAsync();

            return lastInsertId;
        }
    }
}
