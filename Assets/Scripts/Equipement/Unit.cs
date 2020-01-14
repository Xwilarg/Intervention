using System.Text;

public class Unit : AEquipement
{
    public Unit(string name, string description, int price, Grade mgrade) : base(name, description, price)
    {
        gender = LocalRandom.RandomNumber(0, 100) < 70 ? Gender.Male : Gender.Female;
        race = LocalRandom.RandomNumber(0, 100) < 95 ? Race.Human : Race.Elf;
        grade = mgrade;
        unitName = GenerateName();
    }

    public void SetSquadLeader()
        => isSquadLeader = true;

    public void RemoveSquadLeader()
        => isSquadLeader = false;

    public override string GetName()
        => base.GetName() + (isSquadLeader ? " (Squad Leader)" : "");

    public override Type GetEquipementType()
        => Type.Troop;

    private Gender gender;
    private Race race;
    private Grade grade;
    private string unitName;
    private bool isSquadLeader;

    private enum Gender
    {
        Male,
        Female
    }

    private enum Race
    {
        Human,
        Elf
    }

    public enum Grade
    {
        PV1, // Recruit
        PV2, // Second Class
        PFC, // First Class
        CPL, // Caporal
        SGT // Sergeant
    }

    private string[] syllabes = new[] // It's easier to create random coherant names with Japanese syllabes
    {
        "a", "i", "u", "e", "o",
        "ka", "ki", "ku", "ke", "ko", "ga", "gi", "gu", "ge", "go",
        "sa", "shi", "tsu", "se", "so", "za", "ze", "zo",
        "ta", "chi", "te", "to", "da", "de", "do",
        "ma", "mi", "mu", "me", "mo",
        "na", "ni", "nu", "ne", "no",
        "ha", "hi", "fu", "he", "ho", "ba", "bi", "be", "bo", "pa", "pi", "pu", "pe", "po",
        "ra", "ri", "ru", "re", "ro",
        "wa", "wo",
        "ya", "yu", "yo"
    };

    private string GenerateName()
    {
        StringBuilder str = new StringBuilder();
        for (int i = LocalRandom.RandomNumber(2, 4); i >= 0; i--)
            str.Append(syllabes[LocalRandom.RandomNumber(0, syllabes.Length)]);
        return str.ToString();
    }
}
