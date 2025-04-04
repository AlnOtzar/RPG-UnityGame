using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidasPlayer : MonoBehaviour
{
    public movPlayer movimientoPlayer;  // Referencia al script movPlayer
    public int defensa = 1;
    public Image vidaPlayer;
    private float anchoVidasPlayer;
    public static int vida;
    private bool haMuerto;
    public GameObject gameOver;
    private int vidasMax = 20;
    public int vidaActual;
    public static int puedePerderVida = 1;
    public GameObject animacionMuerteCanvas; // Arrastrar en el Inspector
    public int experienciaParaSubir = 100;
    public int experienciaActual = 0;
    public int nivel = 1;
    public Slider barraXP;  // Asocia esto con un Slider para la barra de XP
    public TextMeshProUGUI textoNivel;

    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasMax;
        gameOver.SetActive(false);
        ActualizarUI();
    }

    public void TomarDaño(int daño)
    {
        if (vida > 0 && puedePerderVida == 1)
        {
            puedePerderVida = 0;

            int dañoReducido = daño - defensa;
            dañoReducido = Mathf.Max(0, dañoReducido);

            vida -= dañoReducido;
            DibujaVida(vida);
        }
        if (vida <= 0 && !haMuerto)
        {
            haMuerto = true;
            StartCoroutine(EjecutaMuerte());
        }
    }

    private void DibujaVida(int vida)
    {
        RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
        transformaImagen.sizeDelta = new Vector2
        (anchoVidasPlayer * (float)vida / (float)vidasMax, transformaImagen.sizeDelta.y);
    }

    IEnumerator EjecutaMuerte()
    {
        GetComponent<SpriteRenderer>().enabled = false; // Ocultar el personaje en la escena
        GetComponent<Collider2D>().enabled = false; // Desactivar colisiones para evitar errores
        
        movPlayer.estaMuerto = true; // Usar el nombre de la clase para acceder a la variable estática

        animacionMuerteCanvas.SetActive(true); // Activar la animación en el Canvas

        yield return new WaitForSeconds(0); // Esperar a que termine la animación

        gameOver.SetActive(true); // Mostrar la pantalla de Game Over
    }

    public void AumentarDefensa(int cantidad)
    {
        defensa += cantidad;
    }

    public void RecibirXP(int cantidadXP)
    {
        experienciaActual += cantidadXP;

        // Limita la experiencia a no exceder el máximo
        if (experienciaActual > experienciaParaSubir)
        {
            experienciaActual = experienciaParaSubir;
        }

        ActualizarUI();
    }

    void Update()
    {
        // Actualiza la barra de XP
        if (barraXP != null)
        {
            barraXP.value = (float)experienciaActual / experienciaParaSubir; // Actualiza la barra de XP con el valor correspondiente
        }

        // Si la experiencia alcanza el máximo necesario para subir de nivel
        if (experienciaActual >= experienciaParaSubir)
        {
            SubirDeNivel();
        }
    }

    public void SubirDeNivel()
    {
        nivel++;
        vidasMax += 10; // Aumenta la vida máxima al subir de nivel
        vidaActual = vidasMax; // Restaura la vida al máximo

        // Probabilidad de mejorar la defensa en un 30%
        if (Random.value <= 0.3f)
        {
            defensa += 5; // Aumenta la defensa
        }

        // Aumenta la experiencia necesaria para el siguiente nivel
        experienciaActual -= experienciaParaSubir;
        experienciaParaSubir = Mathf.RoundToInt(experienciaParaSubir * 1.2f); // Se aumenta la cantidad de XP necesaria para el siguiente nivel

        ActualizarUI();
    }

    // Actualiza el texto y la UI cuando sube de nivel
    void ActualizarUI()
    {
        // Actualiza el texto del nivel
        if (textoNivel != null)
        {
            textoNivel.text = $"Nivel: {nivel}";
        }

        // Actualiza la barra de XP
        if (barraXP != null)
        {
            barraXP.value = (float)experienciaActual / experienciaParaSubir; // Actualiza la barra de XP
        }
    }
}
