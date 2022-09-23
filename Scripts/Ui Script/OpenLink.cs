using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
#if UNITY_IPHONE
    private string iOS = "https://apps.apple.com/us/app/sudoku-legend-tournaments/id1516612343?action=write-review";
#elif UNITY_ANDROID
    private string android = "https://rubixcubestudios.com/project/170/";
#endif

    public void AddingLink(string AddLink)
    {
        Application.OpenURL(AddLink);
    }

    public void socialRate()
    {
#if UNITY_IPHONE
        Application.OpenURL(iOS);
#elif UNITY_ANDROID
        Application.OpenURL(android);
#endif

    }
}
