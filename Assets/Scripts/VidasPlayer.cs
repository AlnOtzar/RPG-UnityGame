using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script de Unity | 0 referencias
public class VidasPlayer : MonoBehaviour
{
    public Image vidaPlayer;
    private float anchoVidasPlayer;
    public static int vida;
    private bool haMuerto;
    public GameObject gameOver;
    private const int vidasINI = 5;
    public static int puedePerderVida = 1;

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
        yield return new WaitForSeconds(0.5f);
        gameOver.SetActive(true);
    }

}
