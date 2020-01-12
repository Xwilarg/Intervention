using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class Floor
{
    public Floor()
    {
        _rooms = new Dictionary<Vector2Int, Room>();
        Generate();
    }

    /// <summary>
    /// Generate all rooms
    /// </summary>
    private void Generate()
    {
        _rooms.Clear(); // Make sure that we don't already have some rooms (if the function is called twice)
        Room entrance = new Room(Room.Type.Entrance);
        _rooms.Add(Vector2Int.zero, entrance); // Entrance always is at 0;0
        _entranceDirection = (Direction)Random.Range(0, 4);
        Room firstRoom = new Room(Room.Type.Normal);
        _rooms.Add(DirectionToVector(_entranceDirection), firstRoom);
        BindRooms(entrance, firstRoom, _entranceDirection);

        int nbRooms = Random.Range(_minNumberOfRoom, _maxNumberOfRoom + 1);
        nbRooms -= 2; // We already put 2 rooms

        int maxTries = _maxNumberOfRoom * 10;
        while (nbRooms > 0)
        {
            maxTries--;
            if (maxTries == 0)
                throw new System.TimeoutException("Failed to generate a map after " + _maxNumberOfRoom * 10 + " tries");
            var normalRooms = _rooms.Where(x => x.Value.GetRoomType() == Room.Type.Normal);
            var selectedRoom = normalRooms.ElementAt(Random.Range(0, normalRooms.Count()));
            var directions = selectedRoom.Value.GetAvailablePositions();
            if (directions.Count == 0) // No space to place our new room
                continue;

            var roomDir = directions[Random.Range(0, directions.Count)];
            var finalPos = selectedRoom.Key + DirectionToVector(roomDir);
            if (!IsRoomPositionValid(finalPos)) // We can't place the room here
                continue;

            Room r = new Room(Room.Type.Normal);
            _rooms.Add(finalPos, r);
            BindRooms(selectedRoom.Value, r, roomDir);
            nbRooms--;
        }
    }

    /// <summary>
    /// Make sure the room doesn't obstruct the entrance or any other room
    /// </summary>
    /// <param name="pos">Position of the new room</param>
    private bool IsRoomPositionValid(Vector2Int pos)
    {
        if (_rooms.Any(x => x.Key == pos)) // Check is there is already something at the room position
            return false;
        // Make sure the line that go in the invert way to the entrance is kept empty
        // For example if the entrance (at 0;0) is up, then we won't place any room from 0;0 to 0;-infinity
        // We make that to be sure we always can enter in the building
        if (pos.x == 0 && _entranceDirection == Direction.Up && pos.y < 0 ||
            pos.x == 0 && _entranceDirection == Direction.Down && pos.y > 0 ||
            pos.y == 0 && _entranceDirection == Direction.Left && pos.x > 0 ||
            pos.y == 0 && _entranceDirection == Direction.Right && pos.x < 0)
            return false;
        return true;
    }

    public IEnumerable<System.Tuple<Vector2Int, Room.Type>> GetRooms()
        => _rooms.Select(x => new System.Tuple<Vector2Int, Room.Type>(x.Key, x.Value.GetRoomType()));

    /// <summary>
    /// Keep track of each room along with it position in the world
    /// We consider that the entrance is at 0;0 and place rooms around it
    /// </summary>
    private Dictionary<Vector2Int, Room> _rooms;
    private Direction _entranceDirection; // Where the entrace is, so we don't build of top or around it

    private const int _minNumberOfRoom = 8;
    private const int _maxNumberOfRoom = 12;

    /// <summary>
    /// Get a Vector2Int given a Direction
    /// </summary>
    private Vector2Int DirectionToVector(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Vector2Int.up;
            case Direction.Down: return Vector2Int.down;
            case Direction.Left: return Vector2Int.left;
            case Direction.Right: return Vector2Int.right;
            default: throw new System.ArgumentOutOfRangeException("Invalid Direction " + dir);
        }
    }

    /// <summary>
    /// Bind 2 rooms that are next to each other
    /// </summary>
    /// <param name="a">First room</param>
    /// <param name="b">Second room</param>
    /// <param name="dir">Direction of the second room, based on the first room position</param>
    private void BindRooms(Room a, Room b, Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                a.SetRoomUp(b);
                b.SetRoomDown(a);
                break;

            case Direction.Down:
                a.SetRoomDown(b);
                b.SetRoomUp(a);
                break;

            case Direction.Left:
                a.SetRoomLeft(b);
                b.SetRoomRight(a);
                break;

            case Direction.Right:
                a.SetRoomRight(b);
                b.SetRoomLeft(a);
                break;
        }
    }
}
