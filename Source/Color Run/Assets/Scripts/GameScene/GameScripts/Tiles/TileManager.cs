using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tiles = null;

    private Transform playerTransform;

    // Tiles spawn values
    private float spawnZ = -6.0f; // spawn offset
    private float tileLenght = 40.0f;
    private float safeZone = 105.0f;

    [SerializeField]
    private int amountTilesOnScreen = 5;

    // To prevent spawning 2 similar prefabs in a row while randomize
    private int lastPrefabIndex = 0;

    // List contains spawned tiles objects for passed tiles deletion
    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amountTilesOnScreen; i++)
        {
            SpawnTile(i < 1 ? 0 : -1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amountTilesOnScreen * tileLenght))
        {
            SpawnTile();
            DeleteTile();
        }   
    }

    private void SpawnTile(int index = -1)
    {
        GameObject tile = Instantiate(tiles[index == -1 ? RandomizePrefabIndex() : index]) as GameObject;
        tile.transform.parent = transform;
        tile.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLenght;
        activeTiles.Add(tile);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomizePrefabIndex()
    {
        if (tiles.Length == 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tiles.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
