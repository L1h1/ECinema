using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class MovieQueries
    {
        /// <summary>
        /// Базовые
        /// </summary>

        public static string InsertMovieQuery = @"
        INSERT INTO Movies (Title, Description, ReleaseYear, DurationMinutes) 
        VALUES (@Title, @Description, @ReleaseYear, @DurationMinutes);";

        public static string UpdateMovieTitleQuery = @"
        UPDATE Movies 
        SET Title = @Title 
        WHERE MovieId = @MovieId;";

        public static string UpdateMovieDetailsQuery = @"
        UPDATE Movies 
        SET Description = @Description, ReleaseYear = @ReleaseYear, DurationMinutes = @DurationMinutes 
        WHERE MovieId = @MovieId;";

        public static string DeleteMovieByIdQuery = @"
        DELETE FROM Movies 
        WHERE MovieId = @MovieId;";

        public static string SelectMovieByIdQuery = @"
        SELECT MovieId, Title, Description, ReleaseYear, DurationMinutes, CreatedAt 
        FROM Movies 
        WHERE MovieId = @MovieId;";

        public static string SelectMovieByNameQuery = @"
        SELECT MovieId, Title, Description, ReleaseYear, DurationMinutes, CreatedAt
        FROM Movies
        WHERE Title = @Title;";



        public static string SelectAllMoviesQuery = @"
        SELECT 
            MovieId, 
            Title, 
            Description, 
            ReleaseYear, 
            DurationMinutes, 
            CreatedAt 
        FROM Movies;";


        /// <summary>
        /// С подвязкой жанра
        /// </summary>

        public static string InsertMovieGenreQuery = @"
        INSERT INTO MovieGenres (MovieId, GenreId) 
        VALUES (@MovieId, @GenreId);";

        public static string DeleteMovieGenreQuery = @"
        DELETE FROM MovieGenres 
        WHERE MovieId = @MovieId AND GenreId = @GenreId;";

        //Фильмы по жанру
        public static string SelectMoviesByGenreQuery = @"
        SELECT m.MovieId, m.Title, m.Description, m.ReleaseYear, m.DurationMinutes, m.CreatedAt 
        FROM Movies m
        INNER JOIN MovieGenres mg ON m.MovieId = mg.MovieId
        WHERE mg.GenreId = @GenreId;";


        /// <summary>
        /// С подвязкой кинотеатра
        /// </summary>

        public static string InsertMovieCinemaQuery = @"
        INSERT INTO MovieCinemas (MovieId, CinemaId) 
        VALUES (@MovieId, @CinemaId);";

        public static string DeleteMovieCinemaQuery = @"
        DELETE FROM MovieCinemas 
        WHERE MovieId = @MovieId AND CinemaId = @CinemaId;";

        public static string SelectMoviesByCinemaQuery = @"
        SELECT m.MovieId, m.Title, m.Description, m.ReleaseYear, m.DurationMinutes, m.CreatedAt 
        FROM Movies m
        INNER JOIN MovieCinemas mc ON m.MovieId = mc.MovieId
        WHERE mc.MovieId = @MovieId;";


        /// <summary>
        ///С подвязкой актеров 
        /// </summary>
        
        public static string InsertMovieActorQuery = @"
        INSERT INTO MovieActors (MovieId, ActorId) 
        VALUES (@MovieId, @ActorId);";

        public static string DeleteMovieActorQuery = @"
        DELETE FROM MovieActors 
        WHERE MovieId = @MovieId AND ActorId = @ActorId;";

        public static string SelectMoviesByActorQuery = @"
        SELECT m.MovieId, m.Title, m.Description, m.ReleaseYear, m.DurationMinutes, m.CreatedAt 
        FROM Movies m
        INNER JOIN MovieActors ma ON m.MovieId = ma.MovieId
        WHERE ma.ActorId = @ActorId;";
    }
}
