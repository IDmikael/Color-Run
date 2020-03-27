using UnityEngine;

// Tile with circling spheres
public class Tile_6 : Tile
{
    [SerializeField]
    private GameObject[] spheres = null;

    // Movable spheres movement values
    [SerializeField]
    private Transform[] centers = null; // Circles centers
    private float degrees = 130.0f; // Moving speed in degrees

    protected override void Start()
    {
        base.Start();

        foreach (GameObject sphere in spheres)
        {
            sphere.GetComponent<Renderer>().material.color = secondaryColor;
        };
    }

    private void Update()
    {

        spheres[0].transform.RotateAround(centers[0].position, Vector3.up, degrees * Time.deltaTime);
        spheres[1].transform.RotateAround(centers[1].position, Vector3.down, degrees * Time.deltaTime);
    }
}
