using Microsoft.EntityFrameworkCore;

public class ApiContactContext : DbContext
{
    public DbSet<Contact> ApiContact { get; set; } = null!;
    public string DbPath { get; private set; }

    public ApiContactContext()
    {
    // Path to SQLite database file
    DbPath = "ApiContact.db";
    }

    // The following configures EF to create a SQLite database file locally
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    // Use SQLite as database
    options.UseSqlite($"Data Source={DbPath}");
    // Optional: log SQL queries to console
    //options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
    }
}


