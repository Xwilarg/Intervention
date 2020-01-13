using System.Linq;

public class Squad : AEquipement
{
    public Squad(string name, string description, AEquipement[] content) : base(name, description + "\n\nContent: " + string.Join(", ", content.Select(x => x.GetName())), content.Sum(x => x.GetPrice()))
    { }

    public override Type GetEquipementType()
        => (Type)(-1);

    public AEquipement[] GetContent()
        => content;

    private AEquipement[] content;
}
