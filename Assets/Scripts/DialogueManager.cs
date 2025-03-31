using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; //Patron Singleton si ya hay una instancia del manager en la escena, esta se destruye

    public GameObject dialoguePanel; //Panel principal
    public GameObject pressEPanel; //Panel de E
    public TextMeshProUGUI npcText; //Texto Panel
    public TextMeshProUGUI npcNombre; //TextoNombre
    public Image imagenNPC; //Imagen NPC
    public float textSpeed = 0.05f; //velocidad, mientras mas cerca a 0 mas rapido


    private string[] lines; //Lineas del dialogo
    private int index; //cuenta de la linea actual
    private bool isWriting = false; //para indicar si el texto se esta escribiendo
    private System.Action onDialogueEnd;

    private NPCInteraction currentNPC;  // Referencia al NPC que activó el panel

    private void Awake() //La instancia que se destruye si ya hay una activa
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() //los paneles se descativan automaticamente
    {
        dialoguePanel.SetActive(false);
        pressEPanel.SetActive(false);
    }

    public void ShowPressEPanel(NPCInteraction npc, Vector3 position) //basicamente es el panel E, sirve para mostrarlo cuando nos acercamos al npc
    {
        if (currentNPC == null || npc == currentNPC)  
        {
            currentNPC = npc;  
            pressEPanel.SetActive(true);
            pressEPanel.transform.position = position;
        }
    }

    public void HidePressEPanel(NPCInteraction npc) //para ocultar el panel
    {
        if (currentNPC == npc)  
        {
            pressEPanel.SetActive(false);
            currentNPC = null;
        }
    }

    public void StartDialogue(string nombre, Sprite imagen, string[] dialogos, System.Action callback = null)
    {
        pressEPanel.SetActive(false);  //El panel se oculta al presionar E

        index = 0; //lleva la cuenta del dialogo
        lines = dialogos;
        onDialogueEnd = callback; //llama a una funcion cuando el dialogo se acaba

        npcNombre.text = nombre;
        imagenNPC.sprite = imagen;
        npcText.text = "";
        dialoguePanel.SetActive(true);
        FindAnyObjectByType<movPlayer>().BloquearMovimiento(true);  
        StartCoroutine(WriteLine());

    }

    private IEnumerator WriteLine()
    {
        isWriting = true;
        npcText.text = "";

        foreach (char letter in lines[index].ToCharArray())
        {
            npcText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isWriting = false;
    }

    public void NextLine()
    {
        if (isWriting)
        {
            StopAllCoroutines();
            npcText.text = lines[index];
            isWriting = false;
            return;
        }

        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(WriteLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        FindAnyObjectByType<movPlayer>().BloquearMovimiento(false);
        onDialogueEnd?.Invoke();
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            NextLine();
        }
    }
}
