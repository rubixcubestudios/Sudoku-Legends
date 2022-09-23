using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Score_Save_Pref
{
    private static string username()
    {
        if (SkillzCrossPlatform.GetPlayer() != null)
        {
            var name = SkillzCrossPlatform.GetPlayer().DisplayName;
            Debug.Log("Found Users name: " + name);
            return name;
        }
        else
        {
            Debug.Log("Not Found User Name, There isn't any names.");
            return null;
        }
    }

    public static string DateNameTime()
    {
        return "PlayDate_" + username();
    }

    public static int GetTodayScore()
    {
        return PlayerPrefs.GetInt(username() + "_TodayScore_Save");
    }

    public static void SetTodayScore(int score)
    {
        username();
        PlayerPrefs.SetInt(username() + "_TodayScore_Save", score);
    }

    public static int GetBestScore()
    {
        return PlayerPrefs.GetInt(username() + "best_score_lifetime");
    }

    public static void SetBestScore(int score)
    {
        username();
        PlayerPrefs.SetInt(username() + "best_score_lifetime", score);
    }

    public static void SetBenchMark(int value)
    {
        PlayerPrefs.SetInt("SetBenchMark",value);
    }

    public static int GetBenchMark()
    {
        return PlayerPrefs.GetInt("SetBenchMark");
    }

    //Save Achievements

    public static int GetAchievementPref(string type, EnumBadges achID)
    {
        return PlayerPrefs.GetInt(achID + "_" + type.ToUpper());
    }

    public static void SetAchievementPref(string type, EnumBadges achID, int value)
    {
        PlayerPrefs.SetInt(achID + "_" + type.ToUpper(), value);
    }

    public static void SetDeleteData(string type, EnumBadges achID, int value)
    {
        PlayerPrefs.DeleteKey(achID + "_" + type.ToUpper());
    }

    public static void MakeDeleteData(EnumBadges achID, Achievement[] achievements)
    {
        Achievement achievement = achievements.FirstOrDefault(x => x.ID == achID);

        SetDeleteData("current", achievement.ID, achievement.current);
        SetDeleteData("Unlocked", achievement.ID, (achievement.unlocked == true) ? 1 : 0);
    }

    public static void LoadAchievementData(Achievement[] achievements)
    {
        foreach (Achievement achievement in achievements)
        {
            achievement.current = GetAchievementPref("current", achievement.ID);
            achievement.unlocked = (GetAchievementPref("Unlocked", achievement.ID) == 1) ? true : false;
        }

    }

    public static void SaveAchievementData(EnumBadges achID, Achievement[] achievements)
    {
        Achievement achievement = achievements.FirstOrDefault(x => x.ID == achID);

        SetAchievementPref("current", achievement.ID, achievement.current);
        SetAchievementPref("Unlocked", achievement.ID, (achievement.unlocked == true) ? 1 : 0);
    }
}
