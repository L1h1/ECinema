using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Role
{
    public class DeleteRoleByIdRequest : IRequest<bool>
    {
        public int RoleId { get; set; }
    }
}
