using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchiementNotificationUi : MonoBehaviour
{
    public static AchiementNotificationUi instance;

    private void Awake()
    {
        instance = this;
    }

    public Image icons;
    public Image background;
    public TMP_Text Title;
    public Animator animator;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public void ShowAchievementUnlocked(Achievement achievement)
    {
        icons.sprite = achievement.mainIcon;
        background.color = achievement._LockedAchivement;
        Title.text = @"Congrates you have Complete " +
            achievement.HeaderText;
        animator.SetTrigger("notif_active");
    }
}
