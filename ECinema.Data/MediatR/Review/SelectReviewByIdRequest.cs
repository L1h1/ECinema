using MediatR;
namespace ECinema.Data.MediatR.Review
{
    public class SelectReviewByIdRequest : IRequest<Entities.Review>
    {
        public int ReviewId { get; set; }

    }
}

