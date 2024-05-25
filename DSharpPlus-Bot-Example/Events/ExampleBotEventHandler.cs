using DSharpPlus;
using DSharpPlus.Commands;
using DSharpPlus.Commands.EventArgs;
using DSharpPlus.Commands.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ExampleBot.Events;

public class ExampleBotEventHandler
{
    
    // Dependency Injection
    private readonly IServiceProvider _services;
    private readonly DiscordShardedClient _client;
    private readonly IConfiguration _config;

    public ExampleBotEventHandler(IServiceProvider services, DiscordShardedClient client, IConfiguration config)
    {
        this._services = services;
        this._client = client;
        this._config = config;
        // subscribe to event
        this._client.ComponentInteractionCreated += OnComponentInteractionCreateAsync;
    }
    
#pragma warning disable CA1822
    // ReSharper disable MemberCanBeMadeStatic.Global
    public async Task OnErrorAsync(CommandsExtension commands, CommandErroredEventArgs e)
        // ReSharper restore MemberCanBeMadeStatic.Global
#pragma warning restore CA1822
    {
        commands.Client.Logger.LogError("An error has occurred: {Exception}", e.Exception);
        if (e.Exception is ChecksFailedException)
        {
            
            await e.Context.RespondAsync("Only the bot owner can use these commands!");
            await e.Context.DeferResponseAsync();
        }
        else
        {
            await e.Context.RespondAsync(
                $"An error has occurred, please report the following error on our discord:\n{e.Exception}");
            await e.Context.DeferResponseAsync();
        }
        
    }

    private async Task OnComponentInteractionCreateAsync(DiscordClient sender, ComponentInteractionCreateEventArgs args)
    {
        switch (args.Interaction.Data.CustomId)
        {
            case "test":
                await args.Interaction.CreateResponseAsync(
                    DiscordInteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder(
                        new DiscordMessageBuilder().WithContent("hello")).AsEphemeral());
                break;
        }
    }
    
    
    
}