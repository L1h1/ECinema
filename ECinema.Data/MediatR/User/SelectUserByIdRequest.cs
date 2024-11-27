using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECinema.Data.Entities;
namespace ECinema.Data.MediatR.User
{
    public class SelectUserByIdRequest : IRequest<Entities.User>
    {
        public int UserId { get; set; }
    }

}
