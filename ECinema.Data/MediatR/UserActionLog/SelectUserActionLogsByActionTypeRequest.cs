using MediatR;

namespace ECinema.Data.MediatR.UserActionLog
{
    public class SelectUserActionLogsByActionTypeRequest : IRequest<List<Entities.UserActionLog>>
    {
        public string ActionType { get; set; }

    }
}
