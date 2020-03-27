using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Score controller - handles score and coins, controls difficulty level and game over event
public class ScoreController : MonoBehaviour, IEventHandler
{
    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Text coinText = null;

    [SerializeField]
    private GameOverUI gameOverUI = null; // To show game over menu

    // Changeable values
    private float score = 0.0f;
    private int coins = 0;
    
    // Level values
    private int difficultyLevel = 1;
    [SerializeField]
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;

    private void Start()
    {
        coins = PlayerPrefs.GetInt(PrefsNames.COINS_AMOUNT, 0);
        coinText.text = coins.ToString();
    }

    void Update()
    {
        if (score >= scoreToNextLevel)
            LevelUp();

        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }

    // Level up method - increases player's speed and difficulty level
    private void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerController>().SetSpeed(difficultyLevel);
    }

    // Events methods

    public void OnCoinPickedUp()
    {
        coins++;
        coinText.text = coins.ToString();
        PlayerPrefs.SetInt(PrefsNames.COINS_AMOUNT, coins);
        SoundHelper.PlayCoinSound();
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        gameOverUI.ShowGameOverUI((int)score);
    }
}

public interface IEventHandler : IEventSystemHandler
{
    void OnCoinPickedUp();
    void OnGameOver();
}