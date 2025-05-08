using UnityEngine;

public class GemasMejoras : MonoBehaviour
{
    public VidasPlayer estadisticas;
    public PlayerAttack estadisticasDaño;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (gameObject.tag)
            {
                case "Vida":
                    estadisticas.vidasMax += 10;
                    estadisticas.ActualizarUI();
                    estadisticas.DibujaVida(estadisticas.vidaActual);
                    break;
                case "Mana":
                    estadisticas.energiaMax += 5; // Ajusta según tu lógica
                    estadisticas.ActualizarUI();
                    break;
                case "Defensa":
                    estadisticas.defensa += 1;
                    break;
                case "Ataque":
                    estadisticasDaño.dañoJugador += 1;
                    break;
            }
        }
    }

    
}

