using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SkillzSDK;

public class FinalInfoMatch : MonoBehaviour
{

    public GameObject setPause;
    public AudioSource audioSource;

    [Header("Bottom Results")]
    public TMP_Text _Today;
    public TMP_Text _AvengeScore;
    public TMP_Text _BestScore;

    public GameObject newTodayPrefab;
    public GameObject newBestPrefab;

    private void Awake()
    {
        ExpireNewDate();
    }

    void OnEnable()
    {
#if UNITY_IOS || UNITY_ANDROID
        GetDefaultData();
#endif

        setPause.gameObject.SetActive(false);
        audioSource.Stop();

        if (UiScoreManger.Instance != null)
        {
            UiScoreManger.Instance.lastResult();
        }

        if (Timer_.instance != null )
        {
            Timer_.instance.StopTimer(true);
        }

        _Today.text = todayUiSystem().ToString("N0");

    }

    public void SubmiteScore()
    {
        UiScoreManger.Instance.SubmitSkillzScore();
    }

    private int todayUiSystem()
    {
        int newResult = UiScoreManger.Instance.finalScore;
        int oldResult = Score_Save_Pref.GetTodayScore();
        bool bestResult = newResult > oldResult;

        if (bestResult)
        {
            newTodayPrefab.SetActive(true);
            Score_Save_Pref.SetTodayScore(newResult);
            if (newResult >= Score_Save_Pref.GetBestScore())
            {
                Score_Save_Pref.SetBestScore(newResult);
            }

            return newResult;
        }

        newTodayPrefab.SetActive(false);
        return oldResult;
    }

    void ExpireNewDate()
    {
        DateTime newDate = System.DateTime.UtcNow.Date;

        if (!PlayerPrefs.HasKey(Score_Save_Pref.DateNameTime()))
        {
            string newStringDate = Convert.ToString(newDate);
            PlayerPrefs.SetString(Score_Save_Pref.DateNameTime(), newStringDate);

            Debug.Log("New Times: " + newDate);
        }

        string stringDate = PlayerPrefs.GetString(Score_Save_Pref.DateNameTime());
        DateTime oldDate = Convert.ToDateTime(stringDate).Date;

        TimeSpan difference = newDate.Subtract(oldDate);

        if (difference.Days >= 1)
        {
            Debug.Log("new Day");
            Score_Save_Pref.SetTodayScore(0);
            string newStringDate = Convert.ToString(newDate);
            PlayerPrefs.SetString(Score_Save_Pref.DateNameTime(), newStringDate);
        }
    }

    void OnReceivedData(Dictionary<string, ProgressionValue> data)
    {
        Debug.LogWarning("Success");
        // Now do something with the data
        data.TryGetValue("best_score_lifetime",out var myBestValue);
        int myBValue = int.Parse(myBestValue.Value);


        if (myBValue >= Score_Save_Pref.GetBestScore())
        {
            Score_Save_Pref.SetBestScore(myBValue);
            _BestScore.text = myBValue.ToString("N0");
            PlayerSaveDataSkillz.UpdateData("BEST_SCORE_", myBValue);
        }
        else if (myBValue <= Score_Save_Pref.GetBestScore())
        {
            _BestScore.text = Score_Save_Pref.GetBestScore().ToString("N0");
        }

        

        data.TryGetValue("average_score", out var myAverage);
        _AvengeScore.text = myAverage.Value;
    }

    void OnReceivedDataFail(string reason)
    {
        Debug.LogWarning("Fail: " + reason);
    }

    public void GetDefaultData()
    {
        List<string> keys = GetKeyList();
        SkillzCrossPlatform.GetProgressionUserData(ProgressionNamespace.DEFAULT_PLAYER_DATA, keys, OnReceivedData, OnReceivedDataFail);
    }

    List<string> GetKeyList()
    {
        List<string> key = new List<string>();
        key.Add("best_score_lifetime");
        key.Add("average_score");

        return key;
    }

}
