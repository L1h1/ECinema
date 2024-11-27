using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Review
{
    public class UpdateReviewRequestHandler : IRequestHandler<UpdateReviewRequest>
    {
        private readonly MySqlConnection _connection;

        public UpdateReviewRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(UpdateReviewRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ReviewQueries.UpdateReviewQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ReviewText", request.ReviewText);
            command.Parameters.AddWithValue("@ReviewRating", request.ReviewRating);
            command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);
            command.Parameters.AddWithValue("@ReviewId", request.ReviewId);


            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
