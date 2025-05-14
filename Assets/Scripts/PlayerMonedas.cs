using UnityEngine;

public class PlayerMonedas : MonoBehaviour
{
    public int monedas = 0;

    public bool GastarMonedas(int cantidad)
    {
        if (monedas >= cantidad)
        {
            monedas -= cantidad;
            return true;
        }
        return false;
    }

    public void AgregarMonedas(int cantidad)
    {
        monedas += cantidad;
    }
}
