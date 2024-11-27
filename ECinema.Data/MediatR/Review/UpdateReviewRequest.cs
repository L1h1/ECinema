using MediatR;

namespace ECinema.Data.MediatR.Review
{
    public class UpdateReviewRequest : IRequest
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public string ReviewText { get; set; }
        public int ReviewRating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
