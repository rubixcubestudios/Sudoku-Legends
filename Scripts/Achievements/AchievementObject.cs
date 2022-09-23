using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SkillzSDK;

[System.Serializable]
public class Achievement
{
    public string name;

    [Header("Achievement Details")]
    public string HeaderText; // Option Display

    [Header("Identifier")]
    public EnumBadges ID;

    [Header("Main Icon")]
    public Sprite mainIcon;

    [Header("Colors")]
    public Color _LockedAchivement = Color.red;

    [Header("Progress")]
    public int current;
    public int goal;

    [Header("Goals Checked")]
    public bool unlocked;
}

[CreateAssetMenu(menuName = "Scriptable/achievement")]
public class AchievementObject : ScriptableObject
{
    [Header("My Achievement")]
    public Achievement[] achievements;

    public void AddAchievementProgress(EnumBadges ID, int value)
    {
        Achievement achievement = achievements.FirstOrDefault(x => x.ID == ID);

        if (!achievement.unlocked)
        {
            achievement.current = value;
            if (achievement.current >= achievement.goal)
            {
                achievement.current = achievement.goal;
                achievement.unlocked = true;

                if (AchiementNotificationUi.instance != null)
                    AchiementNotificationUi.instance.ShowAchievementUnlocked(achievement);
            }

            Score_Save_Pref.SaveAchievementData(ID, achievements);
        }
    }
}
