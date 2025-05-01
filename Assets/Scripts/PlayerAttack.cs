using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking;
    private float cooldownAtaque = 1f;
    private float tiempoUltimoAtaque = 0f;

    private Collider2D espadaCollider;
    public Animator anim;
    private movPlayer playerMovement;
    public int dañoJugador = 1;

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


    private IEnumerator ForceEndAttack(float tiempoMaximo)
    {
        yield return new WaitForSeconds(tiempoMaximo);
        if (isAttacking) // Si el evento no se ejecutó, forzar EndAttack
        {
            EndAttack();
        }
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
        dañoJugador = Mathf.Clamp(dañoJugador, 1, 999); // evita que sea 0 o negativo
    }

}

