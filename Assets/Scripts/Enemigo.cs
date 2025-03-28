using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public SpawnerDeEnemigos spawner;

    public static int vidaEnemigo = 1;
    private float frecAtaque = 2.5f, tiempoSigAtaque = 0, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer;
    private SpriteRenderer spriteEnemigo;
    private Animator animator;  // ✅ Agregado para manejar las animaciones
    private Vector3 ultimaPosicion; 

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();  
    }

    void Start()
    {
        vidaEnemigo = 1;
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        ultimaPosicion = transform.position; 
    }

    void Update()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        float distancia = Vector3.Distance(personaje.position, this.transform.position);
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
            ActualizarAnimaciones();  // ✅ Actualiza el Animator
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
        Vector3 direccionMovimiento = transform.position - ultimaPosicion;
        ultimaPosicion = transform.position;

        // Detectar si el enemigo se está moviendo
        animator.SetBool("isMoving", direccionMovimiento.magnitude > 0.1f);

        // Actualizar valores de movX y movY en el Animator
        if (direccionMovimiento.magnitude > 0.1f)
        {
            animator.SetFloat("movX", direccionMovimiento.x);
            animator.SetFloat("movY", direccionMovimiento.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
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
            spawner.EnemigoEliminado(); // Avisar al spawner
            Destroy(gameObject);
        }
    }

}

