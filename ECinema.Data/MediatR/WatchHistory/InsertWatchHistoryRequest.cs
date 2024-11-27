using MediatR;

namespace ECinema.Data.MediatR.WatchHistory
{
    public class InsertWatchHistoryRequest : IRequest<int>
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public DateTime WatchedAt { get; set; }

    }
}
