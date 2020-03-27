using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    // Async loading game scene to prevent user from waiting after play button is clicked
    private AsyncOperation operation;
    
    void Start()
    {
        GetComponent<SoundHelper>().InitNativeAudio(); // Initializing ANA
        StartCoroutine(AsyncLoad());
    }

    // --------- BUTTON CLICK METHODS ----------
    public void OnPlayPressed()
    {
        SoundHelper.PlayButtonSound();
        operation.allowSceneActivation = true;
    }

    // Async loading game scene to prevent user from waiting after play button is clicked
    private IEnumerator AsyncLoad()
    {
        operation = SceneManager.LoadSceneAsync(PrefsNames.GAME_SCENE_ID);
        operation.allowSceneActivation = false;

        Debug.Log("Game Scene loaded");

        yield return null;
    }
}
