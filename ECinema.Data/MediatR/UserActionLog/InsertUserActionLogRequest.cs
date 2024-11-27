using MediatR;

public class InsertUserActionLogRequest : IRequest<int>
{
    public int UserId { get; set; }
    public string ActionType { get; set; }
    public string ActionDetails { get; set; }
    public DateTime ActionTime { get; set; }
}
