using UnityEngine;

public class PRUEBAagregar : MonoBehaviour
{
    public Inventario inventoryManager;
    public Items[] itemsToPickup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario inventoryManager = other.GetComponentInChildren<Inventario>();
            PlayerMonedas playerMonedas = other.GetComponent<PlayerMonedas>();

            if (itemsToPickup.Length > 0)
            {
                Items item = itemsToPickup[0];

                // Si es moneda, suma también
                if (item.nombre == "Moneda" && playerMonedas != null)
                {
                    playerMonedas.AgregarMonedas(1);
                    Debug.Log("Moneda recogida y sumada al contador");
                }

                // Intenta agregar al inventario (ya sea moneda u otro item)
                if (inventoryManager != null)
                {
                    bool result = inventoryManager.AgregarItem(item);
                    if (result)
                    {
                        Debug.Log("ITEM AGREGADO AL INVENTARIO");
                        Destroy(gameObject);
                    }
                    else
                    {
                        Debug.Log("Item NO agregado (inventario lleno)");
                    }
                }
            }
        }
    }


    // Métodos para pruebas o para llamados desde UI

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AgregarItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("ITEM AGREGADO");
        }
        else
        {
            Debug.Log("Item NO agregado");
        }
    }

    public void GetSelectedItem()
    {
        Items itemRecibido = inventoryManager.GetSelectedItem(false);
        if (itemRecibido != null)
        {
            Debug.Log("Item recibido: " + itemRecibido.nombre);
        }
        else
        {
            Debug.Log("Item NO recibido");
        }
    }

    public void UseSelectedItem()
    {
        Items itemRecibido = inventoryManager.GetSelectedItem(true);
        if (itemRecibido != null)
        {
            Debug.Log("Item usado: " + itemRecibido.nombre);
            itemRecibido.Usar(GameObject.FindWithTag("Player")); // Aplica efecto
        }
        else
        {
            Debug.Log("Item NO usado");
        }
    }
}
