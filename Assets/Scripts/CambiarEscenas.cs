using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenas : MonoBehaviour {

    public void Empezar(string EmpezarNivel)
    {
        SceneManager.LoadScene(EmpezarNivel);
    }
    
     public void CambiarEscena(string escena){
        SceneManager.LoadScene(escena);
    }

    public void SalirJuego(){
        Application.Quit();
    }

}
