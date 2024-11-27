using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class GenreQueries
    {
        public static string InsertGenreQuery = @"
        INSERT INTO Genres (GenreName) 
        VALUES (@GenreName);";

        public static string DeleteGenreByIdQuery = @"
        DELETE FROM Genres 
        WHERE GenreId = @GenreId;";

        public static string DeleteGenreByNameQuery = @"
        DELETE FROM Genres 
        WHERE GenreName = @GenreName;";

        public static string SelectAllGenresQuery = @"
        SELECT * FROM Genres;";
    }
}
