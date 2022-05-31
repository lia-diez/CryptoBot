using CryptoBot.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBot;

public class TelebotContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    private TelebotContext(DbContextOptions<TelebotContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    private static TelebotContext? _instance;

    public static TelebotContext Instance => _instance ??=
        new TelebotContext(
            new DbContextOptionsBuilder<TelebotContext>()
            .UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "")
            .Options);
}