using UnityEngine;

// Tile with binded road
public class Tile_5 : Tile
{
    [SerializeField]
    private GameObject mainColorCylinders = null;
    [SerializeField]
    private GameObject secondaryColorCylinders = null;

    protected override void Start()
    {
        base.Start();

        ChangeMaterialColor(mainColorCylinders, secondaryColorCylinders);
    }
}
