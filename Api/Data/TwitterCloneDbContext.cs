using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class TwitterCloneDbContext : DbContext
{
    public TwitterCloneDbContext(DbContextOptions<TwitterCloneDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.PostBody).HasColumnName("post_body");
            entity.Property(e => e.PostUserId).HasColumnName("post_user_id");

            entity.HasOne(d => d.PostUser).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostUserId)
                .HasConstraintName("posts_post_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
            entity.Property(e => e.UserToken)
                .HasMaxLength(10)
                .HasColumnName("user_token");
        });
        modelBuilder.HasSequence("posts_post_id_seq").HasMax(2147483647L);
        modelBuilder.HasSequence("users_user_id_seq").HasMax(2147483647L);

    }

}
