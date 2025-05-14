using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public string[] lines;
    public Sprite imagenNPC;
    public string nombreNPC;
    public float interactDistance = 0.2f;

    public bool esTienda = false; // << NUEVO: Marcar si este NPC es una tienda
    public GameObject tiendaPanel; // << NUEVO: Asignar el panel de tienda

    private Transform player;
    private bool playerInRange = false;
    private bool interactionActive = false;

    private bool tiendaAbierta = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (tiendaAbierta && Input.GetKeyDown(KeyCode.E))
        {
            CerrarTienda();
            return; 
        }

        if (distance < interactDistance && !interactionActive)
        {
            DialogueManager.Instance.ShowPressEPanel(this, transform.position + new Vector3(0, 1.5f, 0));
            playerInRange = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interactuar();
            }
        }
        else if (playerInRange && distance >= interactDistance)
        {
            DialogueManager.Instance.HidePressEPanel(this);
            playerInRange = false;
        }
    }


    private void Interactuar()
    {
        interactionActive = true;

        DialogueManager.Instance.HidePressEPanel(this);

        if (esTienda && tiendaPanel != null)
        {
            tiendaPanel.SetActive(true);
            tiendaAbierta = true;
            FindAnyObjectByType<movPlayer>().BloquearMovimiento(true);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(nombreNPC, imagenNPC, lines, FinInteraccion);
        }
    }



    private void FinInteraccion()
    {
        interactionActive = false;
        if (playerInRange)
        {
            DialogueManager.Instance.ShowPressEPanel(this, transform.position + new Vector3(0, 1.5f, 0));
        }
    }

    // Método auxiliar para cerrar tienda desde un botón UI
    public void CerrarTienda()
    {
        tiendaPanel.SetActive(false);
        tiendaAbierta = false;
        interactionActive = false;
        FindAnyObjectByType<movPlayer>().BloquearMovimiento(false);

        if (playerInRange)
        {
            DialogueManager.Instance.ShowPressEPanel(this, transform.position + new Vector3(0, 1.5f, 0));
        }
    }

}
