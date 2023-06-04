using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EAFUExample : MonoBehaviour
{
    [Header("EAFU")]
    [SerializeField] private EAFU.EAFU eafu;

    // EAFU: Add APIs Here (..or anywhere you please, just a suggestion)
    // API Created for Example
    [SerializeField] private GameApi gameApi;

    [Header("Game Cube")]
    [SerializeField] private Rotation rotation;
    [SerializeField] private Pulse pulse;
    [SerializeField] private ChangeColor changeColor;

    [Header("Game UI")]
    [SerializeField] private Slider gameDuration_Slider;
    [SerializeField] private Text gameDuraton_Text;
    [SerializeField] private Text score_Text;
    [SerializeField] private Text playerName_Text;
    [SerializeField] private Button restartGame_Button;
    [SerializeField] private GameObject loading_Image;
    [SerializeField] private GameObject leaderboard_Panel;
    [SerializeField] private List<Text> leaderboardPositions;

    private int gameDuration;
    private int score;
    private float elapsedTime;
    private bool playerCreatedEventInvoked;
    private string playerName;

    // Start is called before the first frame update
    void Start()
    {
        //EAFU: Add Your Own Error Handling
        eafu.OnError.AddListener(HandleError);

        // Example Setup
        gameDuration = Random.Range(5, 7);
        score = 0;
        playerName = WordGenerator.GenerateRandomWord();

        playerName_Text.text = playerName;
        gameDuration_Slider.maxValue = gameDuration;
        score_Text.text = score.ToString();
        restartGame_Button.onClick.AddListener(() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void HandleError(object error)
    {
        Debug.Log($"EAFU Caught Error: {error}");
        IsCompleted();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Game Duration
        elapsedTime += Time.deltaTime; // Add the time since the last frame to the elapsed time
        float remainingTime = gameDuration - elapsedTime;

        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        // Check for Game Over
        if (remainingTime <= 0f)
        {
            remainingTime = 0f;

            // Game over or other actions when the time is up
            Debug.Log("Time's up!");
            pulse.DisablePulseVFX();

            if (playerCreatedEventInvoked) return;

            // Trigger the PlayerCreated event
            playerCreatedEventInvoked = true;
            gameApi.CreatePlayer(playerName, score, gameDuration, o => 
                gameApi.GetLeaderboard(SetLeaderboard));
        }

        // Get Player Input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            score += Random.Range(1, 3);
            score_Text.text = score.ToString();
            rotation.IncreaseRotation();
            pulse.IncreasePulse();
            pulse.EnablePulseVFX();
        }

        // Update UI Elements
        gameDuraton_Text.text = remainingTime.ToString("F");
        gameDuration_Slider.value = remainingTime;
    }

    public void SetLeaderboard(List<Player> leaderboard)
    {
        // Set initial color of all texts to white
        foreach (var positionText in leaderboardPositions)
        {
            positionText.color = Color.white;
        }

        for (int i = 0; i < leaderboard.Count; i++)
        {
            // Set Leaderboard Data to Leaderboard UI
            var leaderboardPosition = leaderboard[i];
            leaderboardPositions[i].text = i < leaderboardPositions.Count
                ? $"{i + 1}. "
                  + $"{leaderboardPosition.Name}"
                  + $":{leaderboardPosition.Score}"
                : $"{i + 1}.";

            // Check if the player is in the leaderboard for each position
            if (leaderboardPosition.Id == gameApi.player.Id)
            {
                changeColor.ChangeCubeColor();
                leaderboardPositions[i].color = changeColor.Color;
                pulse.EnablePulseVFX();
            }
        }

        leaderboard_Panel.SetActive(true);
    }

    public void IsLoading()
    {
        loading_Image.SetActive(true);
    }

    public void IsCompleted()
    {
        loading_Image.SetActive(false);
    }
}
