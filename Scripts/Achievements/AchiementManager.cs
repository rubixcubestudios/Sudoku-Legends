using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SkillzSDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AchiementManager : MonoBehaviour
{
    public static AchiementManager instance;

    [Header("My Achievement")]
    public AchievementObject achievementObject;

    [Header("Content")]
    public Transform parent;

    [Header("GameObject Achievment")]
    public AchiementManagerUi achievementObj;

    private Color progressBarColor = Color.yellow;

    private void Awake()
    {
        instance = this;

#if UNITY_IOS || UNITY_ANDROID
        SkillzGetData();
        SkillzDefaultGet();
        DisplayName();
#endif
    }

    private void Update()
    {
        PopulateAchievementList();
    }

    public void ExitedProgress()
    {
        SkillzCrossPlatform.ReturnToSkillz();
        SceneManager.LoadScene("SkillzMatch");
        PopulateAchievementList();

#if UNITY_IOS || UNITY_ANDROID
        SkillzGetData();
        SkillzDefaultGet();
        DisplayName();
#endif
    }

    public void PopulateAchievementList()
    {
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }

            RectTransform rect = parent.GetComponent<RectTransform>();
            int ach = 115 * achievementObject.achievements.Length;
            rect.sizeDelta = new Vector2(0, ach);
        }

        foreach (Achievement achievement in achievementObject.achievements.OrderBy(x => x.unlocked))
        {
            var achievementObject = Instantiate(achievementObj, parent);
            achievementObject.progressSlider.maxValue = achievement.goal;
            achievementObject.HeaderText.text = achievement.HeaderText;
            achievementObject.mainIcon.sprite = achievement.mainIcon;

            if (achievement.unlocked)
            {
                achievementObject.backgroundImage.color = progressBarColor;
                achievementObject.mainIcon.sprite = achievement.mainIcon;
                achievementObject.progressSlider.value = achievement.goal;
                achievementObject.ResaultText.text = @$"COMPLETED";
            }
            else
            {
                achievementObject.backgroundImage.color = achievement._LockedAchivement;
                achievementObject.progressSlider.value = achievement.current;
                achievementObject.ResaultText.text = @$"{achievement.current}/{achievement.goal}";
            }

        }

        Score_Save_Pref.LoadAchievementData(achievementObject.achievements);
    }


    private static void DisplayName()
    {
        if (!(SkillzCrossPlatform.GetPlayer().DisplayName == PlayerPrefs.GetString("DisplayName")))
        {
            PlayerPrefs.SetString("DisplayName", SkillzCrossPlatform.GetPlayer().DisplayName);
            Debug.Log("Display Name: " + PlayerPrefs.GetString("DisplayName") + " = " + SkillzCrossPlatform.GetPlayer().DisplayName);
            if (AchiementManager.instance.achievementObject != null)
            {
                var achManage = AchiementManager.instance.achievementObject;

                foreach (var myach in achManage.achievements)
                {
                    Score_Save_Pref.MakeDeleteData(myach.ID, achManage.achievements);
                }

            }
        }
    }

    // Handle success response
    void OnReceivedData(Dictionary<string, SkillzSDK.ProgressionValue> data)
    {
        Debug.LogWarning("Success");
        // Do something with the data, such as populate a custom Progression scene

        int value_Won = int.Parse(data["games_won"].Value);
        achievementObject.AddAchievementProgress(EnumBadges.Win_25_Times, value_Won);
        achievementObject.AddAchievementProgress(EnumBadges.Win_50_Times, value_Won);
        achievementObject.AddAchievementProgress(EnumBadges.Win_100_Times, value_Won);
        achievementObject.AddAchievementProgress(EnumBadges.Win_200_Times, value_Won);

        int value_ = int.Parse(data["cash_games_won"].Value);
        achievementObject.AddAchievementProgress(EnumBadges.Winning_Cash_25_Times, value_);
        achievementObject.AddAchievementProgress(EnumBadges.Winning_Cash_50_Times, value_);
        achievementObject.AddAchievementProgress(EnumBadges.Winning_Cash_100_Times, value_);
        achievementObject.AddAchievementProgress(EnumBadges.Winning_Cash_200_Times, value_);

    }

    void OnReceivedPlayerData(Dictionary<string, SkillzSDK.ProgressionValue> data)
    {

        int value_Com = int.Parse(data["Complete_Coins"].Value);
        achievementObject.AddAchievementProgress(EnumBadges.Complete_Coins, value_Com);

        int value_ComCash = int.Parse(data["Complete_Cash"].Value);
        achievementObject.AddAchievementProgress(EnumBadges.Complete_Cash, value_ComCash);

        int value_Hint = int.Parse(data["Complete_Hint_Coins"].Value);
        achievementObject.AddAchievementProgress(EnumBadges.Complete_Hint_Coins, value_Hint);

        int value_HintCash = int.Parse(data["Complete_Hint_Cash"].Value);
        achievementObject.AddAchievementProgress(EnumBadges.Complete_Hint_Cash, value_HintCash);

        int value_Correct = int.Parse(data["Correct_Answers"].Value);
        correctAnswers(value_Correct);

        int value_No_Wrong_Coins = int.Parse(data["Complete_NO_Wrong_Coins"].Value);
        achievementObject.AddAchievementProgress(EnumBadges.Complete_NO_Wrong_Coins, value_No_Wrong_Coins);


        int value_No_Wrong_Cash = int.Parse(data["Complete_NO_Wrong_Cash"].Value);
        achievementObject.AddAchievementProgress(EnumBadges.Complete_NO_Wrong_Cash, value_No_Wrong_Cash);
    }


    // Handle failure response
    void OnReceivedDataFail(string reason)
    {
        Debug.LogWarning("Fail: " + reason);
        // Continue without Progression data
    }

    public void SkillzDefaultGet()
    {
        List<string> keys = new List<string>() { "games_won", "cash_games_won" };
        SkillzCrossPlatform.GetProgressionUserData(ProgressionNamespace.DEFAULT_PLAYER_DATA, keys,
            OnReceivedData, OnReceivedDataFail);

    }

    public void SkillzGetData()
    {
        List<string> keyPlayers = new List<string>()
        {
            "Complete_Coins",
            "Complete_Cash",
            "Complete_Hint_Coins",
            "Complete_Hint_Cash" ,
            "Correct_Answers",
            "Complete_NO_Wrong_Coins",
            "Complete_NO_Wrong_Cash"
        };
        SkillzCrossPlatform.GetProgressionUserData(ProgressionNamespace.PLAYER_DATA, keyPlayers,
            OnReceivedPlayerData, OnReceivedDataFail);
    }

    private void correctAnswers(int value)
    {
        achievementObject.AddAchievementProgress(EnumBadges.Correct_Answers_3, value);
        achievementObject.AddAchievementProgress(EnumBadges.Correct_Answers_6, value);
        achievementObject.AddAchievementProgress(EnumBadges.Correct_Answers_12, value);
        achievementObject.AddAchievementProgress(EnumBadges.Correct_Answers_24, value);
    }
}
