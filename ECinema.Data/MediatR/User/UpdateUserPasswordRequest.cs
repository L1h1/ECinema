using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.User
{
    public class UpdateUserPasswordRequest : IRequest<bool>
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
    }
}
