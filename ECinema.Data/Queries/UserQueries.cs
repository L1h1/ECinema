using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class UserQueries
    {
        public static string InsertUserQuery = @"
        INSERT INTO Users (Username, PasswordHash, Email, RoleId) 
        VALUES (@Username, @PasswordHash, @Email, @RoleId);";

        public static string UpdateUserEmailQuery = @"
        UPDATE Users 
        SET Email = @Email 
        WHERE UserId = @UserId;";

        public static string UpdateUserPasswordQuery = @"
        UPDATE Users 
        SET PasswordHash = @PasswordHash 
        WHERE UserId = @UserId;";

        public static string DeleteUserByIdQuery = @"
        DELETE FROM Users 
        WHERE UserId = @UserId;";

        public static string SelectUserByIdQuery = @"
        SELECT UserId, Username, Email, RoleId, CreatedAt 
        FROM Users 
        WHERE UserId = @UserId;";

        public static string SelectAllUsersQuery = @"
        SELECT UserId, Username, Email, RoleId, CreatedAt 
        FROM Users;";

        public static string SelectUserByEmailAndPasswordHashQuery = @"
        SELECT 
            u.UserId, 
            u.Username, 
            u.Email, 
            u.RoleId, 
            r.RoleName
        FROM Users u
        INNER JOIN Roles r ON u.RoleId = r.RoleId
        WHERE Email = @Email AND PasswordHash = @PasswordHash;";
    }
}
