using System.Collections.Generic;
using UnityEngine;

// Actually, it's a color controller, but who cares (=
public class GameController : MonoBehaviour
{
    public Color mainColor;
    public Color secondaryColor;

    [SerializeField]
    private List<Color> colorsList = 
        new List<Color>() { Color.black, Color.blue, Color.green, Color.red, Color.white, Color.yellow };

    private void Awake()
    {
        RandomizeColors();
        Debug.Log("Main clor: " + mainColor + ", " + "Secondary color: " + secondaryColor);
    }

    private void RandomizeColors()
    {
        List<Color> tmp = colorsList;

        int rand = Random.Range(0, tmp.Count);
        mainColor = tmp[rand];
        tmp.RemoveAt(rand);

        rand = Random.Range(0, tmp.Count);
        secondaryColor = tmp[rand];
        tmp.RemoveAt(rand);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SoundHelper.StopBackgroundMusic();
        else
            SoundHelper.PlayBackgroundMusic();
    }
}
