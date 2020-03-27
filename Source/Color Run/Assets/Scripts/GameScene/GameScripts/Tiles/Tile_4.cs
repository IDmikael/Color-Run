using UnityEngine;

// Tile with moving forwards and backwards cubes
public class Tile_4 : Tile
{
    [SerializeField]
    private GameObject mainColorCubes = null;
    [SerializeField]
    private GameObject secondaryColorCubes = null;

    // Movable cubes movement values
    private float delta = 2f;  // Amount to move left and right from the start point
    private float speed = 2.0f;
    private Vector3 startPos;

    protected override void Start()
    {
        base.Start();

        ChangeMaterialColor(mainColorCubes, secondaryColorCubes);

        startPos = mainColorCubes.transform.position;
    }

    void Update()
    {
        // Make cubes move forwards and backwards
        Vector3 vecMain = startPos;
        Vector3 vecSecondary = startPos;
        vecMain.x -= delta * Mathf.Sin(Time.time * speed);
        vecSecondary.x += delta * Mathf.Sin(Time.time * speed);
        mainColorCubes.transform.position = vecMain;
        secondaryColorCubes.transform.position = vecSecondary;
    }
}
