using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class NumberButton : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler , IPointerExitHandler
{
    public int value = 0;
    public AudioSource audioSource;
    public AudioClip audioClip;


    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.updateSquareNumberMethod(value);
        audioSource.PlayOneShot(audioClip);
        GameEvents.OnAddKeyBoardMethod(false);
        HelpScript._instance.NumberButton();
        GameEvents.onActiveHintButtonMethod(false);

        if (Timer_.instance != null)
        {
            Timer_.instance.StopTimer(false);
        }

    }

    public void OnSubmit(BaseEventData eventData)
    {

    }
}
