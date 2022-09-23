using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsLink : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("TurnOff"))
        {
            base.gameObject.SetActive(false);
        }
        else
        {
            base.gameObject.SetActive(true);
        }
    }

    public void Close()
    {
        base.gameObject.SetActive(false);
        PlayerPrefs.SetString("TurnOff", "Set");
    }

    public void AddingAdsLink(string AddLink)
    {
        Application.OpenURL(AddLink);
        base.gameObject.SetActive(false);
        PlayerPrefs.SetString("TurnOff", "Set");
    }

}
