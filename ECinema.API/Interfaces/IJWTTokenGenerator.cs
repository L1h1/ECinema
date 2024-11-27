using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.API.Interfaces
{
    internal interface IJWTTokenGenerator
    {
        string GenerateToken(int userId, string email, string role);
    }
}
