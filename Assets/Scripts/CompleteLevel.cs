﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
	public string menuSceneName = "MainMenu";
	public SceneFader sceneFader;

	public string nextLevel = "Level02";
	public int levelToUnlock = 2;

	public void Continue()
	{
		PlayerPrefs.SetInt("levelReached", levelToUnlock);
		sceneFader.FadeTo(nextLevel);
	}

	public void Menu()
	{
		sceneFader.FadeTo(menuSceneName);
	}
}
