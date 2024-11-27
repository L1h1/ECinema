using ECinema.Data.Queries;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Repository
{
    public class DbInitializer
    {
        public static async Task ExecuteTablesCreation(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            using var connection = services.GetRequiredService<MySqlConnection>();
            connection.Open();

            using MySqlCommand command = new MySqlCommand(TableCreationQueries.CreateRoleTableQuery, connection);     
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateUsersTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateGenresTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateCinemasTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateMoviesTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateM2MMovieGenreTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateM2MMovieCinemaTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateWatchHistoryTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateUserActionsLogTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateReviewsTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateActorsTableQuery;
            command.ExecuteNonQuery();

            command.CommandText = TableCreationQueries.CreateM2MovieActorsTableQuery;
            command.ExecuteNonQuery();

            connection.Close();

        }


        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            using var connection = services.GetRequiredService<MySqlConnection>();
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
/*                // Insert Roles
                var insertRoles = @"
            INSERT INTO Roles (RoleId, RoleName) VALUES
            (1, 'Admin'),
            (2, 'User');";
                ExecuteQuery(connection, insertRoles);

                // Insert Users
                var insertUsers = @"
            INSERT INTO Users (UserId, Username, Password, Email, RoleId) VALUES
            (1, 'AdminUser', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'admin@example.com', 1),
            (2, 'RegularUser', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'user@example.com', 2);";
                ExecuteQuery(connection, insertUsers);*/

                // Insert Genres
                var insertGenres = @"
            INSERT INTO Genres (GenreId, GenreName) VALUES
            (1, 'Action'),
            (2, 'Comedy'),
            (3, 'Drama');";
                ExecuteQuery(connection, insertGenres);

                // Insert Cinemas
                var insertCinemas = @"
            INSERT INTO Cinemas (CinemaId, CinemaName) VALUES
            (1, 'CinemaOne'),
            (2, 'CinemaTwo');";
                ExecuteQuery(connection, insertCinemas);

                // Insert Movies
                var insertMovies = @"
            INSERT INTO Movies (MovieId, Title, Description, ReleaseYear, DurationMinutes) VALUES
            (1, 'Action Movie', 'An exciting action movie.', 2022, 120),
            (2, 'Comedy Movie', 'A hilarious comedy movie.', 2021, 90);";
                ExecuteQuery(connection, insertMovies);

                // Insert Actors
                var insertActors = @"
            INSERT INTO Actors (ActorId, ActorName) VALUES
            (1, 'John Doe'),
            (2, 'Jane Smith');";
                ExecuteQuery(connection, insertActors);

                // Insert MovieGenres
                var insertMovieGenres = @"
            INSERT INTO MovieGenres (MovieId, GenreId) VALUES
            (1, 1), -- Action Movie -> Action
            (2, 2); -- Comedy Movie -> Comedy";
                ExecuteQuery(connection, insertMovieGenres);

                // Insert MovieActors
                var insertMovieActors = @"
            INSERT INTO MovieActors (MovieId, ActorId) VALUES
            (1, 1), -- Action Movie -> John Doe
            (2, 2); -- Comedy Movie -> Jane Smith";
                ExecuteQuery(connection, insertMovieActors);

                // Insert MovieCinemas
                var insertMovieCinemas = @"
            INSERT INTO MovieCinemas (MovieId, CinemaId) VALUES
            (1, 1), -- Action Movie -> CinemaOne
            (2, 2); -- Comedy Movie -> CinemaTwo";
                ExecuteQuery(connection, insertMovieCinemas);

                // Insert WatchHistory
                var insertWatchHistory = @"
            INSERT INTO WatchHistory (WatchId, UserId, MovieId, WatchedAt) VALUES
            (1, 2, 1, '2024-11-01 12:00:00'),
            (2, 2, 2, '2024-11-02 15:00:00');";
                ExecuteQuery(connection, insertWatchHistory);

                // Insert Reviews
                var insertReviews = @"
            INSERT INTO Reviews (ReviewId, UserId, MovieId, ReviewText, ReviewRating, CreatedAt) VALUES
            (1, 2, 1, 'Amazing action scenes!', 5, '2024-11-03 10:00:00'),
            (2, 2, 2, 'Very funny and entertaining.', 4, '2024-11-04 11:00:00');";
                ExecuteQuery(connection, insertReviews);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        private static void ExecuteQuery(MySqlConnection connection, string query)
        {
            using var command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
        }
    }
}
