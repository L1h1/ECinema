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
        SELECT wh.WatchId, wh.UserId, wh.MovieId, wh.WatchedAt, m.Title 
        FROM WatchHistory wh 
        INNER JOIN Movies m ON wh.MovieId = m.MovieId
        WHERE UserId = @UserId;";

    }
}
