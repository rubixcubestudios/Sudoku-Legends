using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerColors : MonoBehaviour
{
    public static SinglePlayerColors instance;
    public Color pushColorText;

    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;
    }


}
