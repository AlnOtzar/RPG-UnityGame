using UnityEngine;
using System.Collections;


public class Audio : MonoBehaviour
{
    private AudioSource music;
    public AudioClip ClickAudio;
    public AudioClip switchAudio;

    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    public void ClickAudioOn()
    {
        music.PlayOneShot(ClickAudio);
    }

    public void SwitchAudioOn()
    {
        music.PlayOneShot(switchAudio);
    }
}
