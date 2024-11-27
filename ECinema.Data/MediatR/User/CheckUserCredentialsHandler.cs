using ECinema.Data.Entities;
using ECinema.Data.Queries;
using MediatR;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using System.Text;

public class CheckUserCredentialsHandler : IRequestHandler<CheckUserCredentialsRequest, (int UserId, string RoleName)?>
{
    private readonly MySqlConnection _connection;

    public CheckUserCredentialsHandler(MySqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<(int UserId, string RoleName)?> Handle(CheckUserCredentialsRequest request, CancellationToken cancellationToken)
    {
        // Хэширование пароля
        var passwordHash = ComputeSha256Hash(request.Password);

        await _connection.OpenAsync(cancellationToken);

        using var command = new MySqlCommand(UserQueries.SelectUserByEmailAndPasswordHashQuery, _connection);
        command.Parameters.AddWithValue("@Email", request.Email);
        command.Parameters.AddWithValue("@PasswordHash", passwordHash);

        using (var reader = await command.ExecuteReaderAsync())
        {
            if (await reader.ReadAsync())
            {
                var data = ((int)reader["UserId"], (string)reader["RoleName"]);

                
                await _connection.CloseAsync();
                return data;
            }
        }

        await _connection.CloseAsync();
        return null; 
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
