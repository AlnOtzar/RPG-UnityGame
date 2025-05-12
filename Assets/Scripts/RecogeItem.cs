using UnityEngine;

public class RecogeItem : MonoBehaviour
{

    public GameObject itemCaido;


    [Header("Configuración de ITEM")]
    public Items[] itemsTomar; //items que se pueden agarrar
    public Items monedaItem; //modena scriptable object
    public int[] precios; //precios pero todos valen cero
    private bool jugadorEnColider = false; //el jugador en el colider
    


    void Start()
    {
        if (itemCaido != null)
        {
            itemCaido.SetActive(false);
            Debug.Log("item no es nulo");
        }
        else
        {
            Debug.LogError("¡No hay referencia al item!");
        }
        
    }

// jugador en colider del item
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnColider = true;
            Debug.Log("Jugador en colider ITEM");
            RecogerItemDelSuelo(0);
        }
    }

// jugador sale del colider del item
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnColider = false;
            Debug.Log("Jugador salió del colider ITEM");
        }
    }
    
    public void RecogerItemDelSuelo(int idItem)
    {
        Debug.Log($"Intentando RECOGER: {idItem}");

        // Validaciones
        if (Inventario.instance == null)
        {
            Debug.LogError("ERROR: No hay instancia del Inventario");
            return;
        }

        // verifica que haya moneda
        if (monedaItem == null)
        {
            Debug.LogError("ERROR: No hay ScriptableObject de moneda asignado");
            return;
        }

        //verifica que los items tengan un precio, en este casi debe ser cero
        if (itemsTomar == null || precios == null || itemsTomar.Length == 0 || precios.Length == 0)
        {
            Debug.LogError("ERROR: Arrays de ítems o precios no configurados");
            return;
        }

        // verifica que los ID esten en el rango correcto
        if (idItem < 0 || idItem >= itemsTomar.Length)
        {
            Debug.LogError($"ERROR: ID {idItem} inválido. Rango permitido: 0 a {itemsTomar.Length - 1}");
            return;
        }


        // le damos un valor a las variables
        Items item = itemsTomar[idItem];
        int precio = precios[idItem];

        //si no hay ID asignado al item
        if (item == null)
        {
            Debug.LogError($"ERROR: El ítem con ID {idItem} no está asignado");
            return;
        }

        // RecogeItem item para comprobar
        Debug.Log($"Verificando item tomado {item.name} (Precio: {precio})");

        // LOGICA RECOGER
        if (Inventario.instance.ObtenerCantidadMonedas(monedaItem) >= precio)
        {
            bool itemExitosoRecogido = Inventario.instance.GastarMonedas(monedaItem, precio);
            
            if (itemExitosoRecogido)
            {
                bool agregado = Inventario.instance.AgregarItemDelSuelo(item);
                
                if (agregado)
                {
                    Debug.Log($"EXITO! {item.name} comprado por {precio} monedas");
                    // Opcional: Añadir efectos aquí (sonido/partículas)
                }
                else
                {
                    Debug.Log("¡Inventario lleno! No se pudo agregar el ítem");
                }
                Destroy(itemCaido.gameObject);

            }
            else
            {
                Debug.LogError("Fallo al descontar monedas");
            }
        }
        else
        {
            Debug.Log($"No tienes suficientes monedas. Necesitas {precio}");
        }

    }

}
