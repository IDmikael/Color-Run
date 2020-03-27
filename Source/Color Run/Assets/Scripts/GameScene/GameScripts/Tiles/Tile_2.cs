using UnityEngine;

// Tile with ground cube which player need to jump over
public class Tile_2 : Tile
{
    [SerializeField]
    private GameObject groundCube = null;

    [SerializeField]
    private GameObject cubes = null;
    [SerializeField]
    private GameObject mainColorSpheres = null;
    [SerializeField]
    private GameObject secondaryColorSpheres = null;

    protected override void Start()
    {
        base.Start();

        groundCube.GetComponent<Renderer>().material.color = secondaryColor;
        RandomizeColorsInParent(cubes);
        ChangeMaterialColor(mainColorSpheres, secondaryColorSpheres);
    }
}
