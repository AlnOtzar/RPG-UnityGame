using UnityEngine;

public class ItemRecolectable : MonoBehaviour
{
    public Items item; // Campo para asignar el ScriptableObject

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && item != null)
        {
            if (Inventario.instance.AgregarItem(item))
            {
                Debug.Log($"¡{item.name} recogido!");
                Destroy(gameObject);
            }
        }
    }
}