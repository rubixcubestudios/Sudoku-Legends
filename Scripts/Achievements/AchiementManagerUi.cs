using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchiementManagerUi : MonoBehaviour
{
    [Header("Image")]
    public Image mainIcon;
    public Image backgroundImage;

    [Header("Text")]
    public TMP_Text HeaderText;
    public TMP_Text ResaultText;

    [Header("Slider")]
    public Slider progressSlider;

    public void Start()
    {
        backgroundImage = GetComponent<Image>();
    }

}
