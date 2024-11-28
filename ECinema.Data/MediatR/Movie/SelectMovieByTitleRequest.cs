using ECinema.Data.Entities;
using MediatR;

namespace ECinema.Data.MediatR.Movie
{
    public class SelectMovieByTitleRequest : IRequest<Entities.Movie>
    {
        public string Title { get; set; }
    }
}
