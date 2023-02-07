using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
namespace DiscordBot;

public class Commands : BaseCommandModule
{
    public RPSLS Game { private get; set; } 
    public GameData Data { private get; set; }

    [Command("RPSLS")]
    public async Task PlayRPSLS(CommandContext ctx)
    {
        await ctx.RespondAsync($"Play RPSLS by typing **!PLAY** followed by one of the following Choices (__{string.Join("__ , __", Enum.GetNames(typeof(RPSLS.Choice)))}__)");
    }

    [Command("PLAY")]
    public async Task Play(CommandContext ctx, RPSLS.Choice playerChoice)
    {
        string username = ctx.User.Username;
        RPSLS.Choice computerChoice = Game.ComputerChoice;
        
        RPSLS.Result result = Game.CheckResult(playerChoice, computerChoice, out string outcome);
        await ctx.RespondAsync($"{username} choose **{Enum.GetName(playerChoice)}** \n I coose **{Enum.GetName(computerChoice)}**\n\n _{outcome}_");

        switch (result)
        {
            case RPSLS.Result.Win:
                await Data.PlayerWon(username);
                break;
            case RPSLS.Result.Lose:
                await Data.ComputerWon(username);
                break;
            case RPSLS.Result.Tie:
            default:
                break;
        }

        Score score = await Data.GetScore(username);

        await Print(ctx, score);
    }

    [Command("NEWGAME")]
    public async Task NewGame(CommandContext ctx)
    {
        await Data.DeleteScore(ctx.User.Username);
        await PrintScore(ctx);
    }

    [Command("SCORE")]
    public async Task PrintScore(CommandContext ctx)
    {
        Score score = await Data.GetScore(ctx.User.Username);
        await Print(ctx, score);
    }

    private async Task Print(CommandContext ctx, Score score)
    {
        await ctx.RespondAsync($"__Score:__\n{ctx.User.Username}: **{score.PlayerScore}**\nComputer: **{score.ComputerScore}**");
    }
}