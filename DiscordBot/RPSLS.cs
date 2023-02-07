namespace DiscordBot;

public class RPSLS
{
    public enum Choice
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock
    }

    public enum Result
    {
        Tie,
        Win,
        Lose
    }

    public Choice ComputerChoice => (Choice)Random.Shared.Next(Enum.GetNames(typeof(Choice)).Length);

    public Result CheckResult(Choice player, Choice computer, out string reason)
    {
        if (player == computer)
        {
            reason = "It's a Tie!";
            return Result.Tie;
        }

        List<Choice> selectedChoices = new() { player, computer };

        (Choice winningChoice, Choice _, reason) = _combinations.Find(x =>
            selectedChoices.Contains(x.winningChoice) &&
            selectedChoices.Contains(x.losingChoice));

        return player == winningChoice ? Result.Win : Result.Lose;
    }

    private static readonly List<(Choice winningChoice, Choice losingChoice, string reason)> _combinations = new()
    {
        (Choice.Scissors, Choice.Paper, "Scissors cuts Paper"),
        (Choice.Paper, Choice.Rock, "Paper covers Rock"),
        (Choice.Rock, Choice.Lizard, "Rock crushes Lizard"),
        (Choice.Lizard, Choice.Spock, "Lizard poisons Spock"),
        (Choice.Spock, Choice.Scissors, "Spock smashes Scissors"),
        (Choice.Scissors, Choice.Lizard, "Scissors decapitates Lizard"),
        (Choice.Lizard, Choice.Paper, "Lizard eats Paper"),
        (Choice.Paper, Choice.Spock, "Paper disproves Spock"),
        (Choice.Spock, Choice.Rock, "Spock vaporizes Rock"),
        (Choice.Rock, Choice.Scissors, "Rock crushes Scissors")
    };
}