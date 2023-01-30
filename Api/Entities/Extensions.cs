namespace Api.Entities;

public static class Extensions
{
    public static PostDto ToPostDto(this Post post)
        => new(post.PostId, post.PostUserId, post.PostBody);
}