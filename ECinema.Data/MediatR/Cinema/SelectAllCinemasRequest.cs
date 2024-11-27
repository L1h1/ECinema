using MediatR;
using System.Collections.Generic;

namespace ECinema.Data.MediatR.Cinema
{
    public class SelectAllCinemasRequest : IRequest<List<Entities.Cinema>>
    {
    }
}
