public class Weapon : AEquipement
{
    public Weapon(string name, string description, int price) : base(name, description, price)
    { }

    public override Type GetEquipementType()
        => Type.Equipement;
}
