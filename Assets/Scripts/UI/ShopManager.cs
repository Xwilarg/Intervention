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
        var squad = availableSquads.Where(x => x.GetName() == elem.GetEquipement().GetName()).First(); // We find the corresponding squad given the shop element
        foreach (var e in squad.GetContent()) // Add all elements of the squad in the inventory
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
            if (eq.GetEquipementType() == AEquipement.Type.Troop)
            {
                currTransform = transformTroops;
                troopIndex++;
                index = troopIndex;
            }
            else if (eq.GetEquipementType() == AEquipement.Type.Equipement)
            {
                currTransform = transformEquipements;
                equipementIndex++;
                index = equipementIndex;
            }
            else if (eq.GetEquipementType() == AEquipement.Type.Logistic)
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
            se.Init(sq, BuySquad);
            shopElems.Add(se);
            i++;
        }
    }

    private readonly AEquipement[] available = new AEquipement[]
    {
        speNone,
        recruit, secondClass, firstClass, carporal, sergeant,
        assaultGun
    };

    private readonly Squad[] availableSquads = new[] { basicSquad, trainedSquad };

    /// ---------- SPECIALIZATIONS
    private static readonly Specialization speNone = new Specialization("None", "No specialization. Start with a Handgun.", 0);
    private static readonly Specialization speMed = new Specialization("Medicin", "Medical specialization, can make better use of First Aid Kit. Start with a Handgun and a First Aid Kit.", 150);
    private static readonly Specialization speEngi = new Specialization("Engineer", "Engineer specialization, can make small cover with the surronding furniture. Start with a Handgun and a Retractable Shovel.", 150);
    private static readonly Specialization speMage = new Specialization("Mage", "Mage specialization, can throw spells. Can only use handguns. Start with an elemental gem.", 250);
    private static readonly Specialization speSniper = new Specialization("Sniper", "Sniper specialization, will not go inside the building and provide cover from a near building. May only shoot if the building have windows.", 250);

    /// ---------- SPELLS
    private static readonly Spell spellWall = new Spell("Wall", "Raise the floor to create a wall . Only work at the lowest floor.", 0);
    private static readonly Spell spellStabilize = new Spell("Stabilize", "Stabilize the bleeding of a soldier.", 0);
    private static readonly Spell spellHeal = new Spell("Heal", "Increase the recover speed of a soldier.", 0);
    private static readonly Spell spellProjectile = new Spell("Projectile", "Create a projectile from the surronding environment and throw it towards the enemy at high speed.", 0);
    private static readonly Spell spellDispell = new Spell("Dispell", "Neutralize an enemy spell.", 0);

    /// ---------- TROOPS
    private static readonly Unit recruit = new Unit("Recruit", "Freshly recruited soldier, have no experience on the field.\nDon't expect too much from them.", 50);
    private static readonly Unit secondClass = new Unit("Second Class", "Soldier that only did a few missions on the field.", 150);
    private static readonly Unit firstClass = new Unit("First Class", "Soldier that already have a lot of experience on the field. Second class soldier and higher can have a specialization.", 200);
    private static readonly Unit carporal = new Unit("Carporal", "Along with being an experienced soldier, have some knowledge in leadership.", 300);
    private static readonly Unit sergeant = new Unit("Sergeant", "Veterant soldier who also have a lot of experience in leadership.", 400);

    /// ---------- EQUIPEMENTS
    private static readonly Weapon stungun = new Weapon("Stungun", "An incapacitating weapon that must be used in close contact.", 10);
    private static readonly Weapon tazer = new Weapon("Stungun", "An incapacitating gun that must be used in short range.", 25);
    private static readonly Weapon assaultGun = new Weapon("Assault Rifle", "A standard rifle with a good rate of fire. Usually the basic weapon given to a soldier.", 50);
    private static readonly Weapon smg = new Weapon("Submachine Gun", "A light automatic gun, less powerful than an assault gun. Is however a good weapon for closer quarter combat since they are easier to manipulate.", 40);
    private static readonly Weapon shotgun = new Weapon("Shotgun", "A powerful weapon that shoot many projectile. Really useful for close quarter combat.", 75);
    private static readonly Weapon hmg = new Weapon("Heavy Machine Gun", "A heavy gun that need to be deployed before being used. Really useful to defend a position.", 150);

    private static readonly Protection baslisticVest = new Protection("Combat Vest", "Protect the torso against light guns and others small projectiles.", 25);
    private static readonly Protection baslisticHelmet = new Protection("Combat Helmet", "Protect the head against light guns and others small projectiles.", 35);
    private static readonly Protection balisticShield = new Protection("Balistic Shield", "A shield that protect against bullets. May only be used with a handgun.", 70);

    private static readonly Utilitary explosiveGrenade = new Utilitary("Explosive Grenade", "A grenade that can do explosive damage in a small zone.", 20);
    private static readonly Utilitary flashGrenade = new Utilitary("Flash Grenade", "A grenade that temporarily blind people in a small zone.", 25);
    private static readonly Utilitary antiMagicGrenade = new Utilitary("Anti Magic Grenade", "A grenade that do an small antic magic area where thrown.", 100);
    private static readonly Utilitary claymore = new Utilitary("Claymore", "A small device that shoot metal balls when someone trip on a wire.", 150);

    private static readonly Utilitary firstAidKit = new Utilitary("First Aid Kit", "A medical kit that can stabilize in critical state.", 20);
    private static readonly Utilitary medicalKit = new Utilitary("Medical Kit", "A full medical kit that can treat even the worse injuries. May only be use by a medecin", 50);
    private static readonly Utilitary reanimationKit = new Utilitary("Reanimation Kit", "A one use reanimation kit, allow to save someone whose heart stop beating. May only be use by a medecin", 75);
    private static readonly Utilitary defuseKit = new Utilitary("Defuse Kit", "Allow to desarm bombs. May only be used by an engineer.", 20);
    private static readonly Utilitary doorCamera = new Utilitary("Under Door Camera", "Small camera you put under a door to observe the inside of a room.", 150);
    private static readonly Utilitary retractableShovel = new Utilitary("Retractable Shovel", "Allow to remove debris or break furniture in small parts.", 20);
    private static readonly Utilitary nightVision = new Utilitary("Night Vision", "Improve vision in the dark.", 100);
    private static readonly Utilitary magicAnalyzer = new Utilitary("Magic Analyzer", "Analyze the magic concentration in the near air.", 200);

    /// ---------- LOGISTICS
    private static readonly Squad basicSquad = new Squad("Intervention Squad", "A basic intervention squad", new AEquipement[] { carporal, secondClass, secondClass, secondClass, assaultGun, assaultGun, assaultGun, assaultGun });
    private static readonly Squad trainedSquad = new Squad("Trained Intervention Squad", "An intervention squad with high level soldiers.", new AEquipement[] { sergeant, firstClass, firstClass, firstClass, assaultGun, assaultGun, assaultGun, assaultGun });
    private static readonly Squad defenseSquad = new Squad("Defense Squad", "A squad formed to defend a position", new AEquipement[] { carporal, secondClass, secondClass, secondClass, assaultGun, assaultGun, assaultGun, hmg, claymore, claymore });
    private static readonly Squad offensiveSquad = new Squad("Offensive Squad", "A squad formed for highly offensive attack", new AEquipement[] { carporal, secondClass, secondClass, secondClass, smg, smg, smg, shotgun, explosiveGrenade, explosiveGrenade, flashGrenade, flashGrenade });
}
