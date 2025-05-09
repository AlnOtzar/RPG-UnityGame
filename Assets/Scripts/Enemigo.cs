using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public SpawnerDeEnemigos spawner;
    public GameObject prefabDropEnemigo;
    public GameObject prefabDropMoneda;

    public GameObject monedas;

    public int xpAlDerrotar = 20;
    public int vidaEnemigo = 5;
    public int dañoEnemigo = 0;

    private float frecAtaque = 1.5f, tiempoSigAtaque = 1, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer;
    private SpriteRenderer spriteEnemigo;
    private Animator animator;

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        indiceRuta = Random.Range(0, puntosRuta.Length);
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        agente.avoidancePriority = Random.Range(0, 100); 
        agente.stoppingDistance = 0.5f;

    }

    void Update()
    {
        float distancia = Vector3.Distance(personaje.position, transform.position);
        playerEnRango = distancia < distanciaDeteccionPlayer;

        if (agente.remainingDistance <= 0.1f && !playerEnRango)
        {
            indiceRuta = (indiceRuta + 1) % puntosRuta.Length;
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
            ActualizarAnimaciones();
        }
    }

    private void SigueAlPlayer(bool playerEnRango)
    {
        if (playerEnRango)
        {
            agente.SetDestination(personaje.position);
        }
        else
        {
            agente.SetDestination(puntosRuta[indiceRuta].position);
        }
    }

    private void ActualizarAnimaciones()
    {
        float velocidad = agente.velocity.magnitude;
        animator.SetBool("isMoving", velocidad > 0.1f);

        if (velocidad > 0.1f)
        {
            Vector3 direccionMovimiento = agente.velocity.normalized;
            animator.SetFloat("movX", direccionMovimiento.x);
            animator.SetFloat("movY", direccionMovimiento.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Arma"))
        {
            PlayerAttack jugador = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerAttack>();
            int daño = jugador.dañoJugador;
            TomarDaño(daño);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time >= iniciaConteo + frecAtaque)
        {
            iniciaConteo = Time.time;

            VidasPlayer vida = collision.GetComponentInParent<VidasPlayer>();
            if (vida != null)
            {
                vida.TomarDaño(dañoEnemigo);
            }
        }
    }




    public void TomarDaño(int daño)
    {
        vidaEnemigo -= daño;
        if (vidaEnemigo <= 0)
        {
            VidasPlayer vida = GameObject.FindWithTag("Player").GetComponent<VidasPlayer>();
            if (vida != null)
            {
                vida.RecibirXP(Random.Range(10, xpAlDerrotar + 1));
                
            }
            if (prefabDropEnemigo != null && Random.value <= 0.2f)
            {
                Instantiate(prefabDropEnemigo, transform.position + Vector3.right * 0.3f, Quaternion.identity);
            }
            int cantidadMonedas = Random.Range(1, 3);
            for (int i = 0; i < cantidadMonedas; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
                Instantiate(prefabDropMoneda, transform.position + offset, Quaternion.identity);
            }
            if (spawner != null)
                spawner.EnemigoEliminado();

            Destroy(gameObject);
        }
    }
}