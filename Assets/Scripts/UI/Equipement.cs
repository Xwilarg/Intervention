public struct Equipement
{
    public Equipement(string mname, string mdescription, int mprice, Type mtype)
    {
        name = mname;
        description = mdescription;
        price = mprice;
        type = mtype;
    }

    public string name;
    public string description;
    public int price;
    public Type type;

    public enum Type
    {
        Troop,
        Equipement,
        Logistic,
        Specialisation,
        Spell
    }
}
