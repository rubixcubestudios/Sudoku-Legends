using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    public static BackgroundScript instance;

    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;
    }

    public Image[] PreviewImage;
    public Image[] Rsprite;
    public int SetSaveNumber;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        int saveNumber = PlayerPrefs.GetInt("BackG", 0);
        Background(saveNumber);

        if (PreviewImage[1] != null)
        {
            PreviewImage[1].sprite = Rsprite[saveNumber].sprite;
            PreviewImage[1].color = Rsprite[saveNumber].color;
        }

    }

    public void Background(int select)
    {
        SetSaveNumber = select;
        PreviewImage[0].sprite = Rsprite[select].sprite;
        PreviewImage[0].color = Rsprite[select].color;
    }

    public void SaveButton()
    {
        if (PreviewImage[1] != null)
        {
            PreviewImage[1].sprite = Rsprite[SetSaveNumber].sprite;
            PreviewImage[1].color = Rsprite[SetSaveNumber].color;
        }

        panel.SetActive(false);

        PlayerPrefs.SetInt("BackG", SetSaveNumber);
        PlayerPrefs.Save();
    }

    public void ExitedButton()
    {
        int saveNumber = PlayerPrefs.GetInt("BackG");

        if (PreviewImage[1] != null)
        {
            PreviewImage[1].sprite = Rsprite[saveNumber].sprite;
            PreviewImage[1].color = Rsprite[saveNumber].color;
        }
        else { }
        Background(saveNumber);
        panel.SetActive(false);
    }




}
