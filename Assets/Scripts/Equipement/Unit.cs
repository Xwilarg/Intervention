public class Unit : AEquipement
{
    public Unit(string name, string description, int price) : base(name, description, price)
    { }

    public override Type GetEquipementType()
        => Type.Troop;
}
