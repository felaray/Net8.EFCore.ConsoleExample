using Microsoft.EntityFrameworkCore;

public class BloggingContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public BloggingContext()
    {
    }

    //
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Data Source=.;Initial Catalog=ConsoleExampleDB;Integrated Security=True;Trust Server Certificate=True");
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Blog> Blogs { get; } = new();
}

public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}