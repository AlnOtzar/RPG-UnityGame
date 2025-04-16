using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movPlayer : MonoBehaviour
{
    private bool puedeMoverse = true;
    private Vector2 dirMov;
    public float velMov;
    public Rigidbody2D rb;
    public Animator anim;

    public bool estaMuerto = false;
    private string capaIdle = "idle";
    private string capaCaminar = "Caminar";
    private bool PlayerMoviendose = false;
    private float ultimoMovX, ultimoMovY;

    private PlayerAttack playerAttack; 

    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>(); 
    }

    void FixedUpdate()
    {
        if (puedeMoverse)
        {
            Movimiento();
            Animacionesplayer();
        }
    }

    private void Movimiento()
    {
        if (estaMuerto || playerAttack.IsAttacking()) 
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        dirMov = new Vector2(movX, movY).normalized;
        rb.linearVelocity = new Vector2(dirMov.x * velMov, dirMov.y * velMov);

        if (movX == 0 && movY == 0)
        {
            PlayerMoviendose = false;
        }
        else
        {
            PlayerMoviendose = true;
            ultimoMovX = movX;
            ultimoMovY = movY;
        }

        ActualizaCapa();
    }

    private void ActualizaCapa()
    {
        if (PlayerMoviendose)
        {
            activaCapa(capaCaminar);
        }
        else
        {
            activaCapa(capaIdle);
        }
    }

    private void activaCapa(string nombre)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(anim.GetLayerIndex(nombre), 1);
    }

    private void Animacionesplayer()
    {
        if (playerAttack.IsAttacking()) return;

        anim.SetFloat("movX", ultimoMovX);
        anim.SetFloat("movY", ultimoMovY);
    }

    public void BloquearMovimiento(bool estado)
    {
        puedeMoverse = !estado;
        rb.linearVelocity = Vector2.zero;
    }

    public Vector2 GetUltimaDireccion()
    {
        return new Vector2(ultimoMovX, ultimoMovY);
    }
}
