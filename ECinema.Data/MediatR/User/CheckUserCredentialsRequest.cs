using MediatR;

public class CheckUserCredentialsRequest : IRequest<(int UserId, string RoleName)?>
{
    public string Email { get; set; }
    public string Password { get; set; } 
}
