using System.Collections;
using System.Collections.Generic;
using SkillzSDK;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProfileInfo : MonoBehaviour
{
    [Header("Raw Image")]
    public RawImage _profileImage;
    public RawImage _flagImage;
    [Header("Text")]
    public TMP_Text _userName;
    public TMP_Text _playerHighscore;


    // Start is called before the first frame update
    void Start()
    {

#if UNITY_IOS || UNITY_ANDROID

        string urlProfile = SkillzCrossPlatform.GetPlayer().AvatarURL;
        string urlFlag = SkillzCrossPlatform.GetPlayer().FlagURL;
        StartCoroutine(DownloadImage(urlProfile, _profileImage));
        StartCoroutine(DownloadImage(urlFlag, _flagImage));
        _userName.text = SkillzCrossPlatform.GetPlayer().DisplayName;
        GetDefaultData();

#elif UNITY_2019_1_OR_NEWER

        string urlProfile = "https://adhurihasrate.in/wp-content/uploads/2020/12/sad-whatsapp-profile-pic-9.jpeg";
        string urlFlag = "https://cdn-icons-png.flaticon.com/512/206/206626.png";
        StartCoroutine(DownloadImage(urlProfile, _profileImage));
        StartCoroutine(DownloadImage(urlFlag, _flagImage));
        _userName.text = "QueenBitchLoad";
        _playerHighscore.text = Score_Save_Pref.GetBestScore().ToString("N0");

#endif

    }

    IEnumerator DownloadImage(string MediaUrl, RawImage rawImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            rawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }

    void OnReceivedData(Dictionary<string, ProgressionValue> data)
    {
        Debug.LogWarning("Success");
        // Now do something with the data
        data.TryGetValue("best_score_lifetime", out var myBestValue);
        _playerHighscore.text = myBestValue.Value;
        Score_Save_Pref.SetBestScore(int.Parse(myBestValue.Value));

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

        return key;
    }

}
