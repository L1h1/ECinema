using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class WatchHistoryQueries
    {
        public static string InsertWatchHistoryQuery = @"
        INSERT INTO WatchHistory (UserId, MovieId, WatchedAt) 
        VALUES (@UserId, @MovieId, @WatchedAt);";

        public static string DeleteWatchHistoryQuery = @"
        DELETE FROM WatchHistory 
        WHERE WatchId = @WatchId;";

        public static string SelectWatchHistoryByUserQuery = @"
        SELECT WatchId, UserId, MovieId, WatchedAt 
        FROM WatchHistory 
        WHERE UserId = @UserId;";

    }
}
