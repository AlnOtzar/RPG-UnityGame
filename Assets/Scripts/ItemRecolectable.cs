using UnityEngine;

public class ItemRecolectable : MonoBehaviour 
{
    // Campo oculto en el Inspector pero asignable por código
    [HideInInspector] public Items itemSO;

    void Start()
    {
        // Asigna el sprite automáticamente si hay un SO
        if (itemSO != null && itemSO.image != null)
        {
            GetComponent<SpriteRenderer>().sprite = itemSO.image;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && itemSO != null)
        {
            if (Inventario.instance.AgregarItem(itemSO))
            {
                Debug.Log($"¡{itemSO.name} recogido!");
                Destroy(gameObject);
            }
        }
    }
}