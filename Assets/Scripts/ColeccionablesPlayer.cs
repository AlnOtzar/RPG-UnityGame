using UnityEngine;

public class ColeccionablesPlayer : MonoBehaviour
{
    private Inventario inventario;
    public static string objAcoleccionar;

    void Start()
    {
        inventario = FindObjectOfType<Inventario>();
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.CompareTag("vida"))
        {
            // Lógica para vida...
            Destroy(obj.gameObject);
            return;
        }
        
        // Para todos los otros items coleccionables
        if(EsItemInventariable(obj.tag))
        {
            objAcoleccionar = obj.tag;
            inventario.EscribeEnArreglo();
            Destroy(obj.gameObject);
        }
    }

    private bool EsItemInventariable(string tag)
    {
        switch(tag)
        {
            case "mana":
            case "carne":
            case "moneda":
            case "slime":
            case "Arco":
            case "Espada":
            case "trofeoRata":
            case "trofeoSlime":
            case "gemaRoja":
            case "gemaVerde":
            case "gemaAzul":
            case "gemaAmarilla":
            case "gemaRosa":
                return true;
                
            default:
                return false;
        }
    }
}