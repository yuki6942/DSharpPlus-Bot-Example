using System.Reflection;
using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using ExampleBot.Database;
using ExampleBot.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleBot
{
    public class Program
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        // ReSharper disable once NotAccessedField.Local
        private static IServiceProvider _services;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        // ReSharper disable once InconsistentNaming
        public static async Task Main()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {

            // Setup config using User Secrets
            ConfigurationBuilder builder = new();
            builder.AddUserSecrets<Program>();
            builder.AddEnvironmentVariables();
            IConfiguration config = builder.Build();


            // Initialize the Discord Client
            DiscordShardedClient client = new(new DiscordConfiguration()
            {
                Token = config["DISCORD_TOKEN"]!,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
                AutoReconnect = true
            });

            // Enable Interactivity
            await client.UseInteractivityAsync(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromSeconds(120)
            });

            // Setup database
            ServiceCollection collection = new();
            collection.AddDbContextPool<ExampleBotContext>(db =>
                db.UseNpgsql(config["DB_STRING"]));
            IServiceProvider services = collection.BuildServiceProvider();

            IReadOnlyDictionary<int, CommandsExtension> command =
                await client.UseCommandsAsync(
                    new CommandsConfiguration()
                    {
                        ServiceProvider = services
                    }
                );
            _services = services;

            ExampleBotEventHandler exampleBotEventHandler = new(services, client, config);

            // Add commands to guilds
            foreach ((int _, CommandsExtension commandsExtension) in command)
            {
                commandsExtension.AddCommands(Assembly.GetExecutingAssembly(),
                    1166029845755609198); // Set your guild ID here to add commands to test server, remove to make it public
                commandsExtension.CommandErrored += exampleBotEventHandler.OnErrorAsync;

            }

            // Start bot and keep it running
            await client.StartAsync();
            await Task.Delay(-1);

        }
    }
}