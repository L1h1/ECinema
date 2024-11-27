using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    public static class TableCreationQueries
    {
        public static string CreateRoleTableQuery = @"
        CREATE OR REPLACE TABLE Roles (
            RoleId INT AUTO_INCREMENT PRIMARY KEY,
            RoleName VARCHAR(50) NOT NULL UNIQUE
        );";

        public static string CreateUsersTableQuery = @"
        CREATE OR REPLACE TABLE Users (
            UserId INT AUTO_INCREMENT PRIMARY KEY,  
            Username VARCHAR(50) NOT NULL UNIQUE,   
            PasswordHash VARCHAR(255) NOT NULL,   
            Email VARCHAR(100) NOT NULL UNIQUE,    
            RoleId INT NOT NULL,   
            CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,    
            FOREIGN KEY (RoleId) REFERENCES Roles(RoleId) ON DELETE CASCADE
        );";

        public static string CreateGenresTableQuery = @"
        CREATE OR REPLACE TABLE Genres (
            GenreId INT AUTO_INCREMENT PRIMARY KEY,    
            GenreName VARCHAR(50) NOT NULL UNIQUE
        );";

        public static string CreateCinemasTableQuery = @"
        CREATE OR REPLACE TABLE Cinemas (
             CinemaId INT AUTO_INCREMENT PRIMARY KEY,   
             CinemaName VARCHAR(100) NOT NULL UNIQUE,   
             WebsiteUrl VARCHAR(255)
        );";

        public static string CreateMoviesTableQuery = @"
        CREATE OR REPLACE TABLE Movies (
            MovieId INT AUTO_INCREMENT PRIMARY KEY,    
            Title VARCHAR(255) NOT NULL,   
            Description TEXT,  
            ReleaseYear INT,       
            DurationMinutes INT,    
            CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
        );";

        public static string CreateM2MMovieGenreTableQuery = @"
        CREATE OR REPLACE TABLE MovieGenres (
             MovieId INT NOT NULL,   
             GenreId INT NOT NULL,  
             PRIMARY KEY (MovieId, GenreId),    
             FOREIGN KEY (MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE,    
             FOREIGN KEY (GenreId) REFERENCES Genres(GenreId) ON DELETE CASCADE
        );";

        public static string CreateM2MMovieCinemaTableQuery = @"
        CREATE OR REPLACE TABLE MovieCinemas (
             MovieId INT NOT NULL,   
             CinemaId INT NOT NULL,   
             PRIMARY KEY (MovieId, CinemaId),    
             FOREIGN KEY (MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE,   
             FOREIGN KEY (CinemaId) REFERENCES Cinemas(CinemaId) ON DELETE CASCADE
        );";

        public static string CreateWatchHistoryTableQuery = @"
        CREATE OR REPLACE TABLE WatchHistory (
             WatchId INT AUTO_INCREMENT PRIMARY KEY,    
             UserId INT NOT NULL,   MovieId INT NOT NULL,    
             WatchedAt DATETIME DEFAULT CURRENT_TIMESTAMP,    
             FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,    
             FOREIGN KEY (MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE
        );";

        public static string CreateUserActionsLogTableQuery = @"
        CREATE OR REPLACE TABLE UserActionsLog (
             LogId INT AUTO_INCREMENT PRIMARY KEY,    
             UserId INT NOT NULL,    
             ActionType VARCHAR(100) NOT NULL,    
             ActionDetails TEXT,    
             ActionTime DATETIME DEFAULT CURRENT_TIMESTAMP,    
             FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
        );";

        public static string CreateReviewsTableQuery = @"
        CREATE OR REPLACE TABLE Reviews (
             ReviewId INT AUTO_INCREMENT PRIMARY KEY,    
             MovieId INT NOT NULL,    
             UserId INT NOT NULL,    
             ReviewText TEXT NOT NULL,    
             ReviewRating INT CHECK (ReviewRating BETWEEN 1 AND 10),    
             CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,    
             FOREIGN KEY (MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE,    
             FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE
        );";

        public static string CreateActorsTableQuery = @"
        CREATE OR REPLACE TABLE Actors (
             ActorId INT AUTO_INCREMENT PRIMARY KEY,    
             ActorName VARCHAR(100) NOT NULL UNIQUE,    
             DateOfBirth DATE,    
             Bio TEXT
        );";

        public static string CreateM2MovieActorsTableQuery = @"
        CREATE OR REPLACE TABLE MovieActors (
             MovieId INT NOT NULL,
             ActorId INT NOT NULL,
             PRIMARY KEY(MovieId, ActorId),
             FOREIGN KEY(MovieId) REFERENCES Movies(MovieId) ON DELETE CASCADE,
             FOREIGN KEY(ActorId) REFERENCES Actors(ActorId) ON DELETE CASCADE
        );";

    }

}
