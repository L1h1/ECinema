﻿using MediatR;

namespace ECinema.Data.MediatR.MovieActor
{
    public class InsertMovieActorRequest : IRequest<int> 
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

    }
}
