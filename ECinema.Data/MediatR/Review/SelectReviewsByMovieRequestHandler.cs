using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Review
{
    public class SelectReviewsByMovieRequestHandler : IRequestHandler<SelectReviewsByMovieRequest, List<Entities.Review>>
    {
        private readonly MySqlConnection _connection;

        public SelectReviewsByMovieRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Entities.Review>> Handle(SelectReviewsByMovieRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ReviewQueries.SelectReviewsByMovieQuery;

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@MovieId", request.MovieId);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var reviews = new List<Entities.Review>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var review = new Entities.Review
                {
                    ReviewId = (int)reader["ReviewId"],
                    MovieId = (int)reader["MovieId"],
                    UserId = (int)reader["UserId"],
                    ReviewText = (string)reader["ReviewText"],
                    ReviewRating = (int)reader["ReviewRating"],
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };

                reviews.Add(review);
            }

            await _connection.CloseAsync();

            return reviews;
        }
    }
}
