using UnityEngine;
using TMPro;  // Importa la librería de TextMeshPro
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject interactionPanel;  // Panel de interacción
    public TextMeshProUGUI npcText;  // Texto que se mostrará con TextMeshPro
    public float interactDistance = 0.8f;  // Distancia para interactuar
    public string npcMessage = "¡Hola! ¿Cómo estás?";  // En el inspector deberemos modificar cada mensaje

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;  // Encuentra al jugador
        interactionPanel.SetActive(false);  // Asegúrate de que el panel esté oculto al inicio
    }

    void Update()
    {
        // Calcula la distancia entre el NPC y el jugador
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < interactDistance)  // Si el jugador está cerca
        {
            interactionPanel.SetActive(true);  // Muestra el panel
            npcText.text = npcMessage;  // Muestra el mensaje
        }
        else
        {
            interactionPanel.SetActive(false);  // Oculta el panel cuando el jugador se aleja, a cada npc le debemos agregar el panel y el texto
        }
    }
}
