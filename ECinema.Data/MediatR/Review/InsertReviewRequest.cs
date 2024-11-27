using MediatR;

namespace ECinema.Data.MediatR.Review
{
    public class InsertReviewRequest : IRequest<int> 
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        public int ReviewRating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
