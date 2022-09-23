using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audios")]
    public AudioMixer audioMix;
    
    public Toggle onMusic;
    private float volum;
    public string keyValue = "Music";

    private void Update()
    {
        volum = PlayerPrefs.GetFloat(keyValue);
        audioMix.SetFloat(keyValue, volum);

        if(PlayerPrefs.GetFloat(keyValue) < -79f)
            onMusic.isOn = false;
    }

    public void onMute(bool vl)
    {
        if (vl)
        {
            PlayerPrefs.SetFloat(keyValue, 0);
            onMusic.isOn = true;
        }
        else
        {
            PlayerPrefs.SetFloat(keyValue, -80);
            onMusic.isOn = false;
        }
    }

}
