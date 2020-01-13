using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Equipement> equipements;

    private void Start()
    {
        equipements = new List<Equipement>();
    }

    public void AddEquipement(Equipement eq)
        => equipements.Add(eq);
}
