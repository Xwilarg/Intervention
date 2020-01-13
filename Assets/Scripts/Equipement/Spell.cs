public class Spell : AEquipement
{
    public Spell(string name, string description, int price) : base(name, description, price)
    { }

    public override Type GetEquipementType()
        => Type.Spell;
}
