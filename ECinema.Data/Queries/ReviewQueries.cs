using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class ReviewQueries
    {
        public static string InsertReviewQuery = @"
        INSERT INTO Reviews (MovieId, UserId, ReviewText, ReviewRating, CreatedAt)
        VALUES (@MovieId, @UserId, @ReviewText, @ReviewRating, @CreatedAt);";

        public static string UpdateReviewQuery = @"
        UPDATE Reviews
        SET ReviewText = @ReviewText, ReviewRating = @ReviewRating, CreatedAt = @CreatedAt
        WHERE ReviewId = @ReviewId;";

        public static string DeleteReviewQuery = @"
        DELETE FROM Reviews 
        WHERE ReviewId = @ReviewId;";

        public static string SelectReviewsByMovieQuery = @"
        SELECT ReviewId, MovieId, UserId, ReviewText, ReviewRating, CreatedAt
        FROM Reviews 
        WHERE MovieId = @MovieId;";

        public static string SelectReviewByIdQuery = @"
        SELECT ReviewId, MovieId, UserId, ReviewText, ReviewRating, CreatedAt
        FROM Reviews
        WHERE ReviewId = @ReviewId;";

    }
}
