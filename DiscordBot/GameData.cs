using Google.Cloud.Firestore;

namespace DiscordBot;

public class GameData
{
    private readonly CollectionReference scoreCollection;

    public GameData(FirestoreDb context)
    {
        scoreCollection  = context.Collection("Score");
    }

    private async Task<DocumentReference> GetDocRef(string username)
    {
        QuerySnapshot res = await scoreCollection.WhereEqualTo("Username", username).GetSnapshotAsync();
        
        return res.Count > 0 
            ? scoreCollection.Document(res.Documents[0].Id)
            : await scoreCollection.AddAsync(new Score { Username = username });
    }

    public async Task<Score> GetScore(string username)
    {
        DocumentReference userDoc = await GetDocRef(username);
        DocumentSnapshot res = await userDoc.GetSnapshotAsync();
        return res.ConvertTo<Score>();
    }

    public async Task PlayerWon(string username)
    {
        DocumentReference userDoc = await GetDocRef(username);
        Score score = await GetScore(username);
        score.PlayerScore++;
        await userDoc.SetAsync(score);
    }

    public async Task ComputerWon(string username)
    {
        DocumentReference userDoc = await GetDocRef(username);
        Score score = await GetScore(username);
        score.ComputerScore++;
        await userDoc.SetAsync(score);
    }

    public async Task DeleteScore(string username)
    {
        DocumentReference userDoc = await GetDocRef(username);
        await userDoc.DeleteAsync();
    }
}
