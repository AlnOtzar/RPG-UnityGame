using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movPlayer : MonoBehaviour
{
    private Animator anim;
    public GameObject[] personajes; 
    private int indicePersonaje = 0; 

    private bool puedeMoverse = true;
    private Vector2 dirMov;

    public float velocidadNormal = 3f;
    public float velocidadCorrer = 6f;
    private bool estaCorriendo = false;

    public Rigidbody2D rb;

    public bool estaMuerto = false;
    private string capaIdle = "idle";
    private string capaCaminar = "Caminar";
    private bool PlayerMoviendose = false;
    private float ultimoMovX, ultimoMovY;

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
        if (anim == null) return;

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
        anim.SetBool("corriendo", estaCorriendo); 
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
    }
}
