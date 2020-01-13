public class Specialization : AEquipement
{
    public Specialization(string name, string description, int price) : base(name, description, price)
    { }

    public override Type GetEquipementType()
        => Type.Specialization;
}
