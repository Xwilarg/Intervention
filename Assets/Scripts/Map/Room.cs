using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Room
{
    public Room(Type roomType)
    {
        _roomType = roomType;
        _roomUp = null;
        _roomDown = null;
        _roomLeft = null;
        _roomRight = null;
    }

    public Type GetRoomType()
        => _roomType;

    public ReadOnlyCollection<Direction> GetAvailablePositions()
    {
        List<Direction> pos = new List<Direction>();
        if (_roomUp == null) pos.Add(Direction.Up);
        if (_roomDown == null) pos.Add(Direction.Down);
        if (_roomLeft == null) pos.Add(Direction.Left);
        if (_roomRight == null) pos.Add(Direction.Right);
        return pos.AsReadOnly();
    }

    private Type _roomType;

    /// The room keep track of the others rooms next to it
    private Room _roomUp, _roomDown, _roomLeft, _roomRight;

    public Room GetRoomUp() => _roomUp;
    public Room GetRoomDown() => _roomDown;
    public Room GetRoomLeft() => _roomLeft;
    public Room GetRoomRight() => _roomRight;

    public void SetRoomUp(Room room)
    {
        if (_roomUp == null) _roomUp = room;
        else throw new System.ArgumentException("Room up is already taken");
    }
    public void SetRoomDown(Room room)
    {
        if (_roomDown == null) _roomDown = room;
        else throw new System.ArgumentException("Room down is already taken");
    }
    public void SetRoomLeft(Room room)
    {
        if (_roomLeft == null) _roomLeft = room;
        else throw new System.ArgumentException("Room left is already taken");
    }
    public void SetRoomRight(Room room)
    {
        if (_roomRight == null) _roomRight = room;
        else throw new System.ArgumentException("Room right is already taken");
    }

    public enum Type
    {
        Entrance, // Entrance of the building
        Normal // Any other kind of room
    }
}
