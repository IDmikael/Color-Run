using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    protected Color mainColor;
    protected Color secondaryColor;

    private GameController gameController;

    // Coin spawn fields
    [SerializeField]
    protected GameObject coinPrefab = null;

    [SerializeField]
    protected float[] coinSpawnZposes = null;

    protected float[] coinSpawnXposes = new float[] { -2.6f, 0, 2.6f };

    protected virtual void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        mainColor = gameController.mainColor;
        secondaryColor = gameController.secondaryColor;
        SpawnCoin();
    }

    // For sets of objects with manually chosen colors
    protected void ChangeMaterialColor(GameObject mainColorParent, GameObject secondaryColorParent)
    {
        if (mainColorParent != null)
        {
            foreach (Transform child in mainColorParent.transform)
            {
                child.gameObject.GetComponent<Renderer>().material.color = mainColor;
            }
        }

        if (secondaryColorParent != null)
        {
            foreach (Transform child in secondaryColorParent.transform)
            {
                child.gameObject.GetComponent<Renderer>().material.color = secondaryColor;
            }
        }
    }

    // For objects with certain amount of objects which color is need to randomize
    protected void RandomizeColorsInParent(GameObject parent)
    {
        // Choose how many objects with main color will be
        int mainColorObjects = Random.Range(1, parent.transform.childCount - 1);

        // Paint that objects with main color randomly
        for (int i = 0; i < mainColorObjects; i++)
        {
            parent.transform.GetChild(Random.Range(0, parent.transform.childCount))
                .GetComponent<Renderer>().material.color = mainColor;
        }

        // Paint rest of the objects with secondary color
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.GetComponent<Renderer>().material.color != mainColor)
                child.gameObject.GetComponent<Renderer>().material.color = secondaryColor;
        }
    }

    // Coin spawner
    protected void SpawnCoin()
    {
        // If I suddenly change my mind about spawning a coin on this tile
        if (coinSpawnZposes == null || coinSpawnZposes.Length == 0)
            return;

        GameObject coin = Instantiate(coinPrefab);
        coin.transform.parent = transform;

        // Randomize coin spawn according to manually chosen positions
        coin.transform.localPosition = new Vector3(
            coinSpawnXposes[Random.Range(0, coinSpawnXposes.Length)],
            0.73f, coinSpawnZposes[Random.Range(0, coinSpawnZposes.Length)]
            );
    }
}
