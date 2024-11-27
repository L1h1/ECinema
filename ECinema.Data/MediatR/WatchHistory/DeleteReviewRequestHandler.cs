using ECinema.Data.MediatR.Review;
using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.WatchHistory
{
    public class DeleteReviewRequestHandler : IRequestHandler<DeleteReviewRequest>
    {
        private readonly MySqlConnection _connection;

        public DeleteReviewRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task Handle(DeleteReviewRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ReviewQueries.DeleteReviewQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ReviewId", request.ReviewId);

            await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();
        }
    }
}
