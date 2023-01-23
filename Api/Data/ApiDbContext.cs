using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class ApiDbContext : DbContext
{

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var users = new User[]
        {
            new() { UserId = 1, UserName = "Bruker En", UserToken = "1111111111"},
            new() { UserId = 2, UserName = "Bruker To", UserToken = "2222222222"},
            new() { UserId = 3, UserName = "Bruker Tre", UserToken = "3333333333"},
        };
        var posts = new Post[]
        {
            new() { PostId = 1, PostUserId = 1, PostBody = "Inlegg nummer en"},
            new() { PostId = 2, PostUserId = 2, PostBody = "Inlegg nummer to"},
            new() { PostId = 3, PostUserId = 3, PostBody = "Inlegg nummer tre"},
        };
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Post>().HasData(posts);
    }
}