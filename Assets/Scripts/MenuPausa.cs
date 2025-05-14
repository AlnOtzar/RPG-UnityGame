using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private AudioSource musicaFondo; 

    public static bool enPausa = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (enPausa)
                Reanudar();
            else
                Pausa();
        }
    }

    public void Pausa()
    {
        enPausa = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);

        if (musicaFondo != null)
        {
            musicaFondo.Pause(); 
        }
    }

    public void Reanudar()
    {
        enPausa = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);

        if (musicaFondo != null)
        {
            musicaFondo.UnPause(); 
        }
    }

    public void Reiniciar()
    {
        enPausa = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverInicio(string inicio)
    {
        enPausa = false;
        SceneManager.LoadScene(inicio);
        Time.timeScale = 1f;
    }
}
