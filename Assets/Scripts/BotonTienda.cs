using UnityEngine;

public class BotonTienda : MonoBehaviour
{
    public Items itemAVender;
    public int precio;
    public Inventario inventario;
    public PlayerMonedas monedasJugador;

    public void Comprar()
    {
        if (monedasJugador.GastarMonedas(precio))
        {
            bool agregado = inventario.AgregarItem(itemAVender);
            inventario.GastarItemPorNombre("Moneda", precio);
            if (!agregado)
            {
                Debug.Log("Inventario lleno, no se pudo agregar el item.");
                monedasJugador.AgregarMonedas(precio); // reembolsa
            }
            else
            {
                Debug.Log("Compra exitosa de " + itemAVender.nombre);
            }
        }
        else
        {
            Debug.Log("No tienes suficientes monedas.");
        }
    }
}
