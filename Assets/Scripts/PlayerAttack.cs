
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking;
    private float cooldownAtaque = 1f;
    private float tiempoUltimoAtaque = 0f;

    public GameObject flecha;
    public Animator anim;
    private movPlayer playerMovement;
    public int dañoJugador = 1;
    public VidasPlayer energia;

    void Start()
    {
        playerMovement = GetComponent<movPlayer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atacar();
        }
    }

    private void Atacar()
    {
        if (isAttacking || Time.time < tiempoUltimoAtaque + cooldownAtaque) return;

        // 🔍 Buscar el personaje activo y su Animator
        anim = GetAnimatorPersonajeActivo();
        if (anim == null) return;

        isAttacking = true;
        playerMovement.BloquearMovimiento(true);
        anim.SetTrigger("Atacar");
        anim.SetBool("isAttacking", true);
        tiempoUltimoAtaque = Time.time;

    }

    private Animator GetAnimatorPersonajeActivo()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                return child.GetComponent<Animator>();
            }
        }
        return null;
    }


    public void EndAttack()
    {
        isAttacking = false;
        playerMovement.BloquearMovimiento(false); // 🚀 Libera movimiento
        anim.SetBool("isAttacking", false);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public void MejorarDaño(int cantidad)
    {
        dañoJugador += cantidad;
    }

    public void DisparoFlecha()
    {
        anim = GetAnimatorPersonajeActivo();
        if(energia.energiaActual > 0)
        {
            energia.energiaActual -= 1;
            energia.ActualizarUI();
            energia.DibujaEnergia(energia.energiaActual);
            Vector2 direction = new Vector2(anim.GetFloat("movX"), anim.GetFloat("movY")).normalized;

            if (direction == Vector2.zero)
            {
                direction = Vector2.right; 
            }

            GameObject obj = Instantiate(flecha);

            obj.transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * 2f;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            obj.transform.rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * 10f;
            }
        }
        
    }


}