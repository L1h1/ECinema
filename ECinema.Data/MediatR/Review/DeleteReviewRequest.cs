using MediatR;

namespace ECinema.Data.MediatR.Review
{
    public class DeleteReviewRequest : IRequest
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
    }
}
