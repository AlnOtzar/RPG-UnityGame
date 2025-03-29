using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CicloDiaNoche : MonoBehaviour
{
    public Light2D luzGlobal; // La luz global en la escena
    public float duracionDia = 10f; // Duración del ciclo (10 segundos para pruebas)
    
    private float tiempo;
    
    void Update()
    {
        tiempo += Time.deltaTime;
        float progreso = Mathf.PingPong(tiempo / duracionDia, 1); // Oscila entre 0 y 1

        // Cambia la intensidad de la luz
        luzGlobal.intensity = Mathf.Lerp(1.3f, 0.3f, progreso); // De noche (0.3) a día (1)

        // Cambia el color de la luz (de azul oscuro a amarillo)
        luzGlobal.color = Color.Lerp(new Color(1f, 1f, 1f), new Color(.03f, 0.06f, 0.3f), progreso);
    }
}
