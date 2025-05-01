using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel; 
    public GameObject pressEPanel; 
    public TextMeshProUGUI npcText; 
    public TextMeshProUGUI npcNombre; 
    public Image imagenNPC; 
    public float textSpeed = 0.05f; 


    private string[] lines;
    private int index;
    private bool isWriting = false;
    private System.Action onDialogueEnd;

    private NPCInteraction currentNPC; 

    private void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        pressEPanel.SetActive(false);
    }

    public void ShowPressEPanel(NPCInteraction npc, Vector3 position) //mostrar el panel
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

        index = 0; 
        lines = dialogos;
        onDialogueEnd = callback;

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
