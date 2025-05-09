 using UnityEngine;

public class PRUEBAagregar : MonoBehaviour
{
    public Inventario inventoryManager;
    public Items[] itemsToPickup;
    public Items monedaItem; // este debe ser el ítem tipo moneda
    public int costo = 100;   // cuántas monedas cuesta el item


    private void OnTriggerEnter2D(Collider2D other){
    if (other.CompareTag("Player")){
        Inventario inventoryManager = other.GetComponentInChildren<Inventario>();

        if (inventoryManager != null)
        {
            int monedasDisponibles = inventoryManager.ObtenerCantidadMonedas(monedaItem);
            
            if (monedasDisponibles >= costo){
                bool pago = inventoryManager.GastarMonedas(monedaItem, costo);

                if (pago){
                    bool result = inventoryManager.AgregarItem(itemsToPickup[0]);

                    if (result){
                        Debug.Log("ITEM COMPRADO Y AGREGADO");
                        Destroy(gameObject); 
                    } else {
                        Debug.Log("No se pudo agregar el item al inventario");
                    }
                } else {
                    Debug.Log("No se pudo realizar el pago");
                }
            } else {
                Debug.Log("No tienes suficientes monedas");
            }
        }
    }
}


    public void PickupItem(int id) {    
        bool result = inventoryManager.AgregarItem(itemsToPickup[id]);

        if(result == true){
            Debug.Log("ITEM AGREGADO");
        } else {
            Debug.Log("item NO agregado");
        }
    }

    public void GetSelectedItem(){
        Items itemRecibido = inventoryManager.GetSelectedItem(false); 
        if(itemRecibido != null) {
            Debug.Log("item recibido: " + itemRecibido);
        } else {
            Debug.Log("item NO recibido");
        }
    }

    public void UseSelectedItem(){
        Items itemRecibido = inventoryManager.GetSelectedItem(true); 
        if(itemRecibido != null) {
            Debug.Log("item usado: " + itemRecibido);
        } else {
            Debug.Log("item NO usado");
        }
    }


}
