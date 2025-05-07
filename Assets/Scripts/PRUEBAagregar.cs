 using UnityEngine;

public class PRUEBAagregar : MonoBehaviour
{
    public Inventario inventoryManager;
    public Items[] itemsToPickup;

    public void PickupItem(int id) {
            Debug.Log("Intentando recoger item con ID: " + id);

        inventoryManager.AgregarItem(itemsToPickup[id]);
    }


}

