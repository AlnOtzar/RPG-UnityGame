using UnityEngine;

public class TiendaTrigger : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject menuTienda;

    [Header("Configuración de Tienda")]
    public Items[] itemsEnVenta;
    public Items monedaItem;
    public int[] precios;

    private bool jugadorEnTienda = false;

    private void Start()
    {
        if (menuTienda != null)
            menuTienda.SetActive(false);
    }

    private void Update()
    {
        if (jugadorEnTienda && Input.GetKeyDown(KeyCode.T))
        {
            if (menuTienda != null) // ¡Verificación crítica!
            {
                menuTienda.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if (menuTienda != null && menuTienda.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarTienda();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnTienda = true;
            Debug.Log("Presiona T para abrir la tienda");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnTienda = false;
            CerrarTienda();
        }
    }

    public void CerrarTienda()
    {
        if (menuTienda != null) // ¡Verificación crítica!
        {
            menuTienda.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    // Método para comprar items (desde botones UI)
   public void ComprarItem(int idItem)
{
    // Verifica Singleton del Inventario
    if (Inventario.instance == null)
    {
        Debug.LogError("Inventario no encontrado");
        return;
    }

    // Verifica configuraciones básicas
    if (monedaItem == null)
    {
        Debug.LogError("MonedaItem no asignado");
        return;
    }

    if (itemsEnVenta == null || precios == null || itemsEnVenta.Length == 0 || precios.Length == 0)
    {
        Debug.LogError("Items o precios no configurados");
        return;
    }

    // Verifica ID válido
    if (idItem < 0 || idItem >= itemsEnVenta.Length)
    {
        Debug.LogError("ID de item inválido");
        return;
    }

    // Lógica de compra
    int precioItem = precios[idItem];
    Items item = itemsEnVenta[idItem];

    if (item == null)
    {
        Debug.LogError("Item no asignado");
        return;
    }

    // Usa Inventario.instance directamente (Singleton)
    if (Inventario.instance.ObtenerCantidadMonedas(monedaItem) >= precioItem)
    {
        bool pagoExitoso = Inventario.instance.GastarMonedas(monedaItem, precioItem);
        
        if (pagoExitoso && Inventario.instance.AgregarItem(item))
        {
            Debug.Log($"¡{item.name} comprado!");
            // Opcional: Sonido/efecto visual
        }
        else
        {
            Debug.Log("Inventario lleno o error al agregar");
        }
    }
    else
    {
        Debug.Log("No tienes suficientes monedas");
    }
}
}