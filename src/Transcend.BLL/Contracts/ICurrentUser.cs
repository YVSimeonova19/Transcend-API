namespace Transcend.BLL.Contracts;

// Keep the user currently logged in
public interface ICurrentUser
{
    string UserId { get; }
}
