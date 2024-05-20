using DSharpPlus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ExampleBot.Database;


public class DesignTimeFactory : IDesignTimeDbContextFactory<ExampleBotContext>
{
    
    // Dependency Injection
    private readonly IServiceProvider _services;
    private readonly DiscordShardedClient _client;
    private readonly IConfiguration _config;

    public DesignTimeFactory(IServiceProvider services, DiscordShardedClient client, IConfiguration config)
    {
        this._services = services;
        this._client = client;
        this._config = config;
    }
    
    public ExampleBotContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ExampleBotContext> builder = new();
        builder.UseNpgsql(_config["DB_STRING"]);

        builder.UseNpgsql().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        return new ExampleBotContext(builder.Options);
    }
}