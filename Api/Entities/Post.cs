namespace Api.Entities;

public class Post
{
    public int PostId { get; set; }

    public int PostUserId { get; set; }

    public string PostBody { get; set; } = null!;

    public User PostUser { get; set; } = null!;
}

public record CreatePostDto(int PostUserId, string PostBody);
public record PostDto(int PostId, int PostUserId, string PostBody);
