using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	string mainMenuSceneName = "Main";
	string menuSceneName = "RiskMap";

	public SceneFader sceneFader;
	FaderTest fadertest;

	private void Awake()
	{
		FaderTest[] faders = Resources.FindObjectsOfTypeAll<FaderTest>();
		if (faders.Length == 1) fadertest = faders[0];
		// else what the fuck?
	}

	public void Retry()
	{
		//sceneFader.FadeTo(SceneManager.GetActiveScene().name);
		fadertest.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		//sceneFader.FadeTo(menuSceneName);
		fadertest.FadeTo(menuSceneName);
	}

	public void MainMenu()
	{
		//sceneFader.FadeTo(mainMenuSceneName);
		fadertest.FadeTo(mainMenuSceneName);
	}
}
