using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Text creditsDisplay;

    [SerializeField]
    private RectTransform transformTroops, transformEquipements, transformLogistics, transformSquads;

    private int credits;

    private void Start()
    {
        credits = 1000;
        DisplayCredits();
    }

    private void DisplayCredits()
    {
        creditsDisplay.text = "Remaining Credits: " + credits;
    }

    /// ---------- SPECIALIZATIONS
    private readonly Equipement speNone = new Equipement("None", "No specialization. Start with a Handgun.", 0, Equipement.Type.Specialisation);
    private readonly Equipement speMed = new Equipement("Medicin", "Medical specialization, can make better use of First Aid Kit. Start with a Handgun and a First Aid Kit.", 150, Equipement.Type.Specialisation);
    private readonly Equipement speEngi = new Equipement("Engineer", "Engineer specialization, can make small cover with the surronding furniture. Start with a Handgun and a Retractable Shovel.", 150, Equipement.Type.Specialisation);
    private readonly Equipement speMage = new Equipement("Mage", "Mage specialization, can throw spells. Can only use handguns. Start with an elemental gem.", 250, Equipement.Type.Specialisation);
    private readonly Equipement speSniper = new Equipement("Sniper", "Sniper specialization, will not go inside the building and provide cover from a near building. May only shoot if the building have windows.", 250, Equipement.Type.Specialisation);

    /// ---------- SPELLS
    private readonly Equipement spellWall = new Equipement("Wall", "Raise the floor to create a wall . Only work at the lowest floor.", 0, Equipement.Type.Spell);
    private readonly Equipement spellStabilize = new Equipement("Stabilize", "Stabilize the bleeding of a soldier.", 0, Equipement.Type.Spell);
    private readonly Equipement spellHeal = new Equipement("Heal", "Increase the recover speed of a soldier.", 0, Equipement.Type.Spell);
    private readonly Equipement spellProjectile = new Equipement("Projectile", "Create a projectile from the surronding environment and throw it towards the enemy at high speed.", 0, Equipement.Type.Spell);
    private readonly Equipement spellDispell = new Equipement("Dispell", "Neutralize an enemy spell.", 0, Equipement.Type.Spell);

    /// ---------- TROOPS
    private readonly Equipement recruit = new Equipement("Recruit", "Freshly recruited soldier, have no experience on the field.", 100, Equipement.Type.Troop);
    private readonly Equipement firstClass = new Equipement("First Class", "Soldier that only did a few missions on the field.", 150, Equipement.Type.Troop);
    private readonly Equipement secondClass = new Equipement("Second Class", "Soldier that already have a lot of experience on the field. Second class soldier and higher can have a specialization.", 200, Equipement.Type.Troop);
    private readonly Equipement carporal = new Equipement("Carporal", "Along with being an experienced soldier, have some knowledge in leadership.", 300, Equipement.Type.Troop);
    private readonly Equipement sergeant = new Equipement("Sergeant", "Veterant soldier who also have a lot of experience in leadership.", 400, Equipement.Type.Troop);

    /// ---------- EQUIPEMENTS
    private readonly Equipement assaultGun = new Equipement("Assault Rifle", "A standard rifle with a good rate of fire. Usually the basic weapon given to a soldier.", 50, Equipement.Type.Equipement);
    private readonly Equipement smg = new Equipement("Submachine Gun", "A light automatic gun, less powerful than an assault gun. Is however a good weapon for closer quarter combat since they are easier to manipulate.", 40, Equipement.Type.Equipement);
    private readonly Equipement shotgun = new Equipement("Shotgun", "A powerful weapon that shoot many projectile. Really useful for close quarter combat.", 75, Equipement.Type.Equipement);
    private readonly Equipement hmg = new Equipement("Heavy Machine Gun", "A heavy gun that need to be deployed before being used. Really useful to defend a position.", 150, Equipement.Type.Equipement);

    private readonly Equipement baslisticVest = new Equipement("Combat Vest", "Protect the torso against light guns and others small projectiles.", 25, Equipement.Type.Equipement);
    private readonly Equipement baslisticHelmet = new Equipement("Combat Helmet", "Protect the head against light guns and others small projectiles.", 35, Equipement.Type.Equipement);
    private readonly Equipement balisticShield = new Equipement("Balistic Shield", "A shield that protect against bullets. May only be used with a handgun.", 70, Equipement.Type.Equipement);

    private readonly Equipement stungun = new Equipement("Stungun", "An incapacitating weapon that must be used in close contact.", 10, Equipement.Type.Equipement);
    private readonly Equipement tazer = new Equipement("Stungun", "An incapacitating gun that must be used in short range.", 25, Equipement.Type.Equipement);

    private readonly Equipement explosiveGrenade = new Equipement("Explosive Grenade", "A grenade that can do explosive damage in a small zone.", 20, Equipement.Type.Equipement);
    private readonly Equipement flashGrenade = new Equipement("Flash Grenade", "A grenade that temporarily blind people in a small zone.", 25, Equipement.Type.Equipement);
    private readonly Equipement antiMagicGrenade = new Equipement("Anti Magic Grenade", "A grenade that do an small antic magic area where thrown.", 100, Equipement.Type.Equipement);
    private readonly Equipement claymore = new Equipement("Claymore", "A small device that shoot metal balls when someone trip on a wire.", 150, Equipement.Type.Equipement);

    private readonly Equipement firstAidKit = new Equipement("First Aid Kit", "A medical kit that can stabilize in critical state.", 20, Equipement.Type.Equipement);
    private readonly Equipement medicalKit = new Equipement("Medical Kit", "A full medical kit that can treat even the worse injuries. May only be use by a medecin", 50, Equipement.Type.Equipement);
    private readonly Equipement reanimationKit = new Equipement("Reanimation Kit", "A one use reanimation kit, allow to save someone whose heart stop beating. May only be use by a medecin", 75, Equipement.Type.Equipement);
    private readonly Equipement defuseKit = new Equipement("Defuse Kit", "Allow to desarm bombs. May only be used by an engineer.", 20, Equipement.Type.Equipement);
    private readonly Equipement doorCamera = new Equipement("Under Door Camera", "Small camera you put under a door to observe the inside of a room.", 150, Equipement.Type.Equipement);
    private readonly Equipement retractableShovel = new Equipement("Retractable Shovel", "Allow to remove debris or break furniture in small parts.", 20, Equipement.Type.Equipement);
    private readonly Equipement nightVision = new Equipement("Night Vision", "Improve vision in the dark.", 100, Equipement.Type.Equipement);
    private readonly Equipement magicAnalyzer = new Equipement("Magic Analyzer", "Analyze the magic concentration in the near air.", 200, Equipement.Type.Specialisation);

    /// ---------- LOGISTICS
}
