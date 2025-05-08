using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventarioSlot : MonoBehaviour, IDropHandler
{
    public Image imagen;
    public Color selectedColor, notSelectedColor;

    public void Awake(){
        Deselect();
    }

    public void Select(){
        imagen.color = selectedColor;     
    }
    
    public void Deselect(){
         imagen.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) {
        ColeccionablesPlayer item = eventData.pointerDrag.GetComponent<ColeccionablesPlayer>();
        item.despuesDeArrastrar = transform;
        }
    }

}
