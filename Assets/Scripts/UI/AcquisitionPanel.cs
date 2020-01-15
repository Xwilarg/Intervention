using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AcquisitionPanel : MonoBehaviour
{
    [SerializeField]
    private Text objectName;

    [SerializeField]
    private Button editButton, removeButton;

    public void Init(string mobjectName, Action editCallback, Action removeCallback)
    {
        objectName.text = mobjectName;
        editButton.onClick.AddListener(new UnityAction(editCallback));
        removeButton.onClick.AddListener(new UnityAction(removeCallback));
    }

    public void SetRemoveButtonEnable(bool status)
        => removeButton.interactable = status;
}
