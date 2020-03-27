using UnityEngine;

// Tile with springboard
public class Tile_3 : Tile
{
    [SerializeField]
    private GameObject[] singleCubes = null;
    [SerializeField]
    private GameObject[] archCubes = null;

    [SerializeField]
    private GameObject springboard = null;
    [SerializeField]
    private GameObject springboardCubes = null;

    protected override void Start()
    {
        base.Start();

        ChangeSingleObjectsColor();

        foreach(GameObject archCube in archCubes)
            RandomizeColorsInParent(archCube);

        ChangeMaterialColor(null, springboardCubes);
    }

    private void ChangeSingleObjectsColor()
    {
        singleCubes[0].GetComponent<Renderer>().material.color = secondaryColor;
        singleCubes[1].GetComponent<Renderer>().material.color = mainColor;

        springboard.GetComponent<Renderer>().material.color = mainColor;
    }
}
