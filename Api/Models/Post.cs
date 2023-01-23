namespace Api.Models;

public class Post
{
    public int PostId { get; set; }
    public int PostUserId { get; set; }
    public string PostBody { get; set; }
}

public record CreatePostDto(int PostUserId, string PostBody);