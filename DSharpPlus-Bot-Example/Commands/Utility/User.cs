using System.ComponentModel;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace ExampleBot.Commands.Utility;

[Command("user")]
[Description("User related commands")]
public class User
{
    [Command("avatar")]
    [Description("Get a user's avatar")]
    
    public async Task AvatarCommandAsync(
        CommandContext ctx,
        [Parameter("user")]
        [Description("The user to get the avatar from")] DiscordUser? user = null)
    {
        if (user is null)
        {
            user = ctx.User;
        }

        
        
        DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            .WithColor(DiscordColor.Magenta)
            .WithTitle($"{user.GlobalName ?? user.Username}'s avatar")
            .WithImageUrl(user.AvatarUrl);

        // Link Button example
        DiscordMessageBuilder builder = new DiscordMessageBuilder()
            .AddEmbed(embed)
            .AddComponents(new DiscordLinkButtonComponent(
                user.AvatarUrl,
                "Open in Browser"));

        await ctx.RespondAsync(builder);
        
    }
    
    
    [Command("button")]
    public async Task TestCommandAsync(CommandContext ctx)
    {
        DiscordButtonComponent button = new DiscordButtonComponent(
            DiscordButtonStyle.Success,
            "test",
            "testing"
        );

        DiscordInteractionResponseBuilder sendMessage = new DiscordInteractionResponseBuilder().WithContent("Test")
            .AddComponents(button);
        
        await ctx.RespondAsync(sendMessage);
    }
    
}