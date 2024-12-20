using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class StudioQueries
    {
        public static string InsertStudioQuery = @"
        INSERT INTO Studios (StudioName) 
        VALUES (@StudioName);";

        public static string DeleteStudioByIdQuery = @"
        DELETE FROM Studios 
        WHERE StudioId = @StudioId;";

        public static string SelectAllStudiosQuery = @"
        SELECT * FROM Studios;";

        public static string UpdateStudioNameQuery = @"
        UPDATE Studios
        SET StudioName = @StudioName
        WHERE StudioId = @StudioId;
        ";

        public static string SelectStudioByIdQuery = @"
        SELECT * FROM Studios
        WHERE StudioId = @StudioId;
        ";
    }
}
