using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject floorTile, wallTile, doorTile;

    private Floor floor;
    private const int roomSize = 11; // Number of tile per room, must be an even number because of doors
    private const float tileSize = .1f; // Size of a tile in the game world
    private List<GameObject> tiles;
    private bool showInside;

    private void Start()
    {
        showInside = false;
        tiles = new List<GameObject>();
        floor = new Floor();
        RenderFloor(showInside);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Restart generation
        {
            floor = new Floor();
            UpdateRender();
        }
        else if (Input.GetKeyDown(KeyCode.U)) // Show/Hide inside of building
        {
            showInside = !showInside;
            UpdateRender();
        }
    }

    private void UpdateRender()
    {
        foreach (GameObject go in tiles)
            Destroy(go);
        tiles.Clear();
        RenderFloor(showInside);
    }

    private void RenderFloor(bool showInside)
    {
        var rooms = floor.GetRooms();
        int mostLeft = rooms.Min(x => x.Key.x);
        int mostUp = rooms.Max(x => x.Key.y);
        Dictionary<Vector2Int, GameObject> toRender = new Dictionary<Vector2Int, GameObject>();
        foreach (var room in rooms)
        {
            for (int x = 0; x < roomSize; x++)
            {
                for (int y = 0; y < roomSize; y++)
                {
                    Vector2Int pos = (room.Key * (roomSize - 1)) + new Vector2Int(x, y);
                    if (toRender.ContainsKey(pos)) // We already draw that tile (some walls overlaps)
                        continue;

                    GameObject currTile;
                    if (!showInside) // Normally we don't show the inside of buildings
                        currTile = wallTile;
                    else if (x == 0 || y == 0 || x == roomSize - 1 || y == roomSize - 1) // Border of rooms
                        currTile = wallTile;
                    else
                        currTile = floorTile;

                    toRender.Add(pos, currTile);
                }
            }
        }
        if (showInside) // Generate inside doors
        {
            foreach (var room in rooms)
            {
                Vector2Int pos = room.Key * (roomSize - 1);
                if (room.Value.GetRoomDown() != null)
                    toRender[pos + (Vector2Int.right * roomSize / 2)] = doorTile;
                if (room.Value.GetRoomUp() != null)
                    toRender[pos + new Vector2Int(roomSize / 2, roomSize - 1)] = doorTile;
                if (room.Value.GetRoomLeft() != null)
                    toRender[pos + (Vector2Int.up * roomSize / 2)] = doorTile;
                if (room.Value.GetRoomRight() != null)
                    toRender[pos + new Vector2Int(roomSize - 1, roomSize / 2)] = doorTile;

            }
        }
        foreach (var elem in toRender) // Instantiate everything
        {
            tiles.Add(Instantiate(elem.Value, (Vector2)elem.Key * tileSize, Quaternion.identity));
        }
    }
}
