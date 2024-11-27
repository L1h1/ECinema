using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    public class RoleQueries
    {
        public static string InsertRoleQuery = @"INSERT INTO Roles (RoleName) VALUES (@RoleName);";

        public static string DeleteRoleByIdQuery = @"DELETE FROM Roles WHERE RoleId = @RoleId;";

        public static string DeleteRoleByNameQuery = @"DELETE FROM Roles WHERE RoleName = @RoleName;";

        public static string SelectAllRolesQuery = @"SELECT RoleId, RoleName FROM Roles;";
    }
}
