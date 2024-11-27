using MediatR;
using System.Collections.Generic;
using ECinema.Data.Entities;

namespace ECinema.Data.MediatR.Actor
{
    public class SelectAllActorsRequest : IRequest<List<Entities.Actor>>
    {
    }
}
