using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Genre
{
    public class DeleteGenreByIdRequest : IRequest<bool>
    {
        public int GenreId { get; set; }
    }
}
