using UnityEngine;
using UnityEngine.UI;

public class UnitEditor : MonoBehaviour
{
    [SerializeField]
    private Text title;

    public void Init(Unit unit)
    {
        title.text = unit.GetName();
    }
}
