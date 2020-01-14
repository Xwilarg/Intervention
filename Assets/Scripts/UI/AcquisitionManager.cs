using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class AcquisitionManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab for selectable element in the Acquisition menu")]
    private GameObject selectionAcquisitionPrefab;

    [SerializeField]
    [Tooltip("Parent item for selectionAcquisitionPrefab")]
    private Transform selectionAcquisitionParent;

    [SerializeField]
    [Tooltip("Prefab for squad")]
    private GameObject squadPrefab;

    [SerializeField]
    [Tooltip("Parent item for squad")]
    private Transform squadParent;

    [SerializeField]
    private GameObject addButtonPrefab;
    
    [SerializeField]
    private RectTransform currentAddButton;

    private const float selectionYOffset = -35f;
    private const float squadYOffset = -75f;

    private Inventory inventory;
    private List<GameObject> selectionList;
    private List<GameObject> squadList;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        selectionList = new List<GameObject>();
        squadList = new List<GameObject>();
    }

    public void AddSoldier()
    {
        ClearSelection();
        int index = 0;
        foreach (var item in inventory.GetEquipements(AEquipement.Type.Troop))
        {
            GameObject go = Instantiate(selectionAcquisitionPrefab, selectionAcquisitionParent);
            var rTransform = go.GetComponent<RectTransform>();
            rTransform.anchoredPosition += new Vector2(0f, selectionYOffset * index);
            go.GetComponent<SelectionAcquisition>().Init(item.GetName(), () =>
            {
                GameObject goSquad = Instantiate(squadPrefab, squadParent);
                var rTransformSquad = goSquad.GetComponent<RectTransform>();
                rTransformSquad.anchoredPosition += new Vector2(0f, squadYOffset * squadList.Count);
                if (squadList.Count == 3)
                {
                    Destroy(currentAddButton.gameObject);
                    currentAddButton = null;
                }
                else
                {
                    currentAddButton.anchoredPosition += new Vector2(0f, squadYOffset);
                }
                squadList.Add(goSquad);
                inventory.EquipUnit((Unit)item);
                ClearSelection();
            });
            selectionList.Add(go);
            index++;
        }
    }

    private void ClearSelection()
    {
        foreach (GameObject elem in selectionList)
            Destroy(elem);
        selectionList.Clear();
    }
}
