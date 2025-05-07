 using UnityEngine;

public class PRUEBAagregar : MonoBehaviour
{
    public Inventario inventoryManager;
    public Items[] itemsToPickup;

    public void PickupItem(int id) {
        
        bool result = inventoryManager.AgregarItem(itemsToPickup[id]);

        if(result == true){
            Debug.Log("ITEM AGREGADO");
        } else {
            Debug.Log("item NO agregado");
        }

    }

}

