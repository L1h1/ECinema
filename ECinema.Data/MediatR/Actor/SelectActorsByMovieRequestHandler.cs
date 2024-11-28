using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Actor
{
    internal class SelectActorsByMovieRequestHandler : IRequestHandler<SelectActorsByMovieRequest, List<Entities.Actor>>
    {
        private readonly MySqlConnection _connection;

    public SelectActorsByMovieRequestHandler(MySqlConnection connection)
    {
        _connection = connection;
    }

        public async Task<List<Entities.Actor>> Handle(SelectActorsByMovieRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = ActorQueries.SelectActorsByMovieQuery;

            using var command = new MySqlCommand(query, _connection);
            
            command.Parameters.AddWithValue("@MovieId", request.MovieId);
            using var reader = await command.ExecuteReaderAsync(cancellationToken);
            var actors = new List<Entities.Actor>();

            while (await reader.ReadAsync(cancellationToken))
            {
                var actor = new Entities.Actor
                {
                    ActorId = (int)reader["ActorId"],
                    ActorName = (string)reader["ActorName"],
                    DateOfBirth = reader["DateOfBirth"] as DateTime?,
                    Bio = reader["Bio"] as string
                };
                actors.Add(actor);
            }

            await _connection.CloseAsync();

            return actors;
        }
    }
}
