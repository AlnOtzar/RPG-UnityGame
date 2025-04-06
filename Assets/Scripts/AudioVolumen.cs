using UnityEngine;
using UnityEngine.UI;

public class AudioVolumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imagenMute;

    void Start()
    {
        sliderValue = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        slider.value = sliderValue; // Corregido
        AudioListener.volume = sliderValue;
        RevisaMute();
    }

    public void CambiarSlider(float valor)
    {
        sliderValue = valor; // Corregido
        slider.value = sliderValue; // Corregido
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = sliderValue; // Corregido
        RevisaMute();
    }

    public void RevisaMute()
    {
        if (imagenMute != null) // Evitar error si no está asignada
        {
            imagenMute.enabled = (sliderValue == 0);
        }
    }
}
