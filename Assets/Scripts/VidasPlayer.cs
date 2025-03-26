using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script de Unity | 0 referencias
public class VidasPlayer : MonoBehaviour
{
    public movPlayer movimientoPlayer;  // Referencia al script movPlayer

    public Image vidaPlayer;
    private float anchoVidasPlayer;
    public static int vida;
    private bool haMuerto;
    public GameObject gameOver;
    private const int vidasINI = 1;
    public static int puedePerderVida = 1;
    public GameObject animacionMuerteCanvas; // Arrastrar en el Inspector


    // Mensaje de Unity | 0 referencias
    void Start()
    {
        anchoVidasPlayer = vidaPlayer.GetComponent<RectTransform>().sizeDelta.x;
        haMuerto = false;
        vida = vidasINI;
        gameOver.SetActive(false);
    }

    public void TomarDaño(int daño) {
        if (vida > 0 && puedePerderVida == 1) {
            puedePerderVida = 0;
            vida -= daño;
            DibujaVida(vida);
    } if (vida <= 0 && !haMuerto) {
            haMuerto = true;
            StartCoroutine(EjecutaMuerte());
        }
    }

    private void DibujaVida(int vida) {
        RectTransform transformaImagen = vidaPlayer.GetComponent<RectTransform>();
        transformaImagen.sizeDelta = new Vector2
        (anchoVidasPlayer * (float)vida / (float)vidasINI, transformaImagen.sizeDelta.y);
    }

    IEnumerator EjecutaMuerte() {
        GetComponent<SpriteRenderer>().enabled = false; // Ocultar el personaje en la escena
        GetComponent<Collider2D>().enabled = false; // Desactivar colisiones para evitar errores
        
        movPlayer.estaMuerto = true; // Usar el nombre de la clase para acceder a la variable estática

        animacionMuerteCanvas.SetActive(true); // Activar la animación en el Canvas

        yield return new WaitForSeconds(0); // Esperar a que termine la animación

        gameOver.SetActive(true); // Mostrar la pantalla de Game Over
    }



}
