using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movPlayer : MonoBehaviour
{
    private Vector2 dirMov;
    public float velMov;
    public Rigidbody2D rb;
    public Animator anim;
    public static bool estaMuerto = false;  // Variable global para saber si el jugador está muerto


    private string capaIdle = "Idle";
    private string capaCaminar = "Caminar";
    private bool PlayerMoviendose = false;
    private float ultimoMovX, ultimoMovY;
     
    
    void FixedUpdate(){
        Movimiento();
        Animacionesplayer();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Movimiento()
    {
        if (estaMuerto){
            rb.linearVelocity = Vector2.zero;
            return;

        } // Si está muerto, no mover al personaje

        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        dirMov = new Vector2(movX, movY).normalized;
        rb.linearVelocity = new Vector2(dirMov.x * velMov, dirMov.y * velMov); // Corregido "linearVelocity" a "velocity" para Rigidbody2D

        if (movX == 0 && movY == 0) { // Idle
            PlayerMoviendose = false;
        } else { // Caminar
            PlayerMoviendose = true;
            ultimoMovX = movX;
            ultimoMovY = movY;
        }
        ActualizaCapa();
    }


    // Update is called once per frame
    private void Animacionesplayer()
    {
        anim.SetFloat("movX", ultimoMovX);
        anim.SetFloat("movY", ultimoMovY);
    }

    private void ActualizaCapa()
    {
        if(PlayerMoviendose){
            activaCapa(capaCaminar);
        }else{
            activaCapa(capaIdle);
        }
    }

    private void activaCapa(string nombre)
    {
        for(int i=0; i < anim.layerCount; i++){
            anim.SetLayerWeight(i,0); //ambos con weight peso cero

        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);

    }
    

}
