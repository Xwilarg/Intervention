using System.Text;
using UnityEngine;

public class SquadMember
{
    public SquadMember(Grade mgrade)
    {
        gender = Random.Range(0, 100) < 70 ? Gender.Male : Gender.Female;
        race = Random.Range(0, 100) < 95 ? Race.Human : Race.Elf;
        grade = mgrade;
        name = GenerateName();
    }

    private Gender gender;
    private Race race;
    private Grade grade;
    private string name;

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
        for (int i = Random.Range(2, 4); i >= 0; i--)
            str.Append(syllabes[Random.Range(0, syllabes.Length)]);
        return str.ToString();
    }
}
