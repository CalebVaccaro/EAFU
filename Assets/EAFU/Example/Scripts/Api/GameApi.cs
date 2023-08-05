using System;
using System.Collections.Generic;
using EAFU;

[Serializable]
public class GameApi : EAFUApi
{
    public Player player { get; set; }

    public void CreatePlayer(string name, int score, int gameDuration, Action<object> PostPlayerAction)
    {
        player = new Player(name, score, gameDuration);
        Post(player, PostPlayerAction);
    }
    public void GetLeaderboard(Action<List<Player>> GetLeaderboardsAction) => Get(GetLeaderboardsAction);
    public void GetLeaderboard(int leaderboardCount, Action<List<Player>> GetLeaderboardsAction) =>
        ApiService.Get($"{endpoints.Get}"+$"/{leaderboardCount}", GetLeaderboardsAction);
}

[Serializable] 
public class Player
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int GameDuration { get; set; }

    public Player(string name, int score, int gameDuration)
    {
        this.Id = Guid.NewGuid().ToString();
        this.Name = name;
        this.Score = score;
        this.GameDuration = gameDuration;
    }
}
