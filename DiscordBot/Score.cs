using Google.Cloud.Firestore;

namespace DiscordBot;

[FirestoreData]
public class Score
{
    [FirestoreProperty]
    public string Username { get; set; }
    [FirestoreProperty]
    public int PlayerScore { get; set; }
    [FirestoreProperty]
    public int ComputerScore { get; set; }
}