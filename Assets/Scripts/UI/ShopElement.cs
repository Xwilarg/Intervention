using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField]
    private Text title, description, price;

    [SerializeField]
    private Button buyButton;

    private int priceInt;
    private AEquipement currEquipement;

    public void Init(AEquipement eq, Action<ShopElement> buyCallback)
    {
        title.text = eq.GetName();
        description.text = eq.GetDescription();
        priceInt = eq.GetPrice();
        price.text = "Price: " + priceInt;
        buyButton.onClick.AddListener(new UnityAction(() => buyCallback(this)));
        currEquipement = eq;
    }

    /// <summary>
    /// Make sure we have enough money to buy this item.
    /// If we don't, disable the Buy button.
    /// </summary>
    public void CheckCanBuy(int moneyRemaining)
    {
        if (moneyRemaining < priceInt)
            buyButton.interactable = false;
    }

    public int GetPrice()
        => priceInt;

    public AEquipement GetEquipement()
        => currEquipement;
}
