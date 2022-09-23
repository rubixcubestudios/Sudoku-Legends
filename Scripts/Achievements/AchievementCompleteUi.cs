using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCompleteUi : MonoBehaviour
{
    public static AchievementCompleteUi instance;

    public Image getImage;

    private void Awake()
    {
        instance = this;
    }


}
