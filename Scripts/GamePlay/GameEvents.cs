using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void CheckBoardCompleted();
    public static event CheckBoardCompleted OnCheckBoardCompleted;

    public static void CheckBoardCompletedMethod()
    {
        if (OnCheckBoardCompleted != null)
            OnCheckBoardCompleted();
    }


    public delegate void updateSquareNumber(int number);
    public static event updateSquareNumber onUpdateSqureNumber;

    public static void updateSquareNumberMethod(int number)
    {
        if (onUpdateSqureNumber != null)
            onUpdateSqureNumber(number);
    }

    public delegate void updateSelectedSquareNumber(string number);
    public static event updateSelectedSquareNumber onUpdateSelectedSquareNumber;

    public static void updateSelectedSquareNumberMethod(string StringNumber)
    {
        if (onUpdateSelectedSquareNumber != null)
            onUpdateSelectedSquareNumber(StringNumber);
    }

    public delegate void SquareSelected(int square_index);
    public static event SquareSelected onSquareSelected;

    public static void SquareSelectedMethod(int square_index)
    {
        if (onSquareSelected != null)
            onSquareSelected(square_index);
    }


    public delegate void WrongNumber();
    public static event WrongNumber OnWrongNumber;

    public static void OnWrongNumberMethod()
    {
        if (OnWrongNumber != null)
            OnWrongNumber();
    }

    public delegate void RightNumber();
    public static event RightNumber OnRightNumber;

    public static void OnRightNumberMethod()
    {
        if (OnRightNumber != null)
            OnRightNumber();
    }

    //////////////////////////////////

    public delegate void ClearNumber();
    public static event ClearNumber OnClearNumber;

    public static void OnClearNumberMethod()
    {
        if (OnClearNumber != null)
            OnClearNumber();
    }

    //***********************************

    public delegate void BoardCompleted(bool Hint);
    public static event BoardCompleted OnBoardCompleted;

    public static void OnBoardCompletedMethod(bool Hint)
    {
        if (OnBoardCompleted != null)
            OnBoardCompleted(Hint);
    }

    //**************************************

    public delegate void BoardCompletedTwo(string title, float time);
    public static event BoardCompletedTwo OnBoardCompletedTwo;

    public static void OnBoardCompletedTwoMethod(string title, float time)
    {
        if (OnBoardCompletedTwo != null)
            OnBoardCompletedTwo(title, time);
    }

    //**************************************


    public delegate void addwrongNumber(int number);
    public static event addwrongNumber OnAddWrongNumber;

    public static void OnAddWrongNumberMethod(int number)
    {
        if (OnAddWrongNumber != null)
            OnAddWrongNumber(number);

        Debug.Log("Wrong Number: "+number);
    }

    //**************************************

    public delegate void nextSteps(bool step);
    public static event nextSteps OnnextSteps;

    public static void OnnextStepsMethod(bool steps)
    {
        if (OnnextSteps != null)
            OnnextSteps(steps);
    }

    //////////////////////////////////
    ///
    public delegate void HintButton(bool step);
    public static event HintButton onHintButton;

    public static void OnHintButtonMethod(bool steps)
    {
        if (onHintButton != null)
            onHintButton(steps);
    }

    //////////////////////////////////


    public delegate void WinnerPopupText();
    public static event WinnerPopupText OnWinnerPopupText;

    public static void OnWinnerPopupTextMethod()
    {
        if (OnWinnerPopupText != null)
            OnWinnerPopupText();
    }

    //////////////////////////////////
    ///

    public delegate void SetGameInactive();
    public static event SetGameInactive onSetGameInactive;

    public static void OnSetGameInactiveMethod()
    {
        if (onSetGameInactive != null)
            onSetGameInactive();
    }

    //////////////////////////////////

    ///////
    /// Keyboard

    public delegate void AddKeyBoard(bool v);
    public static event AddKeyBoard OnAddKeyBoard;

    public static void OnAddKeyBoardMethod(bool v)
    {
        if (OnAddKeyBoard != null)
            OnAddKeyBoard(v);
    }

    public delegate void ActiveHintButton(bool step);
    public static event ActiveHintButton onActiveHintButton;

    public static void onActiveHintButtonMethod(bool steps)
    {
        if (onActiveHintButton != null)
            onActiveHintButton(steps);
    }

}
