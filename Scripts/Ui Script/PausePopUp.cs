using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopUp : MonoBehaviour
{
    public GameObject WinPopUp;
    public AudioClip audioClip;
    public AudioSource mainAudio;
    public AudioSource music;

    public void OnExited()
    {
        WinPopUp.SetActive(true);
        this.gameObject.SetActive(false);
        music.Stop();
        mainAudio.PlayOneShot(audioClip);
    }

    public void OnResume()
    {
        this.gameObject.SetActive(false);
        mainAudio.PlayOneShot(audioClip);
    }
}
