using MediatR;
namespace ECinema.Data.MediatR.Movie
{
    public class SelectAllMoviesRequest : IRequest<List<Entities.Movie>>
    {
    }
}


