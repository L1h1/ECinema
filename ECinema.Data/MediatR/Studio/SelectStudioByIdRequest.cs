using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Studio
{
    public class SelectStudioByIdRequest :IRequest<Entities.Studio>
    {
        public int StudioId { get;set; }
    }
}
