using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using UnityEditor;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;

    public List<GameObject> number_notes;
    private bool note_active;

    [HideInInspector]
    public int number_ = 0;
    [HideInInspector]
    public int correct_number_ = 0;

    private bool selected_ = false;
    private int square_index_ = -1;

    private int selectedScore = 1;
    private int WrongNUmber = 1;

    private bool has_default_value_ = false;
    private bool has_wrong_value_ = false;

    private Color WrongColor = new Color(0.754717f, 0.04007347f, 0,1);
    private Color TextColor;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip WrongaudioClip;
    public AudioClip RightaudioClip;
    public AudioClip clickAudio;

    private bool isHintPushed = false;

    public bool HasPushHintButton()
    {
        return isHintPushed;
    }

    public bool IsCorrectNumberSet()
    { return number_ == correct_number_; }

    public bool HasWrongValue() { return has_wrong_value_; }

    public int HasSelectedScore() { return selectedScore; }

    public void SetHasDefaultValue(bool has_default) { has_default_value_ = has_default; }
    public bool GetHasDefualtValue() { return has_default_value_; }

    public bool IsSelected() { return selected_; }
    public void SetSquareIndex(int index)
    {
        square_index_ = index;
    }

    public void SetCorrectNumber(int number)
    {
        correct_number_ = number;
    }

    public void SetCorrectNumber()
    {
        number_ = correct_number_;
        //SetNoteNumberValue(0);
        DisplayText();
        
    }

    protected override void Start()
    {
        selected_ = false;
        note_active = false;
       
        audioSource = this.gameObject.GetComponent<AudioSource>();
        interactable = true;
        //SetNoteNumberValue(0);
    }

    public List<string> GetSqureNotes()
    {
        List<string> notes = new List<string>();

        foreach (var number in number_notes)
        {
            notes.Add(number.GetComponent<TMP_Text>().text);
        }

        return notes;
    }

    private void SetClearEmptyNotes()
    {
        foreach (var number in number_notes)
        {
            if (number.GetComponent<TMP_Text>().text == "0")
                number.GetComponent<TMP_Text>().text = " ";
        }
    }

    public void DisplayText()
    {
        if (number_ <= 0)
            number_text.GetComponent<TMP_Text>().text = "";
        else
            number_text.GetComponent<TMP_Text>().text = number_.ToString();
    }

    public void SetNumber(int number)
    {
        number_ = number;
        has_wrong_value_ = false;
        DisplayText();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        selected_ = true;
        HelpScript._instance.InstroductionSwap();
        GameEvents.SquareSelectedMethod(square_index_);
        audioSource.PlayOneShot(clickAudio);
        GameEvents.OnnextStepsMethod(true);
        GameEvents.OnHintButtonMethod(true);
        GameEvents.OnAddKeyBoardMethod(true);
        GameEvents.onActiveHintButtonMethod(true);
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
    }

    protected override void OnEnable()
    {
        GameEvents.onUpdateSqureNumber += OnSetNumber;
        GameEvents.onSquareSelected += OnSquareSelected;
        GameEvents.OnClearNumber += OnClearNumber;
        GameEvents.onSetGameInactive += SetInActiveObject;
    }

    protected override void OnDisable()
    {
        GameEvents.onUpdateSqureNumber -= OnSetNumber;
        GameEvents.onSquareSelected -= OnSquareSelected;
        GameEvents.OnClearNumber -= OnClearNumber;
        GameEvents.onSetGameInactive -= SetInActiveObject;
    }

    private void SetInActiveObject()
    {
        interactable = false;

    }

    public void OnClearNumber()
    {
        
        if (number_ != correct_number_ && !has_default_value_)
        {
            number_ = 0;
            has_wrong_value_ = false;
            DisplayText();
            selected_ = false;
        }

        string numberY = 0.ToString();
        GameEvents.updateSelectedSquareNumberMethod(numberY);
        SetSquareColour(Color.white);
    }

    private void OnSquareSelected(int square_index)
    {
        if (square_index_ != square_index)
        {
            selected_ = false;
        }

    }

    private void OnSetNumber(int number)
    {
        if (selected_ && has_default_value_ == false)
        {
            if (note_active == true && has_wrong_value_ == false)
            {
                //SetNoteSingleNumberValue(number);
            }
            else if (note_active == false)
            {
                //SetNoteNumberValue(0);
                SetNumber(number);

                if (number_ != correct_number_)
                {
                    has_wrong_value_ = true;
                    WrongNUmber = 1;
                    GameEvents.OnWrongNumberMethod();
                    SetSquareColour(WrongColor);
                    number_text.GetComponent<TMP_Text>().color = Color.white;
                    audioSource.PlayOneShot(WrongaudioClip);
                    GameEvents.OnAddWrongNumberMethod(WrongNUmber);
                }
                else
                {
                    has_wrong_value_ = false;
                    has_default_value_ = true;
                    audioSource.PlayOneShot(RightaudioClip);
                    SetSquareColour(Color.white);
                    GameEvents.OnRightNumberMethod();
                    PlayerSettings.setDiff++;
                    if (SinglePlayerColors.instance != null)
                        TextColor = SinglePlayerColors.instance.pushColorText;

                    number_text.GetComponent<TMP_Text>().color = TextColor;

                }

            }

            GameEvents.CheckBoardCompletedMethod();

            if (number_ != 0)
            {
                string numberY = number_text.GetComponent<TMP_Text>().text;
                GameEvents.updateSelectedSquareNumberMethod(numberY);
            }

          
        }

    }

    public void SetSquareColour(Color col)
    {
        var colors = this.colors;
        colors.normalColor = col;
        this.colors = colors;
    }
}
