using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTodayScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("myDate"))
        {
            DateTime newDate = System.DateTime.Now.Date;
            string newStringDate = Convert.ToString(newDate);
            PlayerPrefs.SetString("myDate", newStringDate);
            Score_Save_Pref.SetBestScore(0);
        }
        else
        ResetTime();
    }

    private static void ResetTime()
    {
        string stringDate = PlayerPrefs.GetString("myDate");
        DateTime oldDate = Convert.ToDateTime(stringDate);
        DateTime newDate = System.DateTime.Now.Date;
        //Debug.Log("LastDay: " + oldDate);
        //Debug.Log("CurrDay: " + newDate);

        TimeSpan difference = newDate.Subtract(oldDate);
        if (difference.Days >= 1)
        {
            Debug.Log("New Score");
            string newStringDate = Convert.ToString(newDate);
            PlayerPrefs.SetString("myDate", newStringDate);
            Score_Save_Pref.SetBestScore(0);
        }
    }
}
