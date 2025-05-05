using UnityEngine;

public class finAtaque : MonoBehaviour
{
    public PlayerAttack ataqueJugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void FinAtaque()
    {
        if (ataqueJugador !=null)
        {
            ataqueJugador.EndAttack();
        }
        else{
            Debug.LogWarning("Player no asignado");
        }
    }

    public void generarFlecha()
    {
        if (ataqueJugador !=null)
        {
            ataqueJugador.DisparoFlecha();
        }
        else{
            Debug.LogWarning("Player no asignado");
        }
    }
}
