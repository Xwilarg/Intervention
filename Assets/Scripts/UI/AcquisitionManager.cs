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

    private const float addButtonYInit = 70f;
    private const float selectionYOffset = -35f;
    private const float squadYOffset = -75f;
    private const float squadYInit = -120f;

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
        foreach (Unit item in inventory.GetEquipements(AEquipement.Type.Troop)) // Add selection items
        {
            GameObject go = Instantiate(selectionAcquisitionPrefab, selectionAcquisitionParent);
            var rTransform = go.GetComponent<RectTransform>();
            rTransform.anchoredPosition += new Vector2(0f, selectionYOffset * index);
            go.GetComponent<SelectionAcquisition>().Init(item.GetName(), () => // Unit added to team
            {
                GameObject goSquad = Instantiate(squadPrefab, squadParent);
                var rTransformSquad = goSquad.GetComponent<RectTransform>();
                rTransformSquad.anchoredPosition += new Vector2(0f, squadYOffset * squadList.Count);
                if (squadList.Count == 3) // Move "Add Soldier" button
                {
                    Destroy(currentAddButton.gameObject);
                    currentAddButton = null;
                }
                else
                {
                    currentAddButton.anchoredPosition += new Vector2(0f, squadYOffset);
                }
                if (squadList.Count == 0) // If it's the first unit we add, set it as squad leader
                    item.SetSquadLeader();
                goSquad.GetComponent<SquadAcquisition>().Init(item, () => // Edit unit
                {

                }, () => // Remove unit
                {
                    squadList.Remove(goSquad);
                    Destroy(goSquad);
                    int i = 0;
                    foreach (var sqGo in squadList)
                    {
                        if (i == 0) // Make sure first unit is squad leader
                        {
                            var sqGoSa = sqGo.GetComponent<SquadAcquisition>();
                            sqGoSa.GetUnit().SetSquadLeader();
                            sqGoSa.UpdateName();
                        }
                        var sqGoRTransform = sqGo.GetComponent<RectTransform>();
                        sqGoRTransform.anchoredPosition = new Vector2(sqGoRTransform.anchoredPosition.x, squadYInit + (squadYOffset * i));
                        i++;
                    }
                    // Move "Add Soldier" button
                    if (currentAddButton == null) // Button doesn't exist so we re instantiate it
                    {
                        GameObject goI = Instantiate(addButtonPrefab, squadParent);
                        currentAddButton = goI.GetComponent<RectTransform>();
                    }
                    currentAddButton.anchoredPosition = new Vector2(currentAddButton.anchoredPosition.x, addButtonYInit + (squadYOffset * squadList.Count)); // Move button to correct position
                    inventory.UnequipUnit((Unit)item);
                    ClearSelection();
                });
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
