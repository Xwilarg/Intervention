using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionAcquisition : MonoBehaviour
{
    [SerializeField]
    private Text itemName;

    [SerializeField]
    private Button callback;

    public void Init(string mitemName, Action mcallback)
    {
        itemName.text = mitemName;
        callback.onClick.AddListener(new UnityAction(mcallback));
    }
}
