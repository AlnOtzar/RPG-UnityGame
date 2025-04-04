using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking;
    private float cooldownAtaque = 0.5f;
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
        if (isAttacking || Time.time < tiempoUltimoAtaque + cooldownAtaque) return; // Cooldown activo

        isAttacking = true;
        playerMovement.BloquearMovimiento(true); // 🚀 Bloquea movimiento
        anim.SetTrigger("Atacar");
        anim.SetBool("isAttacking", true);
        tiempoUltimoAtaque = Time.time;

        StartCoroutine(ForceEndAttack(1.0f)); // 🚀 Seguridad extra
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

