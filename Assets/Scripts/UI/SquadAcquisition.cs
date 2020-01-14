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

    private Unit equipement;

    public void Init(string mitemName, Action editCallback, Action removeCallback)
    {
        itemName.text = mitemName;
        editButton.onClick.AddListener(new UnityAction(editCallback));
        removeButton.onClick.AddListener(new UnityAction(removeCallback));
    }
}
