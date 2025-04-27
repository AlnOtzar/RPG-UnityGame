using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("Configuración Audio")]
    public AudioSource audioSource;
    public float fadeSpeed = 0.5f;

    private bool playerInside = false;
    private float targetVolume = 0f; // Nuevo: volumen objetivo

    private void Start()
    {
        if (audioSource != null)
        {
            audioSource.volume = 0f; // Inicia en silencio
            audioSource.Play(); // Pero ya está reproduciéndose
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            targetVolume = 1f; // Volumen objetivo ahora es 1 (max)
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            targetVolume = 0f; // Volumen objetivo ahora es 0 (min)
        }
    }

    private void Update()
    {
        if (audioSource == null) return;

        // Suavemente cambia el volumen hacia el objetivo
        audioSource.volume = Mathf.MoveTowards(
            audioSource.volume,
            targetVolume,
            fadeSpeed * Time.deltaTime
        );

        // Opcional: Detener la música si el volumen llega a 0 y no está dentro
        if (!playerInside && audioSource.volume <= 0f)
        {
            audioSource.Stop();
        }
        // Si está dentro y no está reproduciéndose, iníciala
        else if (playerInside && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
