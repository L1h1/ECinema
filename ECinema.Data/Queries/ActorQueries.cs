using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.Queries
{
    internal class ActorQueries
    {
        public static string InsertActorQuery = @"
        INSERT INTO Actors (ActorName, DateOfBirth, Bio) 
        VALUES (@ActorName, @DateOfBirth, @Bio);";

        public static string UpdateActorQuery = @"
        UPDATE Actors 
        SET ActorName = @ActorName, DateOfBirth = @DateOfBirth, Bio = @Bio 
        WHERE ActorId = @ActorId;";

        public static string DeleteActorQuery = @"
        DELETE FROM Actors 
        WHERE ActorId = @ActorId;";

        public static string SelectAllActorsQuery = @"
        SELECT ActorId, ActorName, DateOfBirth, Bio
        FROM Actors;";

        public static string SelectActorByIdQuery = @"
        SELECT ActorId, ActorName, DateOfBirth, Bio
        FROM Actors
        WHERE ActorId = @ActorId;";

        public static string SelectActorsByMovieQuery = @"
        SELECT a.ActorId, a.ActorName, a.DateOfBirth, a.Bio
        FROM Actors a
        INNER JOIN MovieActors ma ON a.ActorId = ma.ActorId
        WHERE ma.MovieId = @MovieId;";

    }
}
