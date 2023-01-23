using Api;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, CustomTokenAuthHandler>("BasicAuthentication", null);
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApiDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITokenValidationService, TokenValidationService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/v1/posts", async (ApiDbContext db) =>
{
    return await db.Posts.ToListAsync();
});

app.MapGet("/api/v1/posts/{id:int}", async (int id, ApiDbContext db) =>
{
    return await db.Posts.FirstOrDefaultAsync(p => p.PostId == id)
        is Post post
            ? Results.Ok(post)
            : Results.NotFound();
});

app.MapPost("/api/v1/posts", async (HttpContext context, ApiDbContext db, CreatePostDto post) =>
{
    var newPost = new Post { PostUserId = post.PostUserId, PostBody = post.PostBody };
    db.Add(newPost);
    await db.SaveChangesAsync();
    return Results.Created($"/api/v1/posts/{newPost.PostId}", newPost);
}).RequireAuthorization();

app.MapPut("/api/v1/posts/edit", async (HttpContext context, ApiDbContext db, Post post) =>
{
    var userId = context.User.FindFirst("userid")?.Value;
    var existingPost = await db.Posts.FirstOrDefaultAsync(p => p.PostId == post.PostId);
    if(existingPost == null)
        return Results.NotFound("Resource does not exist.");
    if(post.PostUserId != int.Parse(userId))
        return Results.Forbid();
    existingPost.PostBody = post.PostBody;
    await db.SaveChangesAsync();
    return Results.NoContent();
}).RequireAuthorization();

app.MapDelete("/api/v1/posts/delete/{id:int}", async (HttpContext context, ApiDbContext db, int id) =>
{
    var userId = context.User.FindFirst("userid")?.Value;
    var post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == id);
    if(post == null)
        return Results.NotFound("Resource does not exist.");
    if(post.PostUserId != int.Parse(userId))
        return Results.Forbid();
    db.Remove(post);
    await db.SaveChangesAsync();
    return Results.NoContent();
}).RequireAuthorization();

app.Run();