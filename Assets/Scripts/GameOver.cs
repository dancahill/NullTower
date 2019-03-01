using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	string mainMenuSceneName = "Main";
	string menuSceneName = "RiskMap";
	public SceneFader sceneFader;

	public void Retry()
	{
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
	}

	public void Menu()
	{
		sceneFader.FadeTo(menuSceneName);
	}

	public void MainMenu()
	{
		sceneFader.FadeTo(mainMenuSceneName);
	}
}
