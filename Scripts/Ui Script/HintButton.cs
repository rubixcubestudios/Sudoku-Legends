using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AmountOfTimes : MonoBehaviour
{
    public GameObject GameObjectReward;
    public Button TurnOffButton;
    public TMP_Text CountText;
    private int CountTimes = 1;
    private bool Value = false;

    private void OnEnable()
    {
        GameEvents.onHintButton += TimeSpend;
    }

    private void OnDisable()
    {
        GameEvents.onHintButton -= TimeSpend;
    }

    void TimeSpend(bool SetValue)
    {
        Value = SetValue;
    }

    public void TimeSpendOn()
    {
        if (Value)
        {
            CountText.text = CountTimes.ToString();
            GameObjectReward.SetActive(false);
            TurnOffButton.interactable = false;
        }
    }

}
