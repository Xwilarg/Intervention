using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static EquipementList;

[RequireComponent(typeof(Inventory))]
public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Text creditsDisplay;

    [SerializeField]
    private RectTransform transformTroops, transformEquipements, transformLogistics, transformSquads;
    private int troopIndex, equipementIndex, logisticIndex;

    [SerializeField]
    [Tooltip("Prefab for the element you can buy in the shop")]
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

    private void BuyUnit(ShopElement elem)
    {
        var unit = (Unit)elem.GetEquipement();
        // TODO: Set specialization
        inventory.AddEquipement(unit);
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
            rTransform.anchoredPosition += new Vector2(0f, yOffset * index);
            ShopElement se = go.GetComponent<ShopElement>();
            se.Init(eq, Buy);
            shopElems.Add(se);
        }
        int i = 0;
        foreach (var sq in availableSquads)
        {
            GameObject go = Instantiate(shopElementPrefab, transformSquads);
            var rTransform = go.GetComponent<RectTransform>();
            rTransform.anchoredPosition += new Vector2(0f, yOffset * i);
            ShopElement se = go.GetComponent<ShopElement>();
            se.Init(sq, BuySquad);
            shopElems.Add(se);
            i++;
        }
    }

    private readonly AEquipement[] available = new AEquipement[]
    {
        speNone,
        recruit, secondClass, firstClass, corporal, sergeant,
        assaultGun
    };

    private readonly Squad[] availableSquads = new[] { basicSquad, trainedSquad };
}
