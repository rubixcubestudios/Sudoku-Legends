using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour
{
    public static HelpScript _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject intro_1;
    public GameObject intro_2;
    private bool instro;
    private bool intset;

    private void Start()
    {
        intro_1.SetActive(true);
        intro_2.SetActive(false);
        instro = true;
        intset = true;
    }

    public void StartIntro()
    {
        if (intset)
        {
            intro_1.SetActive(true);
            intset = false;
        }
    }

    public void InstroductionSwap()
    {
        if (instro)
        {
            intro_1.SetActive(false);
            intro_2.SetActive(true);
            instro = false;
        }
    }

    public void NumberButton()
    {
        intro_2.SetActive(false);
    }
}
