using UnityEngine;
using UnityEngine.UI;

public class UnitEditor : MonoBehaviour
{
    [SerializeField]
    private Text title;

    [SerializeField]
    private AcquisitionPanel weaponPanel;

    public void Init(Unit unit)
    {
        title.text = unit.GetName();
        var weapon = unit.GetWeapon();
        weaponPanel.Init(weapon == null ? "None" : weapon.GetName(), () =>
        {
        }, () =>
        {
        });
    }
}
