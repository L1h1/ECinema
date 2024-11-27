using MediatR;

namespace ECinema.Data.MediatR.WatchHistory
{
    public class DeleteWatchHistoryRequest : IRequest<bool> 
    {
        public int WatchId { get; set; }
        public int UserId { get; set; }
    }
}
