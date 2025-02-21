using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script de Unity | Creferencias
public class Enemigo : MonoBehaviour
{
    public static int vidaEnemigo = 1;
    private float frecAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;


    // Mensaje de Unity | referencias
    void Start()
    {
        vidaEnemigo = 1;
    }

    // Mensaje de Unity | U referencias
    void Update(){
        if (tiempoSigAtaque > 0) {
            tiempoSigAtaque = frecAtaque + iniciaConteo - Time.time;
            }else{
                tiempoSigAtaque = 0;
                VidasPlayer.puedePerderVida = 1;
            }

    }

    // Mensaje de Unity | C nelerencias
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player"){
            tiempoSigAtaque = frecAtaque;
            iniciaConteo = Time.time;
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDaño(1);
        }
    }

    public void TomarDaño(int daño){
        vidaEnemigo -= daño;
        if(vidaEnemigo <= 0 ){
            Destroy(gameObject);
        } 
    }
}