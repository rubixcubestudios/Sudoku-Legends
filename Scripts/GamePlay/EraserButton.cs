using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EraserButton : Selectable , IPointerClickHandler
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.PlayOneShot(audioClip);
        GameEvents.OnClearNumberMethod();
    }
}
