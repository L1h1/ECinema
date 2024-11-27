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
    public class InsertUserRequestHandler : IRequestHandler<InsertUserRequest, bool>
    {
        private readonly MySqlConnection _connection;

        public InsertUserRequestHandler(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Handle(InsertUserRequest request, CancellationToken cancellationToken)
        {

            await _connection.OpenAsync(cancellationToken);

            var query = UserQueries.InsertUserQuery;  

            using var command = new MySqlCommand(query, _connection);

            command.Parameters.AddWithValue("@Username", request.Username);
            command.Parameters.AddWithValue("@PasswordHash", ComputeSha256Hash(request.Password));
            command.Parameters.AddWithValue("@Email", request.Email);
            command.Parameters.AddWithValue("@RoleId", request.RoleId.ToString());

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
