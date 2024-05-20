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
        InteractivityExtension interactivity = ctx.Client.GetInteractivity();
        DiscordButtonComponent button = new DiscordButtonComponent(
            DiscordButtonStyle.Success,
            "test",
            "testing"
        );

        DiscordInteractionResponseBuilder sendMessage = new DiscordInteractionResponseBuilder().WithContent("Test")
            .AddComponents(button);
        
        await ctx.RespondAsync(sendMessage);
        
        // Button interaction with DSharpPlus.Interactivity
        InteractivityResult<ComponentInteractionCreateEventArgs> result = await interactivity.WaitForButtonAsync(await ctx.GetResponseAsync(), ctx.User);
        if (result.Result.Id == "test")
        {
            await result.Result.Interaction.CreateResponseAsync(
                DiscordInteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder(
                    new DiscordMessageBuilder().WithContent("hello")).AsEphemeral());
        }
    }
    
}