using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFeedClose : MonoBehaviour
{

    public GameObject newFeed;
    private string newUpdate = "21Feb2021";

    private void Start()
    {
        if (PlayerPrefs.HasKey(newUpdate))
        {
            newFeed.SetActive(false);
        }
        else
        {
            newFeed.SetActive(true);
        }
    }

    public void closeFeed()
    {
        PlayerPrefs.SetString(newUpdate, newUpdate);
        newFeed.SetActive(false);
    }

    public void openFeed()
    {
        PlayerPrefs.DeleteKey(newUpdate);
        newFeed.SetActive(true);
    }
}
