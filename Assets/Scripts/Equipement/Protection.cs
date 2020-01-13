public class Protection : AEquipement
{
    public Protection(string name, string description, int price) : base(name, description, price)
    { }

    public override Type GetEquipementType()
        => Type.Equipement;
}
