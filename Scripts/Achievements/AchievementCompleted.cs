using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCompleted : MonoBehaviour
{
    public static AchievementCompleted instace;

    public Transform parent;
    public AchievementCompleteUi completeUi;
    public Color blackColor;

    private void Awake()
    {
        instace = this;
        OnCompleteAchievement(AchiementManager.instance.achievementObject.achievements);
    }

    private void Update()
    {
        OnCompleteAchievement(AchiementManager.instance.achievementObject.achievements);
    }

    public void OnCompleteAchievement(Achievement[] achievements)
    {
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }

            RectTransform rect = parent.GetComponent<RectTransform>();
            int ach = 28 * achievements.Length;
            rect.sizeDelta = new Vector2(ach, 30);
        }

        foreach (Achievement achievement in achievements.OrderByDescending(x => x.unlocked))
        {
            var achievementObject = Instantiate(completeUi, parent);
            achievementObject.getImage.sprite = achievement.mainIcon;

            if (achievement.unlocked)
            {
                achievementObject.getImage.color = Color.white;
                achievementObject.GetComponent<Shadow>().enabled = true;

            }
            else
            {
                achievementObject.getImage.color = blackColor;
                achievementObject.GetComponent<Shadow>().enabled = false;

            }


        }
    }
}
