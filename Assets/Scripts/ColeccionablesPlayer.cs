using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColeccionablesPlayer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    public Image image;

    [HideInInspector]public Items items;
    [HideInInspector] public Transform despuesDeArrastrar;

    public void InicializarItem(Items newItem){
        items = newItem;
        image.sprite = newItem.image;
    }

    // Drag and drop
    public void OnBeginDrag(PointerEventData eventData) {
        image.raycastTarget = false;

        despuesDeArrastrar = transform.parent;
        transform.SetParent(transform.root);

    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        image.raycastTarget = true;
        transform.SetParent(despuesDeArrastrar);

    }

}
