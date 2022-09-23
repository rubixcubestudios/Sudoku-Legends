using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusic : MonoBehaviour
{
    public AudioClip[] audioClips;
    public TMP_Text ChangeText;
    private AudioSource audioSource;
    public TMP_Dropdown dropdown;
    public float audiotrack;
    public int index;
    public bool setAudio = true;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var ps in audioClips)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = ps.name });
            
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(); });
        dropdown.value = SkillzCrossPlatform.Random.Range(1, audioClips.Length);
        //audiotrack = audioClips[index].length;
    }

    private void Update()
    {
        if (audiotrack >= 0)
        {
            audiotrack -= Time.deltaTime;
            setAudio = true;
        }

        if (audiotrack <= 0)
        {
            if (setAudio)
            {
                dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(); });
                dropdown.value = SkillzCrossPlatform.Random.Range(0, audioClips.Length);
                //audiotrack = audioClips[index].length;
                setAudio = false;
            }
        }
    }

    private void DropdownItemSelected()
    {
        index = dropdown.value;
        AudioSwitch(index);
        ChangeText.text = dropdown.options[index].text;
        audiotrack = audioClips[index].length;
        //Debug.Log("Track: "+ index + " Length: "+ audioClips[index].length);

    }

    public void AudioSwitch(int randomnumber)
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClips[randomnumber];
        audioSource.Play();
    }

}
