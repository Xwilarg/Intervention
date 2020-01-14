using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SquadAcquisition : MonoBehaviour
{
    [SerializeField]
    private Text itemName;

    [SerializeField]
    private Button editButton, removeButton;

    private Unit unit;

    public Unit GetUnit()
        => unit;

    public void Init(Unit munit, Action editCallback, Action removeCallback)
    {
        itemName.text = munit.GetName();
        editButton.onClick.AddListener(new UnityAction(editCallback));
        removeButton.onClick.AddListener(new UnityAction(removeCallback));
        unit = munit;
    }

    public void UpdateName()
        => itemName.text = unit.GetName();
}
