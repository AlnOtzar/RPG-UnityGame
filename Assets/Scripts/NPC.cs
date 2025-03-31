using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public string[] lines;
    public Sprite imagenNPC;
    public string nombreNPC;
    public float interactDistance = 0.2f;

    private Transform player;
    private bool playerInRange = false;
    private bool dialogueActive = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < interactDistance && !dialogueActive)
        {
            DialogueManager.Instance.ShowPressEPanel(this, transform.position + new Vector3(0, 1.5f, 0));  
            playerInRange = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
        }
        else if (playerInRange)  
        {
            DialogueManager.Instance.HidePressEPanel(this);
            playerInRange = false;
        }
    }

    private void StartDialogue()
    {
        dialogueActive = true;
        DialogueManager.Instance.StartDialogue(nombreNPC, imagenNPC, lines, EndDialogue);
    }

    private void EndDialogue()
    {
        dialogueActive = false;
        if (playerInRange)  
        {
            DialogueManager.Instance.ShowPressEPanel(this, transform.position + new Vector3(0, 1.5f, 0));
        }
    }
}
