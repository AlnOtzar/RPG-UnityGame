using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movPlayer : MonoBehaviour
{
    private Animator anim;
    public GameObject[] personajes; 
    private Vector2 ultimaDireccion = Vector2.down; // Por ejemplo, mirando hacia abajo al inicio

    private int indicePersonaje = 0; 

    private bool puedeMoverse = true;
    private Vector2 dirMov;

    public float velocidadNormal = 3f;
    public float velocidadCorrer = 6f;
    private bool estaCorriendo = false;

    public Rigidbody2D rb;

    public bool estaMuerto = false;

    private PlayerAttack playerAttack;

    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        ActivarPersonajeActual(); 
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

        estaCorriendo = Input.GetKey(KeyCode.LeftShift);
        float velocidadActual = estaCorriendo ? velocidadCorrer : velocidadNormal;

        rb.linearVelocity = dirMov * velocidadActual;
    }

    private void Animacionesplayer()
    {
        if (anim == null || playerAttack.IsAttacking()) return;

        float minUmbral = 0.1f;
        Vector2 mov = dirMov;

        // Detecta si hay movimiento significativo
        if (mov.magnitude >= minUmbral)
        {
            ultimaDireccion = mov;
        }
        else
        {
            mov = Vector2.zero;
        }

        // Parámetros para blend trees de caminar y correr
        anim.SetFloat("movX", mov.x);
        anim.SetFloat("movY", mov.y);

        // Parámetros para el blend tree de Idle (no cambian aunque el jugador se detenga)
        anim.SetFloat("idleX", ultimaDireccion.x);
        anim.SetFloat("idleY", ultimaDireccion.y);

        anim.SetBool("corriendo", estaCorriendo);
        bool estaCaminando = mov != Vector2.zero && !estaCorriendo;
        anim.SetBool("caminando", estaCaminando);
    }





    public void BloquearMovimiento(bool estado)
    {
        puedeMoverse = !estado;
        rb.linearVelocity = Vector2.zero;
    }

    private void ActivarPersonajeActual()
    {
        for (int i = 0; i < personajes.Length; i++)
        {
            personajes[i].SetActive(i == indicePersonaje);
        }

        anim = personajes[indicePersonaje].GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CambiarPersonaje();
        }
    }

    private void CambiarPersonaje()
    {
        personajes[indicePersonaje].SetActive(false);

        indicePersonaje = (indicePersonaje + 1) % personajes.Length;

        personajes[indicePersonaje].SetActive(true);
        anim = personajes[indicePersonaje].GetComponent<Animator>();

        anim.SetFloat("idleX", ultimaDireccion.x);
        anim.SetFloat("idleY", ultimaDireccion.y);
        anim.SetFloat("movX", dirMov.x);
        anim.SetFloat("movY", dirMov.y);
        anim.SetBool("corriendo", estaCorriendo);
    }

}
