using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movPlayer : MonoBehaviour
{
    private Vector2 dirMov;
    public float velMov;
    public Rigidbody2D rb;
    public Animator anim;
    
    void FixedUpdate(){
        Debug.Log("Hola mundo");
        Movimiento();
        Animacionesplayer();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Movimiento()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        dirMov = new Vector2(movX, movY).normalized;
        rb.linearVelocity = new Vector2(dirMov.x * velMov, dirMov.y * velMov);
    }

    // Update is called once per frame
    private void Animacionesplayer()
    {
        anim.SetFloat("movX", dirMov.x);
        anim.SetFloat("movY", dirMov.y);
    }
}
