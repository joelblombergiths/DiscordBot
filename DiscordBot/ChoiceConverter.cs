using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;

namespace DiscordBot;

public class ChoiceConverter : IArgumentConverter<RPSLS.Choice>
{
    public Task<Optional<RPSLS.Choice>> ConvertAsync(string value, CommandContext ctx)
    {
        return Task.FromResult(Enum.TryParse(value, true, out RPSLS.Choice choice)
            ? Optional.FromValue(choice)
            : Optional.FromNoValue<RPSLS.Choice>());
    }
}