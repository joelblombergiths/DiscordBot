using DiscordBot;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Newtonsoft.Json;

const string token = "MTA3MjA4NTE0NDU3NzUyMzcxMg.Gogt7G.14Q-xdqgH1_z9IJGqPiZ55jiPSECcbyGVKlzq0";



DiscordClient client = new (new()
{
    Token = token,
    TokenType = TokenType.Bot,
    Intents = DiscordIntents.All,
    MinimumLogLevel = LogLevel.Debug
});

ServiceProvider services = new ServiceCollection()
    .AddSingleton<RPSLS>()
    .AddSingleton<GameData>()
    .AddSingleton(_ => new GameData(
new FirestoreDbBuilder
        {
            ProjectId = "rpsls-11061",
            JsonCredentials = fireStoreCred()
        }
        .Build()))
    .BuildServiceProvider();

CommandsNextExtension commands = client.UseCommandsNext(new()
{
    StringPrefixes = new [] { "!" },
    Services = services
});

commands.RegisterCommands<Commands>();
commands.RegisterConverter(new ChoiceConverter());

await client.ConnectAsync();
await Task.Delay(-1);

static string fireStoreCred()
{
    using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DiscordBot.firestore.json");
    using StreamReader reader = new (stream);
    return reader.ReadToEnd();
}