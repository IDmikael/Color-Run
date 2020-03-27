using UnityEngine;

public class Tile_1 : Tile
{
    [SerializeField]
    private GameObject cubes = null;
    [SerializeField]
    private GameObject mainColorCylinders = null;
    [SerializeField]
    private GameObject secondaryColorCylinders = null;

    protected override void Start()
    {
        // Call parent method to initialize colors
        base.Start();
        
        // Colorize tile objects
        RandomizeColorsInParent(cubes);
        ChangeMaterialColor(mainColorCylinders, secondaryColorCylinders);
    }
}
