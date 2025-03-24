using UnityEngine;

public class DialogoNPC : MonoBehaviour
{
    public GameObject txtDialogo;

    private void OnMouseDown()
    {
        this.gameObject.SetActive(false);
    }
}
