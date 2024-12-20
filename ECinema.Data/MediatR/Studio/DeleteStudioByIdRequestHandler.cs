﻿using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.Studio
{
    public class DeleteStudioByIdRequestHandler : IRequestHandler<DeleteStudioByIdRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public DeleteStudioByIdRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(DeleteStudioByIdRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = StudioQueries.DeleteStudioByIdQuery; 
            query = query.Replace("@StudioId", request.StudioId.ToString());  

            using var command = new MySqlCommand(query, _connection);

            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result > 0;
        }
    }

}