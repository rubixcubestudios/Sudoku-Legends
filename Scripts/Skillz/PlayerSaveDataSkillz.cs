using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillzSDK;

public static class PlayerSaveDataSkillz
{

	private static void OnSentDataSuccess()
	{
		Debug.LogWarning("All good");
	}

	private static void OnSentDataFail(string reason)
	{
		Debug.LogWarning("Fail: " + reason);
	}

	public static void UpdateData(string key, object data)
	{
		Dictionary<string, object> dict = new Dictionary<string, object>();
		dict.Add(key, data);

		SkillzCrossPlatform.UpdateProgressionUserData(ProgressionNamespace.PLAYER_DATA, dict, OnSentDataSuccess, OnSentDataFail);
	}
}
