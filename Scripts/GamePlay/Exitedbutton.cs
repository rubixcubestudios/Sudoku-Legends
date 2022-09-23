using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exitedbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //KeyCode.Escape is the Android back key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Optionally, you could show a "Are you sure you want to exit" prompt
            // (Just make sure the prompt can be confirmed via hitting back again)
            Application.Quit();
        }
    }
}
