using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField]
    private Text title, description, price;

    [SerializeField]
    private Button buyButton;

    public void Init(Equipement eq)
    {
        title.text = eq.name;
        description.text = eq.description;
        price.text = "Price: " + eq.price;
    }
}
