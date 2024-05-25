using DSharpPlus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ExampleBot.Database;


public class DesignTimeFactory : IDesignTimeDbContextFactory<ExampleBotContext>
{
    
    public ExampleBotContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ExampleBotContext> builder = new();
        builder.UseNpgsql("Host=172.17.0.2;Port=5432;Database=example;Username=postgres");

        builder.UseNpgsql().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        return new ExampleBotContext(builder.Options);
    }
}