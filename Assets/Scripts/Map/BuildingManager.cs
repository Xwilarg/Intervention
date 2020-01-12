using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject floorTile, wallTile;

    private Floor floor;
    private const int roomSize = 10; // Number of tile per room
    private const float tileSize = .1f; // Size of a tile in the game world

    private List<GameObject> tiles;

    private void Start()
    {
        tiles = new List<GameObject>();
        floor = new Floor();
        RenderFloor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (GameObject go in tiles)
                Destroy(go);
            tiles.Clear();
            floor = new Floor();
            RenderFloor();
        }
    }

    private void RenderFloor()
    {
        var rooms = floor.GetRooms();
        int mostLeft = rooms.Min(x => x.Item1.x);
        int mostUp = rooms.Max(x => x.Item1.y);
        foreach (var room in rooms)
        {
            for (int x = 0; x < roomSize; x++)
            {
                for (int y = 0; y < roomSize; y++)
                {
                    Vector3 pos = ((room.Item1 * (roomSize - 1)) + new Vector2(x, y)) * tileSize;
                    if (tiles.Any(e => e.transform.position == pos))
                        continue;
                    GameObject go = Instantiate((x == 0 || y == 0 || x == roomSize - 1 || y == roomSize - 1) ? wallTile : floorTile,
                        pos, Quaternion.identity);
                    tiles.Add(go);
                }
            }
        }
    }
}
