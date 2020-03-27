using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private Text currentScore = null;
    [SerializeField]
    private Text bestScore = null;

    // Async loading main scene to prevent user from waiting after home button is clicked
    private AsyncOperation operation;

    void Start()
    {
        StartCoroutine(AsyncLoad()); // Async loading main scene
        gameObject.SetActive(false); // Turning off game over menu until player dies
    }

    public void ShowGameOverUI(int score)
    {
        gameObject.SetActive(true);
        GetComponent<Animator>().Play("GameOverFadeIn"); // Plays game over animation

        currentScore.text = score.ToString();

        // Handling high score
        int bestScoreValue = PlayerPrefs.GetInt(PrefsNames.BEST_SCORE, 0);
        if (score > bestScoreValue)
        {
            bestScoreValue = score;
            PlayerPrefs.SetInt(PrefsNames.BEST_SCORE, score);
        }

        bestScore.text = bestScoreValue.ToString();
    }

    // --------- BUTTON CLICK METHODS ----------

    public void OnHomeButtonPressed()
    {
        SoundHelper.PlayButtonSound();
        Time.timeScale = 1;
        operation.allowSceneActivation = true;
    }

    public void OnReplayButtonPressed()
    {
        SoundHelper.PlayButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    // Async loading main scene to prevent user from waiting after home button is clicked
    private IEnumerator AsyncLoad()
    {
        operation = SceneManager.LoadSceneAsync(PrefsNames.MAIN_SCENE_ID);
        operation.allowSceneActivation = false;

        yield return null;
    }
}
