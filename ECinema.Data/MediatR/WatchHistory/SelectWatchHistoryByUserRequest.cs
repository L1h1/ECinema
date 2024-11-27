using MediatR;

namespace ECinema.Data.MediatR.WatchHistory
{
    public class SelectWatchHistoryByUserRequest : IRequest<List<Entities.WatchHistory>>
    {
        public int UserId { get; set; }

    }
}
