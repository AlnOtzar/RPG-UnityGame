using UnityEngine;

public class TiendaTrigger : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject menuTienda;

    [Header("Configuración de Tienda")]
    public Items[] itemsEnVenta;  // Ítems que se venden
    public Items monedaItem;      // ScriptableObject de la moneda
    public int[] precios;         // Precios correspondientes

    private bool jugadorEnTienda = false;

    private void Start()
    {
        if (menuTienda != null)
        {
            menuTienda.SetActive(false);
            Debug.Log("Menú de tienda desactivado al inicio");
        }
        else
        {
            Debug.LogError("¡No hay referencia al menú de tienda!");
        }
    }

    private void Update()
    {
        // Abrir tienda con T
        if (jugadorEnTienda && Input.GetKeyDown(KeyCode.T))
        {
            if (menuTienda != null)
            {
                menuTienda.SetActive(true);
                Time.timeScale = 0f;
                Debug.Log("Tienda abierta (Juego pausado)");
            }
        }

        // Cerrar tienda con ESC
        if (menuTienda != null && menuTienda.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarTienda();
        }

        
    if (Input.GetKeyDown(KeyCode.M))
    {
        Inventario.instance.AgregarItem(monedaItem);
        Debug.Log($"Monedas actuales: {Inventario.instance.ObtenerCantidadMonedas(monedaItem)}");
    }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnTienda = true;
            Debug.Log("Jugador en tienda. Presiona T para abrir");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnTienda = false;
            Debug.Log("Jugador salió de la tienda");
            CerrarTienda();
        }
    }

    public void CerrarTienda()
    {
        if (menuTienda != null)
        {
            menuTienda.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("Tienda cerrada (Juego reanudado)");
        }
    }

    // Llamado desde botones UI
    public void ComprarItem(int idItem)
    {
        Debug.Log($"Intentando comprar ítem con ID: {idItem}");

        // Validaciones
        if (Inventario.instance == null)
        {
            Debug.LogError("ERROR: No hay instancia del Inventario");
            return;
        }

        if (monedaItem == null)
        {
            Debug.LogError("ERROR: No hay ScriptableObject de moneda asignado");
            return;
        }

        if (itemsEnVenta == null || precios == null || itemsEnVenta.Length == 0 || precios.Length == 0)
        {
            Debug.LogError("ERROR: Arrays de ítems o precios no configurados");
            return;
        }

        if (idItem < 0 || idItem >= itemsEnVenta.Length)
        {
            Debug.LogError($"ERROR: ID {idItem} inválido. Rango permitido: 0 a {itemsEnVenta.Length - 1}");
            return;
        }

        Items item = itemsEnVenta[idItem];
        int precio = precios[idItem];

        if (item == null)
        {
            Debug.LogError($"ERROR: El ítem con ID {idItem} no está asignado");
            return;
        }

        Debug.Log($"Verificando monedas para comprar: {item.name} (Precio: {precio})");

        // Lógica de compra
        if (Inventario.instance.ObtenerCantidadMonedas(monedaItem) >= precio)
        {
            bool pagoExitoso = Inventario.instance.GastarMonedas(monedaItem, precio);
            
            if (pagoExitoso)
            {
                bool agregado = Inventario.instance.AgregarItem(item);
                
                if (agregado)
                {
                    Debug.Log($"¡Éxito! {item.name} comprado por {precio} monedas");
                    // Opcional: Añadir efectos aquí (sonido/partículas)
                }
                else
                {
                    Debug.Log("¡Inventario lleno! No se pudo agregar el ítem");
                }
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