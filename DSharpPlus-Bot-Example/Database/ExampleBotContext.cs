using Microsoft.EntityFrameworkCore;

namespace ExampleBot.Database;

public class ExampleBotContext : DbContext
{
    public ExampleBotContext(DbContextOptions<ExampleBotContext> options) : base(options)
    { }

}