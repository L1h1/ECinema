using MediatR;

namespace ECinema.Data.MediatR.Role
{
    public class SelectAllRolesRequest : IRequest<List<Entities.Role>>
    {
    }
}
