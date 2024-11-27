using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ECinema.Data.MediatR.User
{
    public class UpdateUserPasswordRequestHandler : IRequestHandler<UpdateUserPasswordRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public UpdateUserPasswordRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(UpdateUserPasswordRequest request, CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.UpdateUserPasswordQuery; 

            using var command = new MySqlCommand(query, _connection);
            command.Parameters.AddWithValue("@UserId", request.UserId.ToString());
            command.Parameters.AddWithValue("@PasswordHash", ComputeSha256Hash(request.NewPassword));



            var result = await command.ExecuteNonQueryAsync(cancellationToken);

            await _connection.CloseAsync();

            return result > 0;
        }
        // Метод для вычисления SHA-256 хэша
        private static string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }
    }

}
