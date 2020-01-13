public struct Squad
{
    public Squad(string mname, string mdescription, Equipement[] mcontent)
    {
        name = mname;
        description = mdescription;
        content = mcontent;
    }

    public string name;
    public string description;
    public Equipement[] content;
}
