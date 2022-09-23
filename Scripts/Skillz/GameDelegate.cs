using System;
using System.Collections.Generic;
using SkillzSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameDelegate : SkillzMatchDelegate
{
    private const string GamePlay = "GamePlay - CompareScore";
    private const string StartMenuSceneName = "MainMenu";
    private const string Progress = "Progress";
    private const string SkillzScene = "SkillzMatch";

    public void OnMatchWillBegin(Match matchInfo)
    {
        // Please note Unity provides a helper property to determine the match type
        // if IsCustomSynchronousMatch is true we have a sync v2 match
        // handle your async game start
        SceneManager.LoadSceneAsync(GamePlay);

        if (matchInfo.GameParams.ContainsKey("setlevel"))
        {
            string SetLevel = matchInfo.GameParams["setlevel"];
            int thisLevel = int.Parse(SetLevel);

            switch (thisLevel)
            {
                case 0:
                    PlayerSettings.difficulty = SkillzCrossPlatform.Random.Range(50, 52);
                    break;
                case 1:
                    PlayerSettings.difficulty = SkillzCrossPlatform.Random.Range(51, 53);
                    break;
                case 2:
                    PlayerSettings.difficulty = SkillzCrossPlatform.Random.Range(52, 54);
                    break;
                case 3:
                    PlayerSettings.difficulty = SkillzCrossPlatform.Random.Range(53, 55);
                    break;
                case 4:
                    PlayerSettings.difficulty = SkillzCrossPlatform.Random.Range(56, 58);
                    break;
                case 5:
                    PlayerSettings.difficulty = SkillzCrossPlatform.Random.Range(59, 61);
                    break;
                case 6:
                    PlayerSettings.difficulty = SkillzCrossPlatform.Random.Range(62, 64);
                    break;
            }


            Debug.Log("Difficult: " + PlayerSettings.difficulty + " Timer: " + PlayerSettings.TimerSet);
        }

    }


    public static GameDelegate SetGameController()
    {
        SceneManager.LoadScene(SkillzScene);
        return new GameDelegate();
    }

    public void OnSkillzWillExit()
    {
        SceneManager.LoadSceneAsync(StartMenuSceneName);
    }

    public void OnProgressionRoomEnter()
    {
        SceneManager.LoadSceneAsync(Progress);
    }
}