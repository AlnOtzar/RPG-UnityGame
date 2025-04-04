using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public SpawnerDeEnemigos spawner;

    public int xpAlDerrotar = 20;
    public int vidaEnemigo = 5;
    private VidasPlayer jugador;


    private float frecAtaque = 1.5f, tiempoSigAtaque = 0, iniciaConteo;

    public Transform personaje;
    private NavMeshAgent agente;
    public Transform[] puntosRuta;
    private int indiceRuta = 0;
    private bool playerEnRango = false;
    [SerializeField] private float distanciaDeteccionPlayer;
    private SpriteRenderer spriteEnemigo;
    private Animator animator;
    private Vector3 ultimaPosicion; 

    private void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
        spriteEnemigo = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();  
    }

    void Start()
    {
        jugador = GameObject.FindWithTag("Player").GetComponent<VidasPlayer>(); // Busca al jugador
        indiceRuta = Random.Range(0, puntosRuta.Length);
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        ultimaPosicion = transform.position; 
    }

    void Update()
    {
        float distancia = Vector3.Distance(personaje.position, this.transform.position);
        playerEnRango = distancia < distanciaDeteccionPlayer;

        // Elimina la línea que ajusta la posición Z
        // this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

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
        // Usar agente.velocity para detectar el movimiento
        float velocidad = agente.velocity.magnitude;
        animator.SetBool("isMoving", velocidad > 0.1f);

        if (velocidad > 0.1f)
        {
            Vector3 direccionMovimiento = agente.velocity.normalized; // Obtén la dirección normalizada
            animator.SetFloat("movX", direccionMovimiento.x);
            animator.SetFloat("movY", direccionMovimiento.y);
        }
    }

    // Método para detectar las colisiones con el jugador y con la espada
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // El jugador está tocando al enemigo, hacerle daño
            tiempoSigAtaque = frecAtaque;
            iniciaConteo = Time.time;
            col.transform.GetComponentInChildren<VidasPlayer>().TomarDaño(1);
        }
        else if (col.CompareTag("Arma"))
        {
            // Buscar el daño directamente desde el player
            PlayerAttack jugador = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerAttack>();
            int daño = jugador.dañoJugador;
            TomarDaño(daño);
        }
    }

    // Método para aplicar el daño al enemigo
    public void TomarDaño(int daño)
    {
        vidaEnemigo -= daño;
        if (vidaEnemigo <= 0)
        {
            jugador.RecibirXP(Random.Range(10, xpAlDerrotar + 1)); // XP aleatorio entre 10 y xpAlDerrotar
            spawner.EnemigoEliminado(); // Avisar al spawner que el enemigo fue eliminado
            Destroy(gameObject);  // Eliminar el objeto enemigo
        }
    }
}
