using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidasPlayer : MonoBehaviour
{
    public movPlayer movimientoPlayer;
    public int defensa = 1;
    [Header("energia")]
    public Image enegiaPlayer;
    private float anchoEnergiaPlayer;
    public int energiaActual;
    public int energiaMax = 20;
    public TextMeshProUGUI textoEnergia;

    [Header("Vida")]
    public Image vidaPlayer;
    private float anchoVidasPlayer;
    public int vidasMax = 20;
    public int vidaActual;
    public TextMeshProUGUI textoVida;

    [Header("Muerte y Game Over")]
    private bool haMuerto;
    public GameObject gameOver;
    public GameObject animacionMuerteCanvas;
    public static int puedePerderVida = 1;

    [Header("Experiencia y Nivel")]
    public int experienciaParaSubir = 100;
    public int experienciaActual = 0;
    public int nivel = 1;
    public Slider barraXP;
    public TextMeshProUGUI textoNivel;

    [Header("Música Muerte")]
    public AudioSource musicaMuerte;

    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        anchoEnergiaPlayer = enegiaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vidaActual = vidasMax;
        energiaActual = energiaMax;

        gameOver.SetActive(false);
        ActualizarUI();
        DibujaVida(vidaActual);
        DibujaEnergia(energiaActual);
    }

    public void TomarDaño(int daño)
    {
        if (vidaActual > 0 && puedePerderVida == 1)
        {
            puedePerderVida = 0;

            int dañoReducido = daño - defensa;
            dañoReducido = Mathf.Max(0, dañoReducido);

            vidaActual -= dañoReducido;
            vidaActual = Mathf.Clamp(vidaActual, 0, vidasMax);

            DibujaVida(vidaActual);
            ActualizarUI();
        }

        if (vidaActual <= 0 && !haMuerto)
        {
            haMuerto = true;
            StartCoroutine(EjecutaMuerte());
        }
    }

    public void DibujaVida(int vida)
    {
        if (vidaPlayer == null) return;

        RectTransform rt = vidaPlayer.GetComponent<RectTransform>();
        float porcentajeVida = (float)Mathf.Min(vida, vidasMax) / vidasMax;

        rt.sizeDelta = new Vector2(anchoVidasPlayer * porcentajeVida, rt.sizeDelta.y);

        if (porcentajeVida <= 0.3f)
            vidaPlayer.color = Color.red;
        else if (porcentajeVida <= 0.6f)
            vidaPlayer.color = Color.yellow;
        else
            vidaPlayer.color = new Color(.14f, 0.72f, 0.071f);
    }

    public void DibujaEnergia(int energia)
    {
        if (enegiaPlayer == null) return;

        RectTransform rt = enegiaPlayer.GetComponent<RectTransform>();
        float porcentajeEnergia = (float)Mathf.Min(energia, energiaMax) / energiaMax;

        rt.sizeDelta = new Vector2(anchoEnergiaPlayer * porcentajeEnergia, rt.sizeDelta.y);
    }

    IEnumerator EjecutaMuerte()
    {
        if (musicaMuerte != null)
            musicaMuerte.Play();

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        movimientoPlayer.estaMuerto = true;
        animacionMuerteCanvas.SetActive(true);

        yield return new WaitForSeconds(0);

        gameOver.SetActive(true);
    }

    public void AumentarDefensa(int cantidad)
    {
        defensa += cantidad;
    }

    public void RecibirXP(int cantidadXP)
    {
        experienciaActual += cantidadXP;
        experienciaActual = Mathf.Min(experienciaActual, experienciaParaSubir);

        ActualizarUI();
    }

    void Update()
    {
        if (barraXP != null)
            barraXP.value = (float)experienciaActual / experienciaParaSubir;

        if (experienciaActual >= experienciaParaSubir)
            SubirDeNivel();
    }

    public void SubirDeNivel()
    {
        nivel++;
        vidasMax += 10;
        energiaMax += 10;
        vidaActual = vidasMax;
        energiaActual = energiaMax;

        if (Random.value <= 0.3f)
            defensa += 1;

        experienciaActual -= experienciaParaSubir;
        experienciaParaSubir = Mathf.RoundToInt(experienciaParaSubir * 1.3f);

        ActualizarUI();
        DibujaVida(vidaActual);
    }

    public void ActualizarUI()
    {
        if (textoNivel != null)
            textoNivel.text = $"Nivel: {nivel}";

        if (barraXP != null)
            barraXP.value = (float)experienciaActual / experienciaParaSubir;

        if (textoVida != null)
            textoVida.text = $"{vidaActual} / {vidasMax}";
            
        if (textoEnergia != null)
            textoEnergia.text = $"{energiaActual} / {energiaMax}";
    }
}
