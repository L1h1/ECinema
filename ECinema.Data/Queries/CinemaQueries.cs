using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class CinemaQueries
    {
        public static string InsertCinemaQuery = @"
        INSERT INTO Cinemas (CinemaName, WebsiteUrl) 
        VALUES (@CinemaName, @WebsiteUrl);";

        public static string UpdateCinemaNameQuery = @"
        UPDATE Cinemas 
        SET CinemaName = @CinemaName 
        WHERE CinemaId = @CinemaId;";

        public static string UpdateCinemaWebsiteQuery = @"
        UPDATE Cinemas 
        SET WebsiteUrl = @WebsiteUrl 
        WHERE CinemaId = @CinemaId;";

        public static string DeleteCinemaByIdQuery = @"
        DELETE FROM Cinemas 
        WHERE CinemaId = @CinemaId;";

        public static string SelectCinemaByIdQuery = @"
        SELECT CinemaId, CinemaName, WebsiteUrl 
        FROM Cinemas 
        WHERE CinemaId = @CinemaId;";

        public static string SelectAllCinemasQuery = @"
        SELECT CinemaId, CinemaName, WebsiteUrl 
        FROM Cinemas;";
    }
}
