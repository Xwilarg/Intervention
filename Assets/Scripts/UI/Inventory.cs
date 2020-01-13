using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<AEquipement> equipements;

    private void Start()
    {
        equipements = new List<AEquipement>();
    }

    public void AddEquipement(AEquipement eq)
        => equipements.Add(eq);
}
