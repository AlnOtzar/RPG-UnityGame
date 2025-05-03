using UnityEngine;
using UnityEngine.EventSystems;

public class InventarioSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) {
        ColeccionablesPlayer item = eventData.pointerDrag.GetComponent<ColeccionablesPlayer>();
        item.despuesDeArrastrar = transform;
        }
    }

}
