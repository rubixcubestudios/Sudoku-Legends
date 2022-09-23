using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public void _disablethisObject()
    {
        this.gameObject.SetActive(false);
    }

    public void _destorythisObject()
    {
        Destroy(this.gameObject);
    }
}
