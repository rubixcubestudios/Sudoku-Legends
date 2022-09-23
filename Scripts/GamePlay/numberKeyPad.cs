using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numberKeyPad : MonoBehaviour
{
    public GameObject KeyPad;

    private void Start()
    {
        GameEvents.OnAddKeyBoardMethod(false);
    }

    private void OnEnable()
    {
        GameEvents.OnAddKeyBoard += SetKeyPadbool;
    }

    private void OnDisable()
    {
        GameEvents.OnAddKeyBoard -= SetKeyPadbool;
    }

    public void SetKeyPadbool(bool v)
    {
        KeyPad.SetActive(v);
    }

}
