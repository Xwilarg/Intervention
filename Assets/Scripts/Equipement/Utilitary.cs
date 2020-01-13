public class Utilitary : AEquipement
{
    public Utilitary(string name, string description, int price) : base(name, description, price)
    { }

    public override Type GetEquipementType()
        => Type.Equipement;
}
