using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class UserActionLogQueries
    {
        public static string InsertUserActionLogQuery = @"
        INSERT INTO UserActionsLog (UserId, ActionType, ActionDetails, ActionTime) 
        VALUES (@UserId, @ActionType, @ActionDetails, @ActionTime);";

        public static string SelectAllUserActionLogsQuery = @"
        SELECT LogId, UserId, ActionType, ActionDetails, ActionTime 
        FROM UserActionsLog;";

        public static string SelectUserActionLogsByActionTypeQuery = @"
        SELECT LogId, UserId, ActionType, ActionDetails, ActionTime 
        FROM UserActionsLog
        WHERE ActionType = @ActionType;";

    }
}
