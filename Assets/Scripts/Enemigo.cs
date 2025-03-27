using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Se agregó esta línea para usar NavMeshAgent

// Script de Unity | Creferencias
public class Enemigo : MonoBehaviour
{
    public static int vidaEnemigo = 1;
    private float frecAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer;
    private SpriteRenderer spriteEnemigo;
    private Transform mirarHacia;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
    }

    // Mensaje de Unity | referencias
    void Start()
    {
        vidaEnemigo = 1;
        agente.updateRotation = false; // Corregido el nombre de la variable 'agemte'
        agente.updateUpAxis = false;
    }

    // Mensaje de Unity | U referencias
    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        float distancia = Vector3.Distance(personaje.position, this.transform.position);

        if (this.transform.position == puntosRuta[indiceRuta].position)
        {
            if (indiceRuta < puntosRuta.Length - 1)
            {
                indiceRuta++;
            }
            else if (indiceRuta == puntosRuta.Length - 1)
            {
                indiceRuta = 0;
            }
        }

        if (distancia < distanciaDeteccionPlayer)
        {
            playerEnRango = true;
        }
        else
        {
            playerEnRango = false;
        }

        if (tiempoSigAtaque > 0)
        {
            tiempoSigAtaque = frecAtaque + iniciaConteo - Time.time;
        }
        else
        {
            tiempoSigAtaque = 0;
            VidasPlayer.puedePerderVida = 1;
            SigueAlPlayer(playerEnRango);
            RotaEnemigo(); // Corregido el nombre de la función
        }
    }

    private void SigueAlPlayer(bool playerEnRango)
    {
        if (playerEnRango)
        {
            agente.SetDestination(personaje.position);
            mirarHacia = personaje;
        }
        else
        {
            agente.SetDestination(puntosRuta[indiceRuta].position);
            mirarHacia = puntosRuta[indiceRuta];
        }
    }

    private void RotaEnemigo()
    {
        if (this.transform.position.x > mirarHacia.position.x)
        {
            spriteEnemigo.flipX = true;
            
        }
        else
        {
            spriteEnemigo.flipX = false;
            
        }
    }

    // Mensaje de Unity | C nelerencias
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player")
        {
            tiempoSigAtaque = frecAtaque;
            iniciaConteo = Time.time;
            obj.transform.GetComponentInChildren<VidasPlayer>().TomarDaño(1);
        }
    }

    public void TomarDaño(int daño)
    {
        vidaEnemigo -= daño;
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
    }
}
