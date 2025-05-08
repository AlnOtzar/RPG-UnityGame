using UnityEngine;

public class Flecha : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("LateDestroy", 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemigos"))
        {
        Invoke("LateDestroy", 3);
        transform.parent = collision.transform;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false; 
        }
    }

    private void LateDestroy()
    {
        Destroy(gameObject);
    }
}
