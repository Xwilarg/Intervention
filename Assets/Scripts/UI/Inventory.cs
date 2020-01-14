using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<AEquipement> equipements;
    private List<Unit> squad;

    private void Start()
    {
        equipements = new List<AEquipement>();
        squad = new List<Unit>();
    }

    public void AddEquipement(AEquipement eq)
        => equipements.Add(eq);

    public IEnumerable<AEquipement> GetEquipements(AEquipement.Type eqType)
        => equipements.Where(x => x.GetEquipementType() == eqType);

    public void EquipUnit(Unit unit)
    {
        squad.Add(unit);
        equipements.Remove(unit);
    }
}
