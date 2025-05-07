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
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario inventoryManager = other.GetComponentInChildren<Inventario>();

            if (inventoryManager != null)
            {
                bool result = inventoryManager.AgregarItem(itemsToPickup[0]);

                if (result)
                {
                    Debug.Log("ITEM AGREGADO");
                    Destroy(gameObject); 
                }
                else
                {
                    Debug.Log("item NO agregado");
                }
            }
        }
    }
}
