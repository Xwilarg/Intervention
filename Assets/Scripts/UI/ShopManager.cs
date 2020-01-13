using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Text creditsDisplay;

    [SerializeField]
    private RectTransform transformTroops, transformEquipements, transformLogistics, transformSquads;
    private int troopIndex, equipementIndex, logisticIndex;

    [SerializeField]
    private GameObject shopElementPrefab;

    private int credits;
    private Inventory inventory;
    private List<ShopElement> shopElems; // All the elements of the shop

    private const float yOffset = -150f;

    private void Start()
    {
        troopIndex = -1;
        equipementIndex = -1;
        logisticIndex = -1;
        credits = 1000;
        inventory = GetComponent<Inventory>();
        shopElems = new List<ShopElement>();
        DisplayCredits();
        DisplayShop();
        CheckAvailability();
    }

    private void DisplayCredits()
    {
        creditsDisplay.text = "Remaining Credits: " + credits;
    }

    private void Buy(ShopElement elem)
    {
        inventory.AddEquipement(elem.GetEquipement());
        RemoveMoney(elem);
    }

    private void BuySquad(ShopElement elem)
    {
        var squad = availableSquads.Where(x => x.name == elem.GetEquipement().name).First(); // We find the corresponding squad given the shop element
        foreach (var e in squad.content) // Add all elements of the squad in the inventory
            inventory.AddEquipement(e);
        RemoveMoney(elem);
    }

    /// <summary>
    /// Remove some money after buying an item
    /// </summary>
    /// <param name=""></param>
    private void RemoveMoney(ShopElement elem)
    {
        credits -= elem.GetPrice();
        CheckAvailability();
        DisplayCredits();
    }

    /// <summary>
    /// Check for all shop items if the player have enough money to buy them
    /// </summary>
    private void CheckAvailability()
    {
        foreach (var e in shopElems)
            e.CheckCanBuy(credits);
    }

    private void DisplayShop()
    {
        foreach (var eq in available) // Prepare shop with equipement
        {
            RectTransform currTransform; // Get the correct parent panel
            int index;
            if (eq.type == Equipement.Type.Troop)
            {
                currTransform = transformTroops;
                troopIndex++;
                index = troopIndex;
            }
            else if (eq.type == Equipement.Type.Equipement)
            {
                currTransform = transformEquipements;
                equipementIndex++;
                index = equipementIndex;
            }
            else if (eq.type == Equipement.Type.Logistic)
            {
                currTransform = transformLogistics;
                logisticIndex++;
                index = logisticIndex;
            }
            else
                continue;

            GameObject go = Instantiate(shopElementPrefab, currTransform);
            var rTransform = go.GetComponent<RectTransform>();
            rTransform.anchoredPosition = rTransform.anchoredPosition + new Vector2(0f, yOffset * index);
            ShopElement se = go.GetComponent<ShopElement>();
            se.Init(eq, Buy);
            shopElems.Add(se);
        }
        int i = 0;
        foreach (var sq in availableSquads)
        {

            GameObject go = Instantiate(shopElementPrefab, transformSquads);
            var rTransform = go.GetComponent<RectTransform>();
            rTransform.anchoredPosition = rTransform.anchoredPosition + new Vector2(0f, yOffset * i);
            ShopElement se = go.GetComponent<ShopElement>();
            se.Init(new Equipement(sq.name, sq.description + "\n\nContent: " + string.Join(", ", sq.content.Select(x => x.name)), sq.content.Sum(x => x.price), (Equipement.Type)(-1)), BuySquad);
            shopElems.Add(se);
            i++;
        }
    }

    private readonly Equipement[] available = new[]
    {
        speNone,
        recruit, secondClass, firstClass, carporal, sergeant,
        assaultGun
    };

    private readonly Squad[] availableSquads = new[] { basicSquad, trainedSquad };

    /// ---------- SPECIALIZATIONS
    private static readonly Equipement speNone = new Equipement("None", "No specialization. Start with a Handgun.", 0, Equipement.Type.Specialisation);
    private static readonly Equipement speMed = new Equipement("Medicin", "Medical specialization, can make better use of First Aid Kit. Start with a Handgun and a First Aid Kit.", 150, Equipement.Type.Specialisation);
    private static readonly Equipement speEngi = new Equipement("Engineer", "Engineer specialization, can make small cover with the surronding furniture. Start with a Handgun and a Retractable Shovel.", 150, Equipement.Type.Specialisation);
    private static readonly Equipement speMage = new Equipement("Mage", "Mage specialization, can throw spells. Can only use handguns. Start with an elemental gem.", 250, Equipement.Type.Specialisation);
    private static readonly Equipement speSniper = new Equipement("Sniper", "Sniper specialization, will not go inside the building and provide cover from a near building. May only shoot if the building have windows.", 250, Equipement.Type.Specialisation);

    /// ---------- SPELLS
    private static readonly Equipement spellWall = new Equipement("Wall", "Raise the floor to create a wall . Only work at the lowest floor.", 0, Equipement.Type.Spell);
    private static readonly Equipement spellStabilize = new Equipement("Stabilize", "Stabilize the bleeding of a soldier.", 0, Equipement.Type.Spell);
    private static readonly Equipement spellHeal = new Equipement("Heal", "Increase the recover speed of a soldier.", 0, Equipement.Type.Spell);
    private static readonly Equipement spellProjectile = new Equipement("Projectile", "Create a projectile from the surronding environment and throw it towards the enemy at high speed.", 0, Equipement.Type.Spell);
    private static readonly Equipement spellDispell = new Equipement("Dispell", "Neutralize an enemy spell.", 0, Equipement.Type.Spell);

    /// ---------- TROOPS
    private static readonly Equipement recruit = new Equipement("Recruit", "Freshly recruited soldier, have no experience on the field.\nDon't expect too much from them.", 50, Equipement.Type.Troop);
    private static readonly Equipement secondClass = new Equipement("Second Class", "Soldier that only did a few missions on the field.", 150, Equipement.Type.Troop);
    private static readonly Equipement firstClass = new Equipement("First Class", "Soldier that already have a lot of experience on the field. Second class soldier and higher can have a specialization.", 200, Equipement.Type.Troop);
    private static readonly Equipement carporal = new Equipement("Carporal", "Along with being an experienced soldier, have some knowledge in leadership.", 300, Equipement.Type.Troop);
    private static readonly Equipement sergeant = new Equipement("Sergeant", "Veterant soldier who also have a lot of experience in leadership.", 400, Equipement.Type.Troop);

    /// ---------- EQUIPEMENTS
    private static readonly Equipement assaultGun = new Equipement("Assault Rifle", "A standard rifle with a good rate of fire. Usually the basic weapon given to a soldier.", 50, Equipement.Type.Equipement);
    private static readonly Equipement smg = new Equipement("Submachine Gun", "A light automatic gun, less powerful than an assault gun. Is however a good weapon for closer quarter combat since they are easier to manipulate.", 40, Equipement.Type.Equipement);
    private static readonly Equipement shotgun = new Equipement("Shotgun", "A powerful weapon that shoot many projectile. Really useful for close quarter combat.", 75, Equipement.Type.Equipement);
    private static readonly Equipement hmg = new Equipement("Heavy Machine Gun", "A heavy gun that need to be deployed before being used. Really useful to defend a position.", 150, Equipement.Type.Equipement);

    private static readonly Equipement baslisticVest = new Equipement("Combat Vest", "Protect the torso against light guns and others small projectiles.", 25, Equipement.Type.Equipement);
    private static readonly Equipement baslisticHelmet = new Equipement("Combat Helmet", "Protect the head against light guns and others small projectiles.", 35, Equipement.Type.Equipement);
    private static readonly Equipement balisticShield = new Equipement("Balistic Shield", "A shield that protect against bullets. May only be used with a handgun.", 70, Equipement.Type.Equipement);

    private static readonly Equipement stungun = new Equipement("Stungun", "An incapacitating weapon that must be used in close contact.", 10, Equipement.Type.Equipement);
    private static readonly Equipement tazer = new Equipement("Stungun", "An incapacitating gun that must be used in short range.", 25, Equipement.Type.Equipement);

    private static readonly Equipement explosiveGrenade = new Equipement("Explosive Grenade", "A grenade that can do explosive damage in a small zone.", 20, Equipement.Type.Equipement);
    private static readonly Equipement flashGrenade = new Equipement("Flash Grenade", "A grenade that temporarily blind people in a small zone.", 25, Equipement.Type.Equipement);
    private static readonly Equipement antiMagicGrenade = new Equipement("Anti Magic Grenade", "A grenade that do an small antic magic area where thrown.", 100, Equipement.Type.Equipement);
    private static readonly Equipement claymore = new Equipement("Claymore", "A small device that shoot metal balls when someone trip on a wire.", 150, Equipement.Type.Equipement);

    private static readonly Equipement firstAidKit = new Equipement("First Aid Kit", "A medical kit that can stabilize in critical state.", 20, Equipement.Type.Equipement);
    private static readonly Equipement medicalKit = new Equipement("Medical Kit", "A full medical kit that can treat even the worse injuries. May only be use by a medecin", 50, Equipement.Type.Equipement);
    private static readonly Equipement reanimationKit = new Equipement("Reanimation Kit", "A one use reanimation kit, allow to save someone whose heart stop beating. May only be use by a medecin", 75, Equipement.Type.Equipement);
    private static readonly Equipement defuseKit = new Equipement("Defuse Kit", "Allow to desarm bombs. May only be used by an engineer.", 20, Equipement.Type.Equipement);
    private static readonly Equipement doorCamera = new Equipement("Under Door Camera", "Small camera you put under a door to observe the inside of a room.", 150, Equipement.Type.Equipement);
    private static readonly Equipement retractableShovel = new Equipement("Retractable Shovel", "Allow to remove debris or break furniture in small parts.", 20, Equipement.Type.Equipement);
    private static readonly Equipement nightVision = new Equipement("Night Vision", "Improve vision in the dark.", 100, Equipement.Type.Equipement);
    private static readonly Equipement magicAnalyzer = new Equipement("Magic Analyzer", "Analyze the magic concentration in the near air.", 200, Equipement.Type.Specialisation);

    /// ---------- LOGISTICS
    private static readonly Squad basicSquad = new Squad("Intervention Squad", "A basic intervention squad", new[] { carporal, secondClass, secondClass, secondClass, assaultGun, assaultGun, assaultGun, assaultGun });
    private static readonly Squad trainedSquad = new Squad("Trained Intervention Squad", "An intervention squad with high level soldiers.", new[] { sergeant, firstClass, firstClass, firstClass, assaultGun, assaultGun, assaultGun, assaultGun });
    private static readonly Squad defenseSquad = new Squad("Defense Squad", "A squad formed to defend a position", new[] { carporal, secondClass, secondClass, secondClass, assaultGun, assaultGun, assaultGun, hmg, claymore, claymore });
    private static readonly Squad offensiveSquad = new Squad("Offensive Squad", "A squad formed for highly offensive attack", new[] { carporal, secondClass, secondClass, secondClass, smg, smg, smg, shotgun, explosiveGrenade, explosiveGrenade, flashGrenade, flashGrenade });
}
