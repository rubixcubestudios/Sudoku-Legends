using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SkillzSDK;
using UnityEngine.Networking;

public class UiScoreManger : MonoBehaviour
{
    #region Singleton

    private static UiScoreManger _instance;
    public static UiScoreManger Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

    }

    #endregion

    public GameObject scorethisText;
    private TMP_Text scoreBoardText;

    //animation
    public float animationTime = 1.5f;
    private float desireNumber;
    private float initialNumber;

    [HideInInspector]
    public float currentNumber { set; get; }

    public GameObject PlayerPrefab;
    private Transform _tranformthis;

    public Color _newColors;
    private Color currentColor = new Color(0.9811321f, 0.9589464f, 0.8376647f, 1f);
    private Color lerpedColor;

    public GameObject[] scoreboarder;
    public GameObject uIWinGameObject;

    [Header("Profile Title")]
    public TMP_Text Username;
    public RawImage profileImage;
    public RawImage flagImage;

    [Header("Number remain")]
    public TMP_Text NumberLeft;

    [Header("Pop up")]
    public TMP_Text BoardScoreText;
    public TMP_Text GOCount;

    public Animator scoreAnimator;

    [Header("Final Results")]
    //Top
    public TMP_Text scoreText;
    public TMP_Text timeBonusText;
    public TMP_Text timeBonusPointsText;
    public TMP_Text wrongText;
    public TMP_Text finalScoreText;
    public TMP_Text resultWinOrNotText;

    private int correctNumber_ = 0;
    private int wrongNumbers_ = 0;
    private float scoreValue = 0;
    private float priScore = 50;
    int valueNew = 1;

    [HideInInspector]
    private int FailedTimes = 0;
    public int finalScore;
    private float timerScore;
    private bool colorBool;

    public int finalScoreMade() { return finalScore; }
    public float finalTimeSet() { return timerScore; }

    public GameObject takeTime;

    private bool isAnswerWrong;

    private const string SkillzScene = "SkillzMatch";


    // Start is called before the first frame update
    void Start()
    {
        scoreBoardText = scorethisText.GetComponent<TMP_Text>();
        _tranformthis = scorethisText.GetComponent<Transform>();
        
        scoreBoardText.text = currentNumber.ToString("0");
        colorBool = false;
        isAnswerWrong = true;
        NumberLeft.text = (81 - PlayerSettings.difficulty).ToString() + "/81";


#if UNITY_IOS || UNITY_ANDROID
        Username.text = SkillzCrossPlatform.GetPlayer().DisplayName;
        string ProfileImage = SkillzCrossPlatform.GetPlayer().AvatarURL;
        string FlagImage = SkillzCrossPlatform.GetPlayer().FlagURL;
        StartCoroutine(DownloadImage(ProfileImage, profileImage));
        StartCoroutine(DownloadImage(FlagImage, flagImage));
#elif UNITY_2019_1_OR_NEWER
        Username.text = "QueenBitchLoad";
        string ProfileImage = "https://adhurihasrate.in/wp-content/uploads/2020/12/sad-whatsapp-profile-pic-9.jpeg";
        string FlagImage = "https://cdn-icons-png.flaticon.com/512/206/206626.png";
        StartCoroutine(DownloadImage(ProfileImage, profileImage));
        StartCoroutine(DownloadImage(FlagImage, flagImage));
#endif

    }

    private void Update()
    {
        if (PlayerSettings.setDiff >= valueNew)
        {
           valueNew = PlayerSettings.setDiff + 1;
           NumberLeft.text = ((81 - PlayerSettings.difficulty)+PlayerSettings.setDiff).ToString()+"/81";

        }

        if (currentNumber != desireNumber)
        {
            if (initialNumber < desireNumber)
            {
                currentNumber += (animationTime * Time.deltaTime) * (desireNumber - initialNumber);
                if (currentNumber >= desireNumber)
                    currentNumber = desireNumber;
            }
            else
            {
                currentNumber -= (animationTime * Time.deltaTime) * (initialNumber - desireNumber);
                if (currentNumber <= desireNumber)
                    currentNumber = desireNumber;
            }

            scoreBoardText.text = currentNumber.ToString("0");
        }

        for (int i = 0; i < scoreboarder.Length; i++)
        {
            if (colorBool)
            {
                lerpedColor = Color.Lerp(currentColor, _newColors, Mathf.PingPong(Time.time, 1));
                scoreboarder[i].GetComponent<Image>().color = lerpedColor;
                BoardScoreText.color = lerpedColor;
                NumberLeft.color = lerpedColor;
            }
            else
            {
                BoardScoreText.color = currentColor;
                NumberLeft.color = currentColor;
                scoreboarder[i].GetComponent<Image>().color = currentColor;
            }
        }

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

#region UiScoreSystem

    public void AddToNumber(float value)
    {
        initialNumber = currentNumber;
        desireNumber += value;
        popUpgems(value);
    }

    public void popUpgems(float addnumbers)
    {
        TMP_Text textobject = PlayerPrefab.GetComponent<TMP_Text>();
        textobject.text = "+" + addnumbers.ToString("0");
        Instantiate(PlayerPrefab, _tranformthis);
    }

#endregion

    private void OnEnable()
    {
        GameEvents.OnWrongNumber += WrongNumber;
        GameEvents.OnRightNumber += RightNumbers;
        GameEvents.OnBoardCompleted += OnBoardCompleted;
        GameEvents.OnAddWrongNumber += FailTemps;
    }

    private void OnDisable()
    {
        GameEvents.OnWrongNumber -= WrongNumber;
        GameEvents.OnRightNumber -= RightNumbers;
        GameEvents.OnBoardCompleted -= OnBoardCompleted;
        GameEvents.OnAddWrongNumber -= FailTemps;
    }

    /// <summary>
    /// This is Last Result of the game to be played.
    /// </summary>
    public void lastResult()
    {
        timerScore = (int)Timer_.instance.GetScoreTimer();
        finalScore = (int)(currentNumber + timerScore) - FailedTimes;

        string wrongText = "-" + FailedTimes;
        string timeTexts = $@"= {timerScore} Add Points";

        uiText(wrongText, currentNumber.ToString(), Timer_.instance._Timing, timeTexts, finalScore.ToString());
        resultWinOrNotText.text = "GameOver";

    }


    public void uiText(string wrong, string score, string timeB,
       string timeBP, string final)
    {
        wrongText.text = wrong;
        scoreText.text = score;
        timeBonusText.text = timeB;
        timeBonusPointsText.text = timeBP;
        finalScoreText.text = final;
    }

    private void FailTemps(int number)
    {
        FailedTimes += number;
    }

    private void WrongNumber()
    {
        correctNumber_ = 0;
        colorBool = false;
        isAnswerWrong = false;
        wrongNumbers_++;

        if (wrongNumbers_ >= 1)
        {
            if (Timer_.instance.delta_time >= 10)
            {
                Timer_.instance.delta_time -= 10;
                takeTime.GetComponent<TMP_Text>().text = "-10 Sec";
                takeTime.GetComponent<TMP_Text>().color = Color.red;
                takeTime.SetActive(true);
            }
        }

        if (wrongNumbers_ >= 3)
        {
            if (Timer_.instance.delta_time >= 30)
            {
                Timer_.instance.delta_time -= 30;
                takeTime.GetComponent<TMP_Text>().text = "-30 Sec";
                takeTime.GetComponent<TMP_Text>().color = Color.red;
                takeTime.SetActive(true);
            }
        }

    }

    private void RightNumbers()
    {
        takeTime.SetActive(false);
        wrongNumbers_ = 0;

        if (correctNumber_ <= 2)
        {
            scoreValue = priScore;
            AddToNumber(scoreValue);
        }

        correctNumber_++;
        if (correctNumber_ >= 3)
        {
            scoreValue = priScore + (10 * correctNumber_);
            AddToNumber(scoreValue);
            GOCount.text = $@"x{correctNumber_}";
            scoreAnimator.SetTrigger("Set");
            Timer_.instance.delta_time += 5;
            takeTime.GetComponent<TMP_Text>().text = "+5 Sec";
            takeTime.GetComponent<TMP_Text>().color = Color.yellow;
            takeTime.SetActive(true);
            colorBool = true;

            bool correct = correctNumber_ >= Score_Save_Pref.GetAchievementPref("current", EnumBadges.Correct_Answers_24);
            if (correct)
            {
                PlayerSaveDataSkillz.UpdateData("Correct_Answers", correctNumber_);
            }
        }
    }

    private void OnBoardCompleted(bool isPush)
    {
        uIWinGameObject.SetActive(true);
        GameEvents.OnSetGameInactiveMethod();
        resultWinOrNotText.text = "Victory";

        if (AchiementManager.instance.achievementObject != null)
        {
            if (SkillzCrossPlatform.GetMatchInfo().IsCash == true)
            {
                AchiementManager.instance.achievementObject.AddAchievementProgress(EnumBadges.Complete_Cash, 1);
                PlayerSaveDataSkillz.UpdateData("Complete_Cash", 1);

                if (isAnswerWrong)
                {
                    AchiementManager.instance.achievementObject.AddAchievementProgress(EnumBadges.Complete_NO_Wrong_Cash, 1);
                    PlayerSaveDataSkillz.UpdateData("Complete_NO_Wrong_Cash", 1);
                }

                if (!isPush)
                {
                    AchiementManager.instance.achievementObject.AddAchievementProgress(EnumBadges.Complete_Hint_Cash, 1);
                    PlayerSaveDataSkillz.UpdateData("Complete_Hint_Cash", 1);
                  
                }

            }
            else
            {
                AchiementManager.instance.achievementObject.AddAchievementProgress(EnumBadges.Complete_Coins, 1);
                PlayerSaveDataSkillz.UpdateData("Complete_Coins", 1);

                if (isAnswerWrong)
                {
                    AchiementManager.instance.achievementObject.AddAchievementProgress(EnumBadges.Complete_NO_Wrong_Coins, 1);
                    PlayerSaveDataSkillz.UpdateData("Complete_NO_Wrong_Coins", 1);
                }

                if (!isPush)
                {
                    AchiementManager.instance.achievementObject.AddAchievementProgress(EnumBadges.Complete_Hint_Coins, 1);
                    PlayerSaveDataSkillz.UpdateData("Complete_Hint_Coins", 1);
                    Debug.Log("It hasn't been pushed");
                }
            }

        }

    }

#region SubmiteScore
    public void SubmitSkillzScore()
    {
        int score = GetScore();
        SkillzCrossPlatform.SubmitScore(score, OnSuccess, OnFailure);
        Debug.Log("Score Submited: " + score);
    }

    private int GetScore()
    {
        int score = finalScoreMade();
        return score;
    }

    void OnSuccess()
    {
        Debug.Log("Success");
        MatchComplete();
    }

    void MatchComplete()
    {
        SkillzCrossPlatform.ReturnToSkillz();
        SceneManager.LoadScene(SkillzScene);
        Debug.Log("Return to Skillz: " + SkillzCrossPlatform.ReturnToSkillz());
    }

    void OnFailure(string reason)
    {
        Debug.LogWarning("Fail: " + reason);
        StartCoroutine(RetrySubmit());
    }

    IEnumerator RetrySubmit()
    {
        yield return new WaitForSeconds(2);
        SubmitSkillzScore();
    }

#endregion

}
