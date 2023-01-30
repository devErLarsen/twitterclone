namespace Api.Entities;

public class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserToken { get; set; } = null!;

    public ICollection<Post> Posts { get; } = new List<Post>();
}
