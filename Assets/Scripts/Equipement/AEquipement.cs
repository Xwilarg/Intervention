public abstract class AEquipement
{
    protected AEquipement(string mname, string mdescription, int mprice)
    {
        name = mname;
        description = mdescription;
        price = mprice;
    }

    private string name;
    private string description;
    private int price;

    public string GetName()
        => name;

    public string GetDescription()
        => description;

    public int GetPrice()
        => price;

    public abstract Type GetEquipementType();

    public enum Type // Used for shop
    {
        Troop,
        Equipement,
        Logistic,
        Specialization,
        Spell
    }
}
