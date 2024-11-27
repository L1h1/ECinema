using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Review
{
    public class SelectReviewByIdRequestHandler : IRequestHandler<SelectReviewByIdRequest, Entities.Review>
    {
        private readonly MySqlConnection _connection;

        public SelectReviewByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Entities.Review> Handle(SelectReviewByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ReviewQueries.SelectReviewByIdQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@ReviewId", request.ReviewId);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            Entities.Review review = null;

            if (await reader.ReadAsync(cancellationToken))
            {
                review = new Entities.Review
                {
                    ReviewId = (int)reader["ReviewId"],
                    MovieId = (int)reader["MovieId"],
                    UserId = (int)reader["UserId"],
                    ReviewText = reader["ReviewText"] as string,
                    ReviewRating = (int)reader["ReviewRating"],
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };
            }

            await _connection.CloseAsync();

            return review;
        }
    }
}
