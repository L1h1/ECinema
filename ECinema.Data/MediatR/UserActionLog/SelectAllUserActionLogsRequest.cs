using MediatR;

namespace ECinema.Data.MediatR.UserActionsLog
{
    public class SelectAllUserActionLogsRequest : IRequest<List<Entities.UserActionLog>>
    {
    }
}

